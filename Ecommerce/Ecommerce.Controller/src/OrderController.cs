using Ecommerce.Business.src.Abstractions;
using Ecommerce.Business.src.DTOs;
using Ecommerce.Core.src.Entities;
using Ecommerce.Core.src.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Controller.src
{
    public class OrderController : BaseController<Order, OrderReadDTO, OrderCreateDTO, OrderUpdateDTO>
    {
        protected readonly IOrderService _orderService;
        private readonly IAuthorizationService _authorizationService;
        public OrderController(IOrderService orderService, IAuthorizationService authorizationService) : base(orderService)
        {
            _orderService = orderService;
            _authorizationService = authorizationService;
        }
        [Authorize(Roles = "Customer")]
        public override async Task<ActionResult<OrderReadDTO>> CreateOneAsync([FromBody] OrderCreateDTO createObject)
        {
            var authenticatedClaims = HttpContext.User;
            var userId = authenticatedClaims.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)!.Value;
            return await _orderService.CreateOrderAsync(new Guid(userId), createObject);
        }
        [Authorize(Roles = "Admin")]
        public override async Task<ActionResult<bool>> UpdateOneAsync([FromRoute] Guid id, [FromBody] OrderUpdateDTO updateObject)
        {
            var order = await _orderService.GetByIdAsync(id);
            var authorizationResult = await _authorizationService.AuthorizeAsync(HttpContext.User, order, "AdminOrOwner");

            if (authorizationResult.Succeeded)
            {
                return await base.UpdateOneAsync(id, updateObject);
            }
            else if (User.Identity!.IsAuthenticated)
            {
                return new ForbidResult();
            }
            else
            {
                return new ChallengeResult();
            }


        }
        [Authorize(Roles = "Admin")]
        public override async Task<ActionResult<bool>> DeleteOneAsync([FromRoute] Guid id)
        {
            var order = await _orderService.GetByIdAsync(id);
            var authorizationResult = await _authorizationService.AuthorizeAsync(HttpContext.User, order, "AdminOrOwner");

            if (authorizationResult.Succeeded)
            {
                return await base.DeleteOneAsync(id);
            }
            else if (User.Identity!.IsAuthenticated)
            {
                return new ForbidResult();
            }
            else
            {
                return new ChallengeResult();
            }
        }

    }
}
