using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test_JWT_Token_Auth.Models;

namespace Test_JWT_Token_Auth.Services
{
    public interface IAuthenticateService
    {
        User Authenticate(string userName, string password);
    }
}
