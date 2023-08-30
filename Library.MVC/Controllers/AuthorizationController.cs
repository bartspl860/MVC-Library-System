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
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel credentials)
        {
            if (credentials == null)
            {
                return BadRequest("No credentials passed");
            }

            //TODO: proces logowania

            //Replace later
            if (credentials.Username == "admin" && credentials.Password == "admin")
            {
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("returntheslabpharaoncurse"));
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                var tokenOptions = new JwtSecurityToken(
                    issuer: "https://localhost:64670",
                    audience: "https://localhost:4200",
                    claims: new List<Claim>(),
                    expires: DateTime.Now.AddMinutes(30),
                    signingCredentials: signinCredentials
                );

                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
                return Ok(new AuthResponse { Token = tokenString });
            }
            //^^^Replace later^^^

            return Unauthorized();
        }

        [HttpPost("logout"), Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult Logout()
        {
            //TODO: wylogowywanie

            return Ok("You logged out");
        }
    }
}
