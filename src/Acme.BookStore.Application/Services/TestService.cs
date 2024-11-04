using Acme.BookStore.IServices;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Account;

namespace Acme.BookStore.Services
{
    public class TestService:BookStoreAppService,ITestService
    {
        public TestService()
        {
            
        }
        [Authorize]
        public List<Claim> GetCurrentUser()
        {
            var v = CurrentUser.GetAllClaims().ToList();
            return v;
        }
    }
}
