using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Test.Contexts;
using Test.DTOs;
using Test.Entities;
using Test.Repository;
using Test.Services;

namespace Test.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserServices _user;
        public AuthController(IUserServices user)
        {
            _user = user;
        }


        [HttpPost("register")]
        public ActionResult<User> Register(UserDto request)
        {
            var user = _user.Register(request);
            if (user == null)
            {
                return BadRequest("the username is already used");
            }
            return Ok(user);
        }
        [HttpPost("login")]
        public ActionResult<string> Login(UserDto request)
        {
            var token = _user.Login(request);
            if(token == null)
            {
                return BadRequest("username or password is wrong");
            }
            return Ok(token);
        }
    }
}
