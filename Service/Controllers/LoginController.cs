using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
//using JWTAuthenticationExample.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ModelLayer.Model;
using Service.Utility;

namespace Service.Controllers
{
[ApiController]
[Route("[controller]")]
public class LoginController : ControllerBase
    {
        private readonly IConfiguration _config;
        private List<Users> appUsers = new List<Users>
        {
            new Users { FullName = "Sumit Gupta", UserName = "admin", Password = "1234" },//UserRole = "Admin"
            new Users { FullName = "Test User", UserName = "user", Password = "1234",  }  //UserRole = "User"
        };

        public LoginController(IConfiguration config)
        {
            _config = config;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("api/test")]
        public IActionResult test()
        {
            return Ok("test");
        }


        [HttpPost]
        [AllowAnonymous]
        [Route("api/PasswordHash")]
        public IActionResult PasswordHash([FromBody]Users user)
        {

            HashingOptions hashingOptions = new HashingOptions();
            _config.Bind(hashingOptions);
            IOptions<HashingOptions> myOptions = Options.Create(hashingOptions);

            PasswordHasher passwordHasher = new PasswordHasher(myOptions);

            var hashedPass = passwordHasher.HashPassword(user, user.Password);
            var verify = passwordHasher.VerifyHashedPassword(user,hashedPass,user.Password);

            return Ok(verify);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("api/login")]
        public IActionResult Login([FromBody]Users login) //[FromBody]User login
        {

            IActionResult response = Unauthorized();
            Users user = AuthenticateUser(login);
            if (user != null)
            {
                var tokenString = GenerateJWTToken(user);
                response = Ok(new
                {
                    token = tokenString,
                    userDetails = user,
                });
            }
            return response;
        }

        Users AuthenticateUser(Users loginCredentials)
        {
            Users user = appUsers.SingleOrDefault(x => x.UserName == loginCredentials.UserName && x.Password == loginCredentials.Password);
            return user;
        }

        string GenerateJWTToken(Users userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("Jwt").GetSection("SecretKey").Value ));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userInfo.UserName),
                new Claim("fullName", userInfo.FullName.ToString()),
             //   new Claim("role",userInfo.UserRole),
                new Claim("role","Admin"),

                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };
            var token = new JwtSecurityToken(
            issuer: _config.GetSection("Jwt").GetSection("Issuer").Value,
            audience: _config.GetSection("Jwt").GetSection("Audience").Value,
            claims: claims,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: credentials
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}