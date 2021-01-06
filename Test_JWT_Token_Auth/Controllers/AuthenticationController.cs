using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test_JWT_Token_Auth.Models;
using Test_JWT_Token_Auth.Services;

namespace Test_JWT_Token_Auth.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private IAuthenticateService _authenticateService;
        public AuthenticationController(IAuthenticateService authenticateService)
        {
            _authenticateService = authenticateService;
        }
        [HttpPost]
        public IActionResult Post([FromBody]User model)
        {
            var user = _authenticateService.Authenticate(model.UserName, model.Password);

            if(user == null)
            {
                return BadRequest(new { message = "Username or Password is incorrect" });
            }

            return Ok(user);
        }
    }
}
