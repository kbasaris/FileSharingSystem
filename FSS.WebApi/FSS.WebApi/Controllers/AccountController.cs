using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FSS.Data.Services;
using FSS.Data.Models;
using FSS.Data.Helpers;
using Newtonsoft.Json;
using FSS.Data.Mappings;

namespace FSS.WebApi.Controllers
{

    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly ILogger _logger;
        private readonly IJwtFactory _jwtFactory;
        private readonly JwtIssuerOptions _jwtOptions;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IEmailSender emailSender,
            ILogger<AccountController> logger, 
            IJwtFactory jwtFactory, JwtIssuerOptions jwtOptions)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _logger = logger;
            _jwtFactory = jwtFactory;
            _jwtOptions = jwtOptions;
        }

        [TempData]
        public string ErrorMessage { get; set; }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Post([FromBody]LoginDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var identity = await GetClaimsIdentity(loginDto.Email, loginDto.Password);
            if (identity == null)
            {
                ModelState.AddModelError("login_error", "Invalid username oa password");
                return BadRequest(ModelState);
            }

            var jwt = await Tokens.GenerateJwt(identity, _jwtFactory, loginDto.Email, _jwtOptions, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return Ok(jwt);
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Post([FromBody]RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            ApplicationUser userIdentity = Mappings.FromRegisterViewModelToApplicationUser(model);

            var result = await _userManager.CreateAsync(userIdentity, model.Password);

            if (!result.Succeeded) {
                var error = result.Errors.First();
                if (error != null)
                {
                    ModelState.AddModelError("register_error", error.Description);
                    return new BadRequestObjectResult(ModelState);
                }
            }

            var identity = await GetClaimsIdentity(model.Email, model.Password);
            if (identity == null)
            {
                ModelState.AddModelError("login_error", "Invalid username oa password");
                return BadRequest(ModelState);
            }

            var jwt = await Tokens.GenerateJwt(identity, _jwtFactory, model.Email, _jwtOptions, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return Ok(jwt);
        }
        private async Task<ClaimsIdentity> GetClaimsIdentity(string userName, string password)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
                return await Task.FromResult<ClaimsIdentity>(null);

            // get the user to verifty
            var userToVerify = await _userManager.FindByNameAsync(userName);

            if (userToVerify == null) return await Task.FromResult<ClaimsIdentity>(null);

            // check the credentials
            if (await _userManager.CheckPasswordAsync(userToVerify, password))
            {
                return await Task.FromResult(_jwtFactory.GenerateClaimsIdentity(userName, userToVerify.Id));
            }

            // Credentials are invalid, or account doesn't exist
            return await Task.FromResult<ClaimsIdentity>(null);
        }
    }
}
