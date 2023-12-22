using Ecommerce.Core.src.Entities;

namespace Ecommerce.WebAPI.src.Database
{
    public class SeedingData
    {
        private static Guid productSize1 = Guid.NewGuid();
        private static Guid productSize2 = Guid.NewGuid();
        private static Guid productSize3 = Guid.NewGuid();
        private static Guid productSize4 = Guid.NewGuid();

        private static ProductSize size1 = new ProductSize { Id = productSize1, Value = 32 };
        private static ProductSize size2 = new ProductSize { Id = productSize2, Value = 34 };
        private static ProductSize size3 = new ProductSize { Id = productSize3, Value = 36 };
        private static ProductSize size4 = new ProductSize { Id = productSize4, Value = 38 };

        private static Guid categoryGuid1 = Guid.NewGuid();
        private static Guid categoryGuid2 = Guid.NewGuid();
        private static Guid categoryGuid3 = Guid.NewGuid();
        private static Guid categoryGuid4 = Guid.NewGuid();
        private static Guid categoryGuid5 = Guid.NewGuid();

        private static Category category1 = new Category
        {
            Id = categoryGuid1,
            Name = "T-Shirts",
            Image = "tshirts.jpg"
        };
        private static Category category2 = new Category
        {
            Id = categoryGuid2,
            Name = "Jeans",
            Image = "jeans.jpg"
        };
        private static Category category3 = new Category
        {
            Id = categoryGuid3,
            Name = "Dresses",
            Image = "dresses.jpg"
        };
        private static Category category4 = new Category
        {
            Id = categoryGuid4,
            Name = "Footwear",
            Image = "footwear.jpg"
        };
        private static Category category5 = new Category
        {
            Id = categoryGuid5,
            Name = "Accessories",
            Image = "accessories.jpg"
        };

        private static Guid productLineGuid1 = Guid.NewGuid();
        private static Guid productLineGuid2 = Guid.NewGuid();
        private static Guid productLineGuid3 = Guid.NewGuid();
        private static Guid productLineGuid4 = Guid.NewGuid();
        private static Guid productLineGuid5 = Guid.NewGuid();

        private static ProductLine productLine1 = new ProductLine
        {
            Id = productLineGuid1,
            Title = "Cotton T-Shirt",
            Description = "A comfortable and stylish cotton T-shirt.",
            Price = 19.99m,
            CategoryId = categoryGuid1,
            //Category = category1,
            //Images = new List<Image> { image1, image2 }
        };
        private static ProductLine productLine2 = new ProductLine
        {
            Id = productLineGuid2,
            Title = "Slim Fit Jeans",
            Description = "Slim fit jeans for a modern look.",
            Price = 49.99m,
            CategoryId = categoryGuid2,
            //Category = category2,
            //Images = new List<Image> { image2, image3 }
        };
        private static ProductLine productLine3 = new ProductLine
        {
            Id = productLineGuid3,
            Title = "Summer Dress",
            Description = "A floral summer dress for any occasion.",
            Price = 29.99m,
            CategoryId = categoryGuid3,
            //Category = category3,
            //Images = new List<Image> { image3, image4 }
        };
        private static ProductLine productLine4 = new ProductLine
        {
            Id = productLineGuid4,
            Title = "Sneakers",
            Description = "Casual sneakers for everyday wear.",
            Price = 39.99m,
            CategoryId = categoryGuid4,
            //Category = category4,
            //Images = new List<Image> { image4, image1 }
        };
        private static ProductLine productLine5 = new ProductLine
        {
            Id = productLineGuid5,
            Title = "Leather Belt",
            Description = "Classic leather belt to complete your look.",
            Price = 14.99m,
            CategoryId = categoryGuid5,
            //Category = category5,
            //Images = new List<Image> { image1, image2 }
        };

        private static Image image1 = new Image { Id = Guid.NewGuid(), ProductLineId = productLineGuid1, Data = GenerateRandomImageData(10, 10) };
        private static Image image2 = new Image { Id = Guid.NewGuid(), ProductLineId = productLineGuid2, Data = GenerateRandomImageData(10, 10) };
        private static Image image3 = new Image { Id = Guid.NewGuid(), ProductLineId = productLineGuid3, Data = GenerateRandomImageData(10, 10) };
        private static Image image4 = new Image { Id = Guid.NewGuid(), ProductLineId = productLineGuid4, Data = GenerateRandomImageData(10, 10) };
        private static Image image5 = new Image { Id = Guid.NewGuid(), ProductLineId = productLineGuid1, Data = GenerateRandomImageData(10, 10) };
        private static Image image6 = new Image { Id = Guid.NewGuid(), ProductLineId = productLineGuid2, Data = GenerateRandomImageData(10, 10) };
        private static Image image7 = new Image { Id = Guid.NewGuid(), ProductLineId = productLineGuid3, Data = GenerateRandomImageData(10, 10) };
        private static Image image8 = new Image { Id = Guid.NewGuid(), ProductLineId = productLineGuid4, Data = GenerateRandomImageData(10, 10) };

