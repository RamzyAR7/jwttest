using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

        [Authorize]
        [HttpGet("getall")]
        public ActionResult<ICollection<User>> GetUsers()
        {
            var users = _user.GetUsers();
            return Ok(users);
        }
    }
}
