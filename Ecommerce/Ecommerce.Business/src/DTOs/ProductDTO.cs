using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Business.src.DTOs
{
    public class ProductReadDTO : BaseEntityDTO
    {
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public IEnumerable<ImageReadDTO> Images { get; set; }
    }
    public class ProductCreateDTO
    {
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public Guid CategoryId { get; set; }
        public List<ImageCreateDTO> Images { get; set; } = new List<ImageCreateDTO>();
    }
    public class ProductUpdateDTO : BaseEntityDTO
    {
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public Guid CategoryId { get; set; }
        //public IEnumerable<ImageUpdateDTO> Images { get; set; }
    }
}