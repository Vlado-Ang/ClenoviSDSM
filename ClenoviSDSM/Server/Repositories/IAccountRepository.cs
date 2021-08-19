using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClenoviSDSM.Shared.Models;


namespace ClenoviSDSM.Server.Repositories
{
    public interface IAccountRepository
    {
        public string GetAuthenticationToken(LoginModel loginModel);
    }
}
