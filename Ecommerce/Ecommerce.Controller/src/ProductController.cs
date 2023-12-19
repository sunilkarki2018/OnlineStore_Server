using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Business.src.Abstractions;
using Ecommerce.Business.src.DTOs;
using Ecommerce.Core.src.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controller.src
{
    public class ProductController : BaseController<Product, ProductReadDTO, ProductCreateDTO, ProductUpdateDTO>
    {
        public ProductController(IProductService service) : base(service)
        {
        }
    }
}