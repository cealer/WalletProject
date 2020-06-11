using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalletService.API.Infrastructure.Infrastructure.Services
{
    public interface IIdentityService 
    {
        string GetUserIdentity();

        string GetUserName();
    }
}
