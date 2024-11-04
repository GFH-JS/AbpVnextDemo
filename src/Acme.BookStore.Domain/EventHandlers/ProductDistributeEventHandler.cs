using Acme.BookStore.Entities.Product;
using Acme.BookStore.Etos;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Events;
using Volo.Abp.Domain.Entities.Events.Distributed;
using Volo.Abp.EventBus.Distributed;

namespace Acme.BookStore.EventHandlers
{
    /// <summary>
    /// 分布式订阅(如果不配置 默认实现本地事件)
    /// </summary>
    public class ProductDistributeEventHandler : IDistributedEventHandler<ProductEventArgs>,IDistributedEventHandler<EntityCreatedEto<ProductEventArgs>>
    {
        private readonly ILogger<ProductDistributeEventHandler> _logger;
        public ProductDistributeEventHandler(ILogger<ProductDistributeEventHandler> logger)
        {
            _logger = logger;
        }
        public Task HandleEventAsync(ProductEventArgs eventData)
        {
            _logger.LogInformation(eventData.Id + eventData.Name);
            return Task.CompletedTask;
        }

        public Task HandleEventAsync(EntityCreatedEto<ProductEventArgs> eventData)
        {
            throw new NotImplementedException();
        }
    }
}
