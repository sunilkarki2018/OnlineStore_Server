
namespace Ecommerce.Business.src.DTOs
{
    public class ProductSizeCreateDTO
    {
       public string Value { get; set; }
    }
    public class ProductSizeReadDTO : BaseEntityDTO
    {
       public string Value { get; set; }
    }
    public class ProductSizeUpdateDTO : BaseEntityDTO
    {
         public Guid Id { get; set; }
         public string Value { get; set; }
    }
}