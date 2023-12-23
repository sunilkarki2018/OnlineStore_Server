
using Ecommerce.Core.src.Entities;

namespace Ecommerce.Business.src.DTOs
{
    public class ProductLineReadDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
        public IEnumerable<ImageReadDTO> ImageReadDTOs { get; set; }
    }
    public class ProductLineCreateDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
        public List<ImageCreateDTO> ImageCreateDTOs { get; set; } = new List<ImageCreateDTO>();
    }
    public class ProductLineUpdateDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public Guid CategoryId { get; set; }
        //public Category Category { get; set; }
        //public List<ImageUpdateDTO> ImageUpdateDTOs { get; set; } = new List<ImageUpdateDTO>();
    }
}