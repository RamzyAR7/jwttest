using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Test.Entities;
using Test.Services;

namespace Test.Controllers
{
    [ApiController]
    [Route("api[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserServices _user;
        public UserController(IUserServices user)
        {
            _user = user;
        }

        [Authorize(Roles = "admin")]
        [HttpGet("admin")]
        public ActionResult<ICollection<User>> GetUsers()
        {
            var users = _user.GetUsers();
            return Ok(users);
        }
        [Authorize(Roles = "admin, user")]
        [HttpGet("user")]
        public ActionResult<string> UsersOnly()
        {
            return Ok($"Hello user");
        }
    }
}
