using Ecommerce.Business.src.Abstractions;
using Ecommerce.Business.src.DTOs;
using Ecommerce.Business.src.Shared;
using Ecommerce.Core.src.Entities;
using Ecommerce.Core.src.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace Ecommerce.Controller.src
{

    [ApiController]
    [Route("api/v1/[controller]s")]
    public class ProductLineController : ControllerBase
    {
        protected readonly IProductLineService _productLineService;
        public ProductLineController(IProductLineService productLineService)
        {
            _productLineService = productLineService; ;
        }
        [Authorize(Roles = "Admin")]
        [HttpPost("")]
        public new async Task<ActionResult<ProductLineReadDTO>> CreateProductLineAsync([FromForm] ProductLineCreateForm productLineCreateForm)
        {
            var productLineCreateDTO = new ProductLineCreateDTO();

            if (productLineCreateForm.Images == null || productLineCreateForm.Images.Count == 0)
            {
                throw CustomException.NotFoundException("avatar is missing");
            }
            else
            {
                productLineCreateDTO.Title = productLineCreateForm.Title;
                productLineCreateDTO.Price = productLineCreateForm.Price;
                productLineCreateDTO.Description = productLineCreateForm.Description;
                productLineCreateDTO.CategoryId = productLineCreateForm.CategoryId;

                foreach (var item in productLineCreateForm.Images)
                {
                    using (var ms = new MemoryStream())
                    {
                        await item.CopyToAsync(ms);
                        var content = ms.ToArray();
                        var productImage = new ImageCreateDTO { Data = content };
                        productLineCreateDTO.ImageCreateDTOs.Add(productImage);
                        //return BitConverter.ToString(content);
                    }
                }
            }
            return CreatedAtAction(nameof(CreateProductLineAsync), await _productLineService.CreateOneAsync(productLineCreateDTO));
        }

        [Authorize(Roles = "Admin")]
        [HttpPatch("{id:guid}")]
        public async Task<ActionResult<bool>> UpdateProductLineAsync([FromBody] ProductLineUpdateForm productLineUpdateForm)
        {
            var productLineUpdateDTO = new ProductLineUpdateDTO();

            productLineUpdateDTO.Id = productLineUpdateForm.Id;
            productLineUpdateDTO.Title = productLineUpdateForm.Title;
            productLineUpdateDTO.Price = productLineUpdateForm.Price;
            productLineUpdateDTO.Description = productLineUpdateForm.Description;
            productLineUpdateDTO.CategoryId = productLineUpdateForm.CategoryId;

            return Ok(await _productLineService.UpdateOneAsync(productLineUpdateForm.Id, productLineUpdateDTO));
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id:guid}")]
        public virtual async Task<ActionResult<bool>> DeleteOneAsync([FromRoute] Guid id)
        {
            return Ok(await _productLineService.DeleteOneAsync(id));
        }
        [HttpGet()]
        public virtual async Task<ActionResult<IEnumerable<ProductLineReadDTO>>> GetAllProductLinesAsync([FromQuery] GetAllOptions getAllOptions)
        {
            return Ok(await _productLineService.GetAllAsync(getAllOptions));
        }
        [AllowAnonymous]
        [HttpGet("{id:guid}")]
        public virtual async Task<ActionResult<ProductLineReadDTO>> GetProductLinesByIdAsync([FromRoute] Guid id)
        {
            return Ok(await _productLineService.GetByIdAsync(id));
        }

    }

    public class ProductLineCreateForm
    {
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public Guid CategoryId { get; set; }
        public List<IFormFile> Images { get; set; }
    }
    public class ProductLineUpdateForm
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public Guid CategoryId { get; set; }
        //public List<IFormFile> Images { get; set; }
    }

}