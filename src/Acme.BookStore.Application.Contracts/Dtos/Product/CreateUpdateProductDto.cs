using Acme.BookStore.Consts;
using Acme.BookStore.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acme.BookStore.Dtos.Product
{
    public class CreateUpdateProductDto
    {
        [Required(ErrorMessage = "必要参数")]
        public Guid CategoryId { get; set; }
        [Required(ErrorMessage = "必要参数")]
        [StringLength(ProductManagement.MaxNameLength)]
        public string Name { get; set; }
        public float Price { get; set; }
        public bool IsFreeCargo { get; set; }
        public DateTime ReleaseDate { get; set; }
        public ProductStockState StockState { get; set; }
    }
}
