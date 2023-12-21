using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Core.src.Entities
{
    public class Product : BaseEntity
    {
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
        public IEnumerable<Image> Images { get; set; }
        //public IEnumerable<ProductColor> Products { get; set; }
    }
    /* public class ProductColor : BaseEntity
     {
         public string Value { get; set; }
         public IEnumerable<Product> Products { get; set; }
     }*/
}