using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SeniorLearnDataSeed.Data.Core;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace SeniorLearnWebApi.Controllers
{
    [Route("api/v1/token")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly UserManager<ApplicationUser> _userManager;

        public TokenController(
            IConfiguration config,
            UserManager<ApplicationUser> userManager)
        {
            _config = config;
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginApiModel model)
        {
            if(ModelState.IsValid)
            {
                ApplicationUser? user = await _userManager.FindByEmailAsync(model.Email);

                if(user != null && await _userManager.CheckPasswordAsync(user, model.Password))
                {
                    List<Claim> claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.Id),
                        new Claim(ClaimTypes.Name, user.UserName),
                        new Claim(ClaimTypes.Email, user.Email),
                        new Claim(JwtRegisteredClaimNames.Sub, _config["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString())

                    };
                    var roles = (await _userManager.GetRolesAsync(user))
                        .Select(role => new Claim(ClaimTypes.Role, role));
                    claims.AddRange(roles);

                    SymmetricSecurityKey key = new SymmetricSecurityKey(Convert.FromBase64String(_config["Jwt:Key"]));

                    SigningCredentials sign = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                    JwtSecurityToken token = new JwtSecurityToken(
                        _config["Jwt:Issuer"],
                        _config["Jwt:Audience"],
                        claims,
                        expires: DateTime.UtcNow.AddDays(1),
                        signingCredentials: sign);

                    return Ok(new JwtSecurityTokenHandler().WriteToken(token));

                }
            }
            return BadRequest("Invalid credentials");
            

        }
        

        
    }
    public class LoginApiModel
    {
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? Password { get; set; }
    }
}
