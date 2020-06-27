using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestFul_API.Infrastructure.Entities.Concrete
{
    public class AppUser:KernelEntity
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }
    }
}
