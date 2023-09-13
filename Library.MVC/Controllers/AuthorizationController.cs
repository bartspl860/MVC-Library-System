using Library.BLL;
using Library.Model;
using Library.MVC.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Library.MVC.Controllers
{
    [Route("api/authorize")]
    [ApiController]
    public class AuthorizationController : Controller
    {
        private readonly IUserService _userService;

        public AuthorizationController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel credentials)
        {
            if (credentials == null)
            {
                return BadRequest("No credentials passed");
            }

            if (credentials.Username == null || credentials.Password == null)
            {
                return BadRequest("No username or password provided");
            }

            UserRequestModel loginModel = new UserRequestModel(credentials.Username, credentials.Password);

            if (!_userService.IsCredentialsValid(loginModel))
            {
                return Unauthorized(new { error = "Invalid username or password" });
            }

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("returntheslabpharaoncurse"));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var tokenOptions = new JwtSecurityToken(
                issuer: "https://localhost:64670",
                audience: "https://localhost:4200",
                claims: new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Sub, credentials.Username)
                },
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: signinCredentials
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            return Ok(new AuthResponse { Token = tokenString });
        }

        [HttpPost("logout"), Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult Logout()
        {
            //TODO: wylogowywanie

            return Ok("You logged out, trust me bro");
        }

        [HttpPost("register"), Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult Register([FromBody] LoginModel credentials)
        {
            //nulle chyba można usunąć bo to sprawdza na wejściu ale zostawie tobie do obejrzenia
            //Pusta nazwa użytkownika robi śmieszne rzeczy tzn, wszystkie zapytania przechodzą ale nic się w bazie nie zmienia
            if( (credentials.Username == null || credentials.Username == "") || (credentials.Password == null || credentials.Password == ""))
            {
                return BadRequest("No username or password provided");
            }

            if (_userService.IsUsernameExists(credentials.Username))
            {
                return Conflict("This user already exists, please choose another username");
            }

            UserRequestModel registerModel = new UserRequestModel(credentials.Username, credentials.Password);
            _userService.RegisterUser(registerModel);

            return Ok("You registered a new account, you can now log in");
        }

        [HttpPut("changePassword"), Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult ChangePassword([FromBody] ChangePasswordModel passwordModel)
        {
            string? tokenString = ReadJWTToken();

            if (tokenString == null)
            {
                return Unauthorized(new { error = "Unknown token" });
            }

            if (passwordModel.NewPassword == "")
            {
                return BadRequest("New password is too short");
            }

            //Lekki syf się tu zrobił ale nie mogłem tego zostawić bez komunikatu o złym haśle
            string username = _userService.JWTWhoAmI(tokenString);
            bool canChangePassword = _userService.IsCredentialsValid(new UserRequestModel(username, passwordModel.OldPassword));

            if (!canChangePassword)
            {
                return Unauthorized(new { error = "Old password don't match" });
            }

            _userService.UpdateUserPassword(tokenString, passwordModel.OldPassword, passwordModel.NewPassword);

            return Ok("Password changed!");
        }

        [HttpGet("whoami"), Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult WhoAmI()
        {
            string? tokenString = ReadJWTToken();
            if (tokenString != null)
            {
                return Ok(_userService.JWTWhoAmI(tokenString));
            }

            return Unauthorized(new { error = "Guest" });
        }

        private string? ReadJWTToken()
        {
            var authorizationHeader = HttpContext.Request.Headers["Authorization"].FirstOrDefault();

            if (authorizationHeader != null && authorizationHeader.StartsWith("Bearer "))
            {
                return authorizationHeader.Substring("Bearer ".Length).Trim();
            }

            return null;
        }
    }
}
