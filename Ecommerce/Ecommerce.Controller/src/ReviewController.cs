using Ecommerce.Business.src.Abstractions;
using Ecommerce.Business.src.DTOs;
using Ecommerce.Core.src.Entities;
using Ecommerce.Core.src.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Controller.src
{
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;
        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }
        [Authorize(Roles = "Customer")]
        public async Task<ActionResult<ReviewReadDTO>> CreateReviewAsync([FromBody] ReviewCreateDTO createObject)
        {
            var authenticatedClaims = HttpContext.User;
            var userId = authenticatedClaims.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)!.Value;
            return CreatedAtAction(nameof(CreateReviewAsync), await _reviewService.CreateReviewAsync(new Guid(userId), createObject));
        }
        [HttpGet()]
        public virtual async Task<ActionResult<IEnumerable<ReviewReadDTO>>> GetAllReviewsAsync([FromQuery] GetAllOptions getAllOptions)
        {
            return Ok(await _reviewService.GetAllAsync(getAllOptions));
        }
        [AllowAnonymous]
        [HttpGet("{id:guid}")]
        public virtual async Task<ActionResult<ReviewReadDTO>> GetByIdAsync([FromRoute] Guid id)
        {
            return Ok(await _reviewService.GetByIdAsync(id));
        }
    }
}
