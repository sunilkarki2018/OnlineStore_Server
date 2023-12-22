
namespace Ecommerce.Business.src.DTOs
{
    public class ProductSizeCreateDTO
    {
       public string Value { get; set; }
    }
    public class ProductSizeReadDTO
    {
       public string Value { get; set; }
    }
    public class ProductSizeUpdateDTO
    {
         public Guid Id { get; set; }
         public string Value { get; set; }
    }
}