using Ecommerce.Business.src.Abstractions;
using Ecommerce.Business.src.DTOs;
using Ecommerce.Core.src.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controller.src
{
    public class ProductController : BaseController<Product, ProductReadDTO, ProductCreateDTO, ProductUpdateDTO>
    {
        protected readonly IProductService _service;
        public ProductController(IProductService service) : base(service)
        {
            _service = service;
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public override Task<ActionResult<ProductReadDTO>> CreateOneAsync([FromBody] ProductCreateDTO createObject)
        {
            return base.CreateOneAsync(createObject);
        }
        [Authorize(Roles = "Admin")]
        [HttpPatch("{id:guid}")]
        public override Task<ActionResult<bool>> UpdateOneAsync([FromRoute] Guid id, [FromBody] ProductUpdateDTO updateObject)
        {
            return base.UpdateOneAsync(id, updateObject);
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id:guid}")]
        public override Task<ActionResult<bool>> DeleteOneAsync([FromRoute] Guid id)
        {
            return base.DeleteOneAsync(id);
        }

    }


}