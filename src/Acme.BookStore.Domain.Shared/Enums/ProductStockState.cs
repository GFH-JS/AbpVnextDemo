using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acme.BookStore.Enums
{
    public enum ProductStockState : byte
    {
        PreOrder,
        Instock,
        NotAvailable,
        Stopped
    }
}
