using System.Text;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using DatingApp.API.Data;
using DatingApp.API.Dtos;
using DatingApp.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace DatingApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository repo;
        private readonly IConfiguration config;

        public AuthController(IAuthRepository repo , IConfiguration config)
        {
            this.repo = repo;
            this.config = config;
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserForRegistrationDto user){
            //validate later
            user.username = user.username.ToLower();
            if(await repo.UserExists(user.username)){
                return BadRequest("Username already exists.");
            }

            var UserToCreate = new User{
                Username = user.username

            };
            var createdUser = await repo.Register(UserToCreate,user.Password);
            return StatusCode(201);
        }

        [HttpPost("login")]

        public async Task<IActionResult> Login(UserForloginDto login){

            var _user = await repo.Login(login.username.ToLower(),login.Password);

            if(_user==null)
            return Unauthorized();

            var claims = new []{
              new  Claim(ClaimTypes.NameIdentifier,_user.Id.ToString()),
              new  Claim(ClaimTypes.Name,_user.Username)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key,SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor{
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

        return Ok(new{
            token=tokenHandler.WriteToken(token)
        });
        } 


    }
}