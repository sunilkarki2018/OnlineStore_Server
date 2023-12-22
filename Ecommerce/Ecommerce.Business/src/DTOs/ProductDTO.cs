using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Core.src.Entities;

namespace Ecommerce.Business.src.DTOs
{
    public class ProductReadDTO : BaseEntityDTO
    {
        public int Inventory { get; set; }
        public ProductLine ProductLine { get; set; } = new ProductLine();
        public ProductSize ProductSize { get; set; } = new ProductSize();
        public Guid ProductLineId { get; set; }
        public Guid ProductSizeId { get; set; }
    }
    public class ProductCreateDTO
    {
        public int Inventory { get; set; }
        public Guid ProductLineId { get; set; }
        public Guid ProductSizeId { get; set; }
    }
    public class ProductUpdateDTO : BaseEntityDTO
    {
        public int Inventory { get; set; }
        public Guid ProductLineId { get; set; }
        public Guid ProductSizeId { get; set; }
    }
}