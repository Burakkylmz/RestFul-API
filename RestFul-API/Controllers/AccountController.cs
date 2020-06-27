using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestFul_API.Infrastructure.Entities.Concrete;
using RestFul_API.Infrastructure.Repository.Abstract;

namespace RestFul_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAuthRepository _repository;

        public AccountController(IAuthRepository userRepository)
        {
            this._repository = userRepository;
        }

        [HttpPost("login")]
        public IActionResult LogIn([FromBody] AppUser appUser)
        {
            var user = _repository.Authentication(appUser.UserName, appUser.Password);

            if (user == null)
            {
                return BadRequest(new { message = "User name or password is incorret..!" });
            }

            return Ok(appUser);
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] AppUser appUser)
        {
            bool ifUserUnique = _repository.IsUniqeuUser(appUser.UserName);

            if (!ifUserUnique)
            {
                return BadRequest(new { message = "User name already is exists..!" });
            }

            var user = _repository.Register(appUser.UserName, appUser.Password);

            if (user == null)
            {
                return BadRequest(new { message = "Something goes wrong..!" });
            }

            return Ok();
        }
    }
}