﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Acme.BookStore.Dtos.Product
{
    public class ProductRequestDto : PagedAndSortedResultRequestDto
    {
        public string? Name { get; set; }
    }
}
