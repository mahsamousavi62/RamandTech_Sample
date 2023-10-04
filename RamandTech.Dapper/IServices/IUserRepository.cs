using RamandTech.Dapper.Entities;
using RamandTech.Service.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RamandTech.Dapper.IServices
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User> GetByUserNameAndPassword(string userName, string password);
        Task<string> Authenticate(User user);
    }
}
