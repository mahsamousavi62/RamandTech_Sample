using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RamandTech.Dapper;
using RamandTech.Dapper.Entities;
using RamandTech.Dapper.IServices;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

public class UserRepository : IUserRepository
{
    private readonly IConfiguration configuration;
    public UserRepository(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    public async Task<string> Authenticate(User user)
    {

        var token = GenerateJwtToken(user);

        return token;
    }

    private string GenerateJwtToken(User user)
    {

        var tokenHandler = new JwtSecurityTokenHandler();
        string k = configuration["SecretKey:key"];
        var key = Encoding.ASCII.GetBytes(k);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
            Expires = DateTime.UtcNow.AddDays(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
    public async Task<IReadOnlyList<User>> GetAllAsync()
    {
        using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
        {
            connection.Open();
            var result = await connection.QueryAsync<User>("SP_GetUsers", commandType: CommandType.StoredProcedure);
            return result.ToList();
        }
    }
    public async Task<User> GetByIdAsync(int id)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@Id", id, DbType.Int32);
        using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
        {
            connection.Open();
            var result = await connection.QueryFirstOrDefaultAsync<User>("SP_GetUserById", parameters, commandType: CommandType.StoredProcedure);
            return result;
        }
    }

    public async Task<User> GetByUserNameAndPassword(string userName, string password)
    {

        using var sha1 = SHA1.Create();
        var pass= Convert.ToHexString(sha1.ComputeHash(Encoding.UTF8.GetBytes(password)));

        var parameters = new DynamicParameters();
        parameters.Add("@UserName", userName, DbType.String);
        parameters.Add("@Password", pass, DbType.String);

        using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
        {
            connection.Open();
            var result = await connection.QueryFirstOrDefaultAsync<User>("SP_GetUserByUserNameAndPassword", parameters, commandType: CommandType.StoredProcedure);
            return result;
        }
    }


}