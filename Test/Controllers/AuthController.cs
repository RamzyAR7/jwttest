using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Test.Contexts;
using Test.DTOs;
using Test.Entities;
using Test.Repository;

namespace Test.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserDbContext _dbcontext;
        private readonly IHelperRepository _helper;

        public AuthController(UserDbContext dbcontect, IHelperRepository helper)
        {
            _dbcontext = dbcontect;
            _helper = helper;
        }

        [HttpPost("register")]
        public ActionResult<User> Register(UserDto request)
        {
            var user = new User
            {
                UserName = request.UserName
            };

            var hashedPassword = new PasswordHasher<User>()
                .HashPassword(user, request.Password);

            user.PasswordHash = hashedPassword;

            _dbcontext.Users.Add(user);
            _dbcontext.SaveChanges();

            return Ok(user);
        }
        [HttpPost("login")]
        public ActionResult<string> Login(UserDto request)
        {
            var user = _dbcontext.Users.FirstOrDefault(u => u.UserName == request.UserName);
            if (user == null)
            {
                return BadRequest("username is wrong");
            }
            // VerifyHashedPassword -> enum of type PasswordVerificationResult
            if (new PasswordHasher<User>().VerifyHashedPassword(user, user.PasswordHash, request.Password)
                == PasswordVerificationResult.Success )
            {
                string token = _helper.CreateToken(user);
                return Ok(token);
            }
            else
            {
                return BadRequest("password is wrong");
            }


        }
    }
}
