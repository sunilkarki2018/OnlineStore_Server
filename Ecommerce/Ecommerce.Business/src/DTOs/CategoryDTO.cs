using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Business.src.DTOs
{
    public class CategoryDTO : BaseEntityDTO
    {
        public string Name { get; set; }
        public string Image { get; set; }
    }
    public class CategoryCreateDTO
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
    public class CategoryUpdateDTO : BaseEntityDTO
    {
        public string Name { get; set; }
        public string Image { get; set; }
    }
}