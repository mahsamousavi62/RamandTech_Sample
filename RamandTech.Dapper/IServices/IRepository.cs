using RamandTech.Dapper.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RamandTech.Service.IServices
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int id=1);
        Task<IReadOnlyList<T>> GetAllAsync();
       
    }
}
