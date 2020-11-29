using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer.IBusiness;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.ViewModel;
using Service.Utility;

namespace Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginAccessController : ControllerBase
    {
        private readonly ILoginAccess loginAccess;

        public LoginAccessController(ILoginAccess loginAccess)
        {
            this.loginAccess = loginAccess;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("login")]
        [TypeFilter(typeof(CustomExceptionFilter))]
        public IActionResult Login([FromBody]UsersDto login) //[FromBody]User login
        {
            IActionResult response = Unauthorized();
            var users= loginAccess.VerifyLogin(login);
            response = Ok(users);
            return response;
        }
    }
}