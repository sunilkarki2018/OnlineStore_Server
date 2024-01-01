using Ecommerce.Business.src.Abstractions;
using Ecommerce.Business.src.DTOs;
using Ecommerce.Business.src.Shared;
using Ecommerce.Core.src.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

        /*
        [Authorize(Roles = "Admin")]
        [HttpPost()]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult<ProductLineReadDTO>> CreateProductLineAsync([FromForm] ProductLineCreateForm productLineCreateForm)
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
                    }
                }
            }
            return CreatedAtAction(nameof(CreateProductLineAsync), await _productLineService.CreateOneAsync(productLineCreateDTO));
        }
*/




        [Authorize(Roles = "Admin")]
        [HttpPatch()]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult<bool>> UpdateProductLineAsync(Guid id, [FromForm] ProductLineUpdateForm productLineUpdateForm)
        {
            var productLineUpdateDTO = new ProductLineUpdateDTO();
            productLineUpdateDTO.Id = productLineUpdateForm.Id;
            productLineUpdateDTO.Title = productLineUpdateForm.Title;
            productLineUpdateDTO.Price = productLineUpdateForm.Price;
            productLineUpdateDTO.Description = productLineUpdateForm.Description;
            productLineUpdateDTO.CategoryId = productLineUpdateForm.CategoryId;

            if (productLineUpdateForm.Images != null || productLineUpdateForm.Images.Count != 0)
            {
                foreach (var item in productLineUpdateForm.Images)
                {
                    using (var ms = new MemoryStream())
                    {
                        await item.CopyToAsync(ms);
                        var content = ms.ToArray();
                        var productImage = new ImageUpdateDTO { Data = content, ProductLineId = id };
                        productLineUpdateDTO.Images.Add(productImage);
                    }
                }
            }
            return CreatedAtAction(nameof(UpdateProductLineAsync), await _productLineService.UpdateOneAsync(productLineUpdateDTO.Id, productLineUpdateDTO));
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
            var productLineReadDTOs = await _productLineService.GetAllAsync(getAllOptions);
            var result = ConvertImageDataToBase64(productLineReadDTOs.ToList());
            return Ok(result);
        }

        private List<ProductLineReadDTO> ConvertImageDataToBase64(List<ProductLineReadDTO> productLines)
        {
            foreach (var productLine in productLines)
            {
                if (productLine.Images != null)
                {
                    foreach (var imageReadDto in productLine.Images)
                    {
                        if (imageReadDto.Data != null)
                        {
                            imageReadDto.ImgBase64Data = Convert.ToBase64String(imageReadDto.Data);
                        }
                    }
                }
            }
            return productLines;
        }

        [HttpGet("{id:guid}")]
        public virtual async Task<ActionResult<ProductLineReadDTO>> GetProductLinesByIdAsync([FromRoute] Guid id)
        {
            var productLineDTO = await _productLineService.GetByIdAsync(id);
            foreach (var imageReadDto in productLineDTO.Images)
            {
                if (imageReadDto.Data != null)
                {
                    imageReadDto.ImgBase64Data = Convert.ToBase64String(imageReadDto.Data);
                }
            }
            return Ok(productLineDTO);
        }
    }

    public class ProductLineCreateForm
    {
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public Guid CategoryId { get; set; }
        public List<IFormFile>? Images { get; set; } = new List<IFormFile>();
    }
    public class ProductLineUpdateForm
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public Guid CategoryId { get; set; }
        public List<IFormFile>? Images { get; set; } = new List<IFormFile>();
    }

}