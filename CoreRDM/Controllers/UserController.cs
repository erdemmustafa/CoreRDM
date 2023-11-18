using CoreRDM.Models;
using CoreRDM.Services;
using Microsoft.AspNetCore.Mvc;

namespace CoreRDM.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private IUserService _userService;
        private readonly NHibernate.ISession _session;

        public UserController(IUserService userService, NHibernate.ISession session)
        {
            _userService = userService;
            _session = session;
        }
        [HttpPost("GetToken")]
        public IActionResult Authenticate(AuthenticateRequest model)
        {

            var response = _userService.Authenticate(model);


            if (response == null)
                return BadRequest(new { message = "Kullanıcı adı veya şifre hatalı " });

            return Ok(response);
        }
        [HttpGet("getAll")]
        public IActionResult GetAll()
        {
            var users = _userService.GetAll();
            return Ok(users);
        }
    }
}
