using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RestFul_API.Infrastructure.Context;
using RestFul_API.Infrastructure.CustomSettings;
using RestFul_API.Infrastructure.Entities.Concrete;
using RestFul_API.Infrastructure.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RestFul_API.Infrastructure.Repository.Concrete
{
    public class EfAuthRepository : IAuthRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly AppSettings _appSettings;


        public EfAuthRepository(ApplicationDbContext applicationDbContext, IOptions<AppSettings> appSettings)
        {
            this._context = applicationDbContext;
            this._appSettings = appSettings.Value;
        }


        public AppUser Authentication(string userName, string password)
        {
            var user = _context.Users.SingleOrDefault(x => x.UserName == userName && x.Password == password);

            if (user == null)
            {
                return null;
            }
            else
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_appSettings.SecretKey);
                var tokenDescriptor = new SecurityTokenDescriptor()
                {
                    Subject = new ClaimsIdentity(new Claim[] {
                        new Claim(ClaimTypes.Name, user.Id.ToString())
                    }),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                user.Token = tokenHandler.WriteToken(token);

                return user;
            }
        }

        public bool IsUniqeuUser(string userName)
        {
            var user = _context.Users.SingleOrDefault(x => x.UserName == userName);

            if (user == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public AppUser Register(string userName, string password)
        {
            AppUser userObj = new AppUser()
            {
                UserName = userName,
                Password = password,
            };

            _context.Users.Add(userObj);
            _context.SaveChanges();

            return userObj;
        }
    }
}
