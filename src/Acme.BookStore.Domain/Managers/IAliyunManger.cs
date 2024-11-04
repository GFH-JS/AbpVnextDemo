using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace Acme.BookStore.Managers
{
    public interface IAliyunManger:IDomainService
    {
        Task SendSmsAsync(string phoneNumbers, string code);
    }
}
