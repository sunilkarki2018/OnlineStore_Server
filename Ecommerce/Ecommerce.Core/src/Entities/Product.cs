using System.Text.Json.Serialization;

namespace Ecommerce.Core.src.Entities
{
    public class Product : BaseEntity
    {
        public int Inventory { get; set; }
        public ProductLine ProductLine { get; set; }
        public ProductSize ProductSize { get; set; }
        public Guid ProductLineId { get; set; }
        public Guid ProductSizeId { get; set; }
    }
    public class ProductSize : BaseEntity
    {
        public int Value { get; set; }
        [JsonIgnore]
        public IEnumerable<Product> Products { get; set; }
    }
}