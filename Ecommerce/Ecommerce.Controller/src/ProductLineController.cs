using Ecommerce.Business.src.Abstractions;
using Ecommerce.Business.src.DTOs;
using Ecommerce.Core.src.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controller.src
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductLineController : BaseController<ProductLine, ProductLineReadDTO, ProductLineCreateDTO, ProductLineUpdateDTO>
    {
        protected readonly IProductLineService _productLineService;
        public ProductLineController(IProductLineService productLineService) : base(productLineService)
        {
            _productLineService = productLineService; ;
        }
        [Authorize(Roles = "Admin")]
        [HttpPost("create-product-line")]
        public new async Task<ActionResult<ProductLineReadDTO>> CreateProductLineAsync([FromForm] ProductLineForm productLineForm)
        {
            var productLineCreateDTO = new ProductLineCreateDTO();

            if (productLineForm.Images == null || productLineForm.Images.Count == 0)
            {
                throw new Exception("avatar is missing");
            }
            else
            {
                productLineCreateDTO.Title = productLineForm.Title;
                productLineCreateDTO.Price = productLineForm.Price;
                productLineCreateDTO.Description = productLineForm.Description;
                productLineCreateDTO.CategoryId = productLineForm.CategoryId;

                foreach (var item in productLineForm.Images)
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
            return await _productLineService.CreateOneAsync(productLineCreateDTO);
        }
    }

    public class ProductLineForm
    {
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public Guid CategoryId { get; set; }
        public List<IFormFile> Images { get; set; }
    }

}