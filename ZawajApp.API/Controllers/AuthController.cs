using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Mvc;
using ZawajApp.API.Date.Interfaces;
using ZawajApp.API.Dtos;
using ZawajApp.API.Models;
using System.Text;
using Microsoft.Extensions.Configuration;
using System;
using System.IdentityModel.Tokens.Jwt;

namespace ZawajApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _auth;
        private readonly IConfiguration _config;
        public AuthController(IAuthRepository auth, IConfiguration config)
        {
            _config = config;
            _auth = auth;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDto userForRegisterDto)
        {
            //validation
            userForRegisterDto.Username = userForRegisterDto.Username.ToLower();
            if (await _auth.IsUserExists(userForRegisterDto.Username)) return BadRequest("User is exist");
            var userToCreate = new User
            {
                Username = userForRegisterDto.Username
            };

            var createdUser = await _auth.RegisterAsync(userToCreate, userForRegisterDto.Password);
            return StatusCode(201);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserToLoginDto userToLoginDto)
        {
            var user = await _auth.Login(userToLoginDto.Username.ToLower(), userToLoginDto.Password);

            if (user == null) return Unauthorized();

            var claims = new[]{
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username)
            };

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));
            var credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha512);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = credentials
            };

            var tokenHndler = new JwtSecurityTokenHandler();
            var token = tokenHndler.CreateToken(tokenDescriptor);

            return Ok(new { token = tokenHndler.WriteToken(token) });
        }
    }
}