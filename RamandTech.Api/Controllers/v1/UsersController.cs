namespace RamandTech.Api.Controllers.v1;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RamandTech.Api.Model;
using RamandTech.Dapper.Entities;
using RamandTech.Dapper.IServices;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserRepository _userRepository;

    public UsersController(IUserRepository userService)
    {
        _userRepository = userService;
    }

    [MapToApiVersion("1.0")]
    [HttpPost("Authenticate")]
    [ProducesResponseType(400)]
    [ProducesResponseType(200, Type = typeof(string))]
    public async Task<IActionResult> Authenticate(AuthenticateRequest model)
    {

        var user = await _userRepository.GetByUserNameAndPassword(model.Username, model.Password);
        if (user is null)
            return BadRequest("Username or password is incorrect");

        var response = await _userRepository.Authenticate(user);

        return Ok(response);
    }

    [MapToApiVersion("1.0")]
    [Authorize]
    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<User>))]
    [ProducesResponseType(401)]
    public async Task<IActionResult> GetAll()
    {
        var users = await _userRepository.GetAllAsync();
        return Ok(users);

    }
}