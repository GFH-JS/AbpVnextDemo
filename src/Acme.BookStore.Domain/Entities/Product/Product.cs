using Acme.BookStore.Entities.Category;
using Acme.BookStore.Enums;
using Acme.BookStore.Etos;
using Acme.BookStore.EventHandlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Volo.Abp.Domain.Entities.Auditing;

namespace Acme.BookStore.Entities.Product
{
    public class Product:FullAuditedAggregateRoot<Guid>
    {
        public Guid CategoryId { get; set; }
        public Category.Category Category { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public bool IsFreeCargo { get; set; }
        public DateTime ReleaseDate { get; set; }
        public ProductStockState StockState { get; set; }



        /// <summary>
        /// 聚合根发布事件
        /// </summary>
        public void CreateEvent()
        {
            this.AddLocalEvent(new ProductEventArgs() { Id = Id, Name = Name });
            //AddDistributedEvent
        }

    }
}
