using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using ML.Business.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;


namespace ML.WebAPI.Controllers
{ 
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class LoginController : Controller
    {
        private readonly IConfiguration config;

        public LoginController(IConfiguration config)
        {
            this.config = config;
        }

        [HttpPost]
        public IActionResult Login([FromBody] UserDto user)
        {
            UserDto resultUser = AuthenticateUser(user);
            if (resultUser != null)
            {
                var token = GenerateJsonWebToken(resultUser);

                return Ok(token);

            }
            return Unauthorized();
        }

        private object GenerateJsonWebToken(UserDto user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:SecretKey"]));

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new Claim[]
            {
                new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Sub, user.Username),
                new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: config["Jwt:Issuer"],
                audience: config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private UserDto AuthenticateUser(UserDto user)
        {
            var users = new UserDto[]
            {
                new UserDto
                {
                    Password = "test1Password",
                    Username = "test1Username"
                },
                new UserDto
                {
                    Password = "test2Password",
                    Username = "test2Username"
                },
                new UserDto
                {
                    Password = "test3Password",
                    Username = "test3Username"
                }
            };

            var foundUser = users.FirstOrDefault(u => u.Username == user.Username && u.Password == user.Password);

            if (foundUser != null)
            {
                return new UserDto
                {
                    Username = foundUser.Username
                };
            }

            return null;
        }
    }
}
