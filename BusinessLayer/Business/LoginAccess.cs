using AutoMapper;
using BusinessLayer.IBusiness;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ModelLayer.Model;
using ModelLayer.ViewModel;
using RepositoryLayer.IRepository;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace BusinessLayer.Business
{
    public class LoginAccess : ILoginAccess
    {
        private readonly IConfiguration _config;
        public LoginAccess(IMapper mapper,IOptions<JwtDto> jwt,IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.jwt = jwt;
            this.unitOfWork = unitOfWork;
        }
        
        private readonly IMapper mapper;
        private readonly IOptions<JwtDto> jwt;
        private readonly IUnitOfWork unitOfWork;
        private List<Users> appUsers = new List<Users>
        {
            new Users { FullName = "Sumit Gupta", UserName = "admin", Password = "1234" },//UserRole = "Admin"
            new Users { FullName = "Test User", UserName = "user", Password = "1234",  }  //UserRole = "User"
        };
        public LoginResponse VerifyLogin(UsersDto login)
        {
            //IActionResult response = Unauthorized();
            LoginResponse loginResponse = new LoginResponse();
            Users user = AuthenticateUser(login);
            if (user != null)
            {
                var tokenString = GenerateJWTToken(user);
                
                loginResponse.token = tokenString;
               UsersDto users= mapper.Map<UsersDto>(user);
                loginResponse.users = users;
            }
            return loginResponse;
        }


        Users AuthenticateUser(UsersDto loginCredentials)
        {
            //  Users user = appUsers.SingleOrDefault(x => x.UserName == loginCredentials.UserName && x.Password == loginCredentials.Password);

            HashingOptions hashingOptions = new HashingOptions();
            IOptions<HashingOptions> hashingOption = Options.Create(hashingOptions);
            PasswordHasher passwordHasher = new PasswordHasher(hashingOption);
          
            Users user =unitOfWork.UsersRepository.GetUserLogin(loginCredentials);
        //    var hashedPass = passwordHasher.HashPassword(user, loginCredentials.Password);
            var  verificationResult=    passwordHasher.VerifyHashedPassword(user, user.Password, loginCredentials.Password);
            if (verificationResult == PasswordVerificationResult.Success) { return user; }
            else { return null; }
        }

        string GenerateJWTToken(Users userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Value.SecretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, userInfo.UserName));
            claims.Add(new Claim(ClaimTypes.Name, userInfo.FullName.ToString()));
            foreach (UserRole userRole in userInfo.UserRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, userRole.Role.Name));
            }
            foreach (UserClaims userClaim in userInfo.UserClaims)
            {
                claims.Add(new Claim(type: userClaim.ClaimType, value: userClaim.ClaimValue));
            }
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString());

            var token = new JwtSecurityToken(
            issuer: jwt.Value.Issuer,
            audience: jwt.Value.Audience,
            claims: claims,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: credentials
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
