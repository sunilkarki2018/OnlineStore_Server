using Ecommerce.Business.src.Abstractions;
using Ecommerce.Business.src.DTOs;
using Ecommerce.Core.src.Entities;
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
        [HttpPost("submit-product")]
        public new async Task<ActionResult<ProductReadDTO>> CreateProductAsync([FromForm] ProductForm productForm)
        {
            var productCreateDTO = new ProductCreateDTO();

            if (productForm.Images == null || productForm.Images.Count == 0)
            {
                throw new Exception("avatar is missing");
            }
            else
            {
                productCreateDTO.Title = productForm.Title;
                productCreateDTO.Price = productForm.Price;
                productCreateDTO.Quantity = productForm.Quantity;
                productCreateDTO.Description = productForm.Description;
                productCreateDTO.CategoryId = productForm.CategoryId;

                foreach (var item in productForm.Images)
                {
                    using (var ms = new MemoryStream())
                    {
                        await item.CopyToAsync(ms);
                        var content = ms.ToArray();
                        var productImage = new ImageCreateDTO { Data = content };
                        productCreateDTO.Images.Add(productImage);
                        //return BitConverter.ToString(content);
                    }
                }
            }
            return await _service.CreateOneAsync(productCreateDTO);
        }
    }

    public class ProductForm
    {
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public Guid CategoryId { get; set; }
        public List<IFormFile> Images { get; set; }
    }

}