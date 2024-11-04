using Acme.BookStore.Options;
using AlibabaCloud.SDK.Dysmsapi20170525;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Services;

namespace Acme.BookStore.Managers
{
    internal class AliyunManger : DomainService, IAliyunManger
    {
        private readonly ILogger _logger;
        private readonly AliyunOptions Options;
        public AliyunManger(ILogger<AliyunManger> logger, IOptions<AliyunOptions> options)
        {
            Options = options.Value;
            _logger = logger;
        }

        private Client CreateClient()
        {
            AlibabaCloud.OpenApiClient.Models.Config config = new AlibabaCloud.OpenApiClient.Models.Config
            {
                // 必填，您的 AccessKey ID
                AccessKeyId = Options.AccessKeyId,
                // 必填，您的 AccessKey Secret
                AccessKeySecret = Options.AccessKeySecret,
            };
            // 访问的域名
            config.Endpoint = "dysmsapi.aliyuncs.com";
            return new Client(config);
        }
        public async Task SendSmsAsync(string phoneNumbers, string code)
        {
            try
            {
                var _aliyunClient = CreateClient();
                AlibabaCloud.SDK.Dysmsapi20170525.Models.SendSmsRequest sendSmsRequest = new AlibabaCloud.SDK.Dysmsapi20170525.Models.SendSmsRequest
                {
                    PhoneNumbers = phoneNumbers,
                    SignName = Options.Sms.SignName,
                    TemplateCode = Options.Sms.TemplateCode,
                    TemplateParam = System.Text.Json.JsonSerializer.Serialize(new { code })
                };

                var response = await _aliyunClient.SendSmsAsync(sendSmsRequest);
            }

            catch (Exception _error)
            {
                _logger.LogError(_error, "阿里云短信发送错误:" + _error.Message);
                throw new UserFriendlyException("阿里云短信发送错误:" + _error.Message);
            }
        }
    }
}
