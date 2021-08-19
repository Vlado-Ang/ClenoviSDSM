using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClenoviSDSM.Shared.Models;

namespace ClenoviSDSM.Client.Services
{
    interface IAcountService
    {
        Task<bool> LoginAsync(LoginModel model);
        Task<bool> LogoutAsync();
    }
}