        private static Product product1 = new Product
        {
            Id = Guid.NewGuid(),
            Inventory = 100,
            ProductLineId = productLineGuid1,
            ProductSizeId = productSize1
        };
        private static Product product2 = new Product
        {
            Id = Guid.NewGuid(),
            Inventory = 200,
            ProductLineId = productLineGuid1,
            ProductSizeId = productSize2
        };
        private static Product product3 = new Product
        {
            Id = Guid.NewGuid(),
            Inventory = 200,
            ProductLineId = productLineGuid1,
            ProductSizeId = productSize3
        };
        private static Product product4 = new Product
        {
            Id = Guid.NewGuid(),
            Inventory = 100,
            ProductLineId = productLineGuid1,
            ProductSizeId = productSize4
        };

        private static Product product5 = new Product
        {
            Id = Guid.NewGuid(),
            Inventory = 100,
            ProductLineId = productLineGuid2,
            ProductSizeId = productSize1
        };
        private static Product product6 = new Product
        {
            Id = Guid.NewGuid(),
            Inventory = 200,
            ProductLineId = productLineGuid2,
            ProductSizeId = productSize2
        };
        private static Product product7 = new Product
        {
            Id = Guid.NewGuid(),
            Inventory = 200,
            ProductLineId = productLineGuid2,
            ProductSizeId = productSize3
        };
        private static Product product8 = new Product
        {
            Id = Guid.NewGuid(),
            Inventory = 100,
            ProductLineId = productLineGuid2,
            ProductSizeId = productSize4
        };

        private static Product product9 = new Product
        {
            Id = Guid.NewGuid(),
            Inventory = 100,
            ProductLineId = productLineGuid3,
            ProductSizeId = productSize1
        };
        private static Product product10 = new Product
        {
            Id = Guid.NewGuid(),
            Inventory = 200,
            ProductLineId = productLineGuid3,
            ProductSizeId = productSize2
        };
        private static Product product11 = new Product
        {
            Id = Guid.NewGuid(),
            Inventory = 200,
            ProductLineId = productLineGuid3,
            ProductSizeId = productSize3
        };
        private static Product product12 = new Product
        {
            Id = Guid.NewGuid(),
            Inventory = 100,
            ProductLineId = productLineGuid3,
            ProductSizeId = productSize4
        };

        private static Product product13 = new Product
        {
            Id = Guid.NewGuid(),
            Inventory = 100,
            ProductLineId = productLineGuid4,
            ProductSizeId = productSize1
        };
        private static Product product14 = new Product
        {
            Id = Guid.NewGuid(),
            Inventory = 200,
            ProductLineId = productLineGuid4,
            ProductSizeId = productSize2
        };
        private static Product product15 = new Product
        {
            Id = Guid.NewGuid(),
            Inventory = 200,
            ProductLineId = productLineGuid4,
            ProductSizeId = productSize3
        };
        private static Product product16 = new Product
        {
            Id = Guid.NewGuid(),
            Inventory = 100,
            ProductLineId = productLineGuid4,
            ProductSizeId = productSize4
        };

        private static Product product17 = new Product
        {
            Id = Guid.NewGuid(),
            Inventory = 100,
            ProductLineId = productLineGuid4,
            ProductSizeId = productSize1
        };
        private static Product product18 = new Product
        {
            Id = Guid.NewGuid(),
            Inventory = 200,
            ProductLineId = productLineGuid4,
            ProductSizeId = productSize2
        };
        private static Product product19 = new Product
        {
            Id = Guid.NewGuid(),
            Inventory = 200,
            ProductLineId = productLineGuid4,
            ProductSizeId = productSize3
        };
        private static Product product20 = new Product
        {
            Id = Guid.NewGuid(),
            Inventory = 100,
            ProductLineId = productLineGuid4,
            ProductSizeId = productSize4
        };



        private static byte[] GenerateRandomImageData(int width, int height)
        {
            Random random = new Random();
            byte[] imageData = new byte[width * height * 3];
            for (int i = 0; i < imageData.Length; i++)
            {
                imageData[i] = (byte)random.Next(256); // Generating random byte values (0 to 255)
            }
            return imageData;
        }

        public static List<Category> GetCategories()
        {
            return new List<Category>() { category1, category2, category3, category4, category5 };
        }

        public static List<ProductSize> GetProductSizes()
        {
            return new List<ProductSize>() { size1, size2, size3, size4 };
        }


        public static List<Image> GetImages()
        {
            return new List<Image>() { image1, image2, image3, image4, image5, image6, image7, image8 };
        }

        public static List<ProductLine> GetProductLines()
        {
            return new List<ProductLine>() { productLine1, productLine2, productLine3, productLine4, productLine5 };
        }

        public static List<Product> GetProducts()
        {
            return new List<Product>() { product1, product2, product3, product4, product5, product6, product7, product8, product9, product10, product11, product12, product13, product14, product15, product16, product17, product18, product19, product20 };
        }

    }
}
