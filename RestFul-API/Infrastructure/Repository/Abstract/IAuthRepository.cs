using RestFul_API.Infrastructure.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestFul_API.Infrastructure.Repository.Abstract
{
    public interface IAuthRepository
    {
        AppUser Authentication(string userName, string password);
        AppUser Register(string userName, string password);
        bool IsUniqeuUser(string userName);
    }
}
