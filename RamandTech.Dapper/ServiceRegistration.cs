using Microsoft.Extensions.DependencyInjection;
using RamandTech.Dapper.IServices;
using RamandTech.Service.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RamandTech.Dapper
{
    public static class ServiceRegistration
    {
        public static void AddDapper(this IServiceCollection services)
        {
            
            services.AddTransient<IUserRepository, UserRepository>();
        }
    }
}
