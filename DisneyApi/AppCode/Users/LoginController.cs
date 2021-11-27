using DisneyApi.AppCode.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DisneyApi.AppCode.Users
{
    [Route("auth")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUserCommandService _cmdService;
        private readonly IAccountService _accountService;
        public LoginController(IUserCommandService cmdService, IAccountService accountService)
        {
            _cmdService = cmdService;
            _accountService = accountService;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        public IActionResult Login(User user)
        {
            IActionResult response = Unauthorized();
            
            var accepted = _accountService.ValidateUser(user.email, user.password);
            if (accepted)
            {
                var tokenString = _accountService.GetToken();
                response = Ok(new { token = tokenString });
            }
            return response;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("register")]
        public async Task<ActionResult> RegisterAsync( RegisterData model)
        {
            bool nameAlreadyExists =  _cmdService.UserNameExists(model.email);
            if(nameAlreadyExists)
            {
                return BadRequest("User name already exists");
            }
            
            var newUserId = await _cmdService.RegisterUserAsync(model);
            return Ok(newUserId);
        }

    }
}