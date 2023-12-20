using Ecommerce.Business.src.Abstractions;
using Ecommerce.Business.src.DTOs;
using Ecommerce.Core.src.Entities;
using Ecommerce.Core.src.Shared;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Controller.src
{
    public class OrderController : BaseController<Order, OrderReadDTO, OrderCreateDTO, OrderUpdateDTO>
    {
        protected readonly IOrderService _service;
        public OrderController(IOrderService service) : base(service)
        {
        }
    }
}
