using Acme.BookStore.Entities.Product;
using Acme.BookStore.Etos;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Entities.Events;
using Volo.Abp.EventBus;
using Volo.Abp.EventBus.Local;

namespace Acme.BookStore.EventHandlers
{

    /// <summary>
    /// 订阅事件
    /// </summary>
    public class ProductEventHandler : ILocalEventHandler<ProductEventArgs>, ITransientDependency,ILocalEventHandler<EntityCreatedEventData<Product>>
    {
        private readonly ILogger<ProductEventHandler> _logger;
        public ProductEventHandler(ILogger<ProductEventHandler> logger)
        {
            _logger = logger;
        }
        public Task HandleEventAsync(ProductEventArgs eventData)
        {
            _logger.LogInformation(eventData.Name);
            return Task.CompletedTask;
        }

        /// <summary>
        /// abp 框架自动发布
        /// </summary>
        /// <param name="eventData"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task HandleEventAsync(EntityCreatedEventData<Product> eventData)
        {
            _logger.LogInformation(eventData.Entity.Name);
            return Task.CompletedTask;
        }
    }


}
