using System;
using Ecommerce.Core.src.Entities;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Ecommerce.WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class Create : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:order_status", "registered,delivered,canceled,pending")
                .Annotation("Npgsql:Enum:role", "admin,customer");

            migrationBuilder.CreateTable(
                name: "categories",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    image = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_categories", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "product_size",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    value = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_product_size", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    first_name = table.Column<string>(type: "text", nullable: false),
                    last_name = table.Column<string>(type: "text", nullable: false),
                    email = table.Column<string>(type: "text", nullable: false),
                    password = table.Column<string>(type: "text", nullable: false),
                    role = table.Column<Role>(type: "role", nullable: false),
                    salt = table.Column<byte[]>(type: "bytea", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "product_line",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    title = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    price = table.Column<decimal>(type: "numeric", nullable: false),
                    category_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_product_line", x => x.id);
                    table.ForeignKey(
                        name: "fk_product_line_categories_category_id",
                        column: x => x.category_id,
                        principalTable: "categories",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "address",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    house_number = table.Column<string>(type: "text", nullable: false),
                    street = table.Column<string>(type: "text", nullable: false),
                    post_code = table.Column<string>(type: "text", nullable: false),
                    city = table.Column<string>(type: "text", nullable: false),
                    country = table.Column<string>(type: "text", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_address", x => x.id);
                    table.ForeignKey(
                        name: "fk_address_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "avatar",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    data = table.Column<byte[]>(type: "bytea", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_avatar", x => x.id);
                    table.ForeignKey(
                        name: "fk_avatar_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "orders",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    order_status = table.Column<OrderStatus>(type: "order_status", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_orders", x => x.id);
                    table.ForeignKey(
                        name: "fk_orders_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "image",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    product_line_id = table.Column<Guid>(type: "uuid", nullable: false),
                    data = table.Column<byte[]>(type: "bytea", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_image", x => x.id);
                    table.ForeignKey(
                        name: "fk_image_product_line_product_line_id",
                        column: x => x.product_line_id,
                        principalTable: "product_line",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "products",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    inventory = table.Column<int>(type: "integer", nullable: false),
                    product_line_id = table.Column<Guid>(type: "uuid", nullable: false),
                    product_size_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_products", x => x.id);
                    table.ForeignKey(
                        name: "fk_products_product_line_product_line_id",
                        column: x => x.product_line_id,
                        principalTable: "product_line",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_products_product_size_product_size_id",
                        column: x => x.product_size_id,
                        principalTable: "product_size",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "order_items",
                columns: table => new
                {
                    order_id = table.Column<Guid>(type: "uuid", nullable: false),
                    product_id = table.Column<Guid>(type: "uuid", nullable: false),
                    price = table.Column<decimal>(type: "numeric", nullable: false),
                    quantity = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_order_items", x => new { x.product_id, x.order_id });
                    table.ForeignKey(
                        name: "fk_order_items_orders_order_id",
                        column: x => x.order_id,
                        principalTable: "orders",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_order_items_products_product_id",
                        column: x => x.product_id,
                        principalTable: "products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "reviews",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    rating = table.Column<int>(type: "integer", nullable: false),
                    comment = table.Column<string>(type: "text", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    product_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_reviews", x => x.id);
                    table.ForeignKey(
                        name: "fk_reviews_products_product_id",
                        column: x => x.product_id,
                        principalTable: "products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_reviews_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "categories",
                columns: new[] { "id", "created_at", "image", "name", "updated_at" },
                values: new object[,]
                {
                    { new Guid("3be32f4e-493d-4ea8-ac87-894133202e63"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "dresses.jpg", "Dresses", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("5f59ad2c-913a-4741-9fb4-b11f5e0316e2"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "accessories.jpg", "Accessories", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("680f59c7-1ba6-469d-a9fd-71453fefb2e5"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "jeans.jpg", "Jeans", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("75b7846e-18d6-4493-a528-80c5283cee98"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "tshirts.jpg", "T-Shirts", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("e187928a-e076-401e-a58d-d314c2830e6a"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "footwear.jpg", "Footwear", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "product_size",
                columns: new[] { "id", "created_at", "updated_at", "value" },
                values: new object[,]
                {
                    { new Guid("1aba3111-0d53-4789-8dff-c3dac5bd9b12"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 36 },
                    { new Guid("38b52562-538f-47ce-ae57-846e7d30be3f"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 34 },
                    { new Guid("d6c24717-7e2c-4490-949a-79e5870e0a12"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 38 },
                    { new Guid("d9f06780-c8db-4b1f-ad86-09b923c3722f"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 32 }
                });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id", "created_at", "email", "first_name", "last_name", "password", "role", "salt", "updated_at" },
                values: new object[,]
                {
                    { new Guid("81a7fb9b-4377-4fe2-a1df-a51eb705f0ef"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@mail.com", "JohnAdmin", "Doe", "49d3ef32bc4a4116033fb6479b2d28f05c37f16c8617c83663fe4439f6b46877", Role.Admin, new byte[] { 222, 238, 105, 1, 210, 35, 10, 72, 143, 56, 168, 46, 73, 37, 248, 218, 141, 40, 33, 72, 64, 106, 109, 195, 108, 10, 169, 125, 133, 157, 90, 172, 173, 78, 29, 239, 193, 13, 241, 180, 228, 100, 123, 156, 96, 41, 115, 22, 52, 173, 96, 148, 93, 100, 242, 253, 118, 201, 101, 223, 219, 236, 4, 55 }, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("fcd852f8-3750-47d4-a543-424586098dae"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "customer@mail.com", "Jane", "Doe", "3ba3bbf6fdef9042364db1de1ad14947b7411217422291d12cd2a5e6482e0883", Role.Customer, new byte[] { 108, 50, 170, 57, 161, 242, 136, 177, 179, 42, 136, 136, 64, 97, 113, 188, 86, 178, 195, 112, 13, 110, 117, 19, 11, 226, 219, 158, 43, 45, 69, 9, 98, 0, 117, 62, 201, 96, 149, 48, 158, 131, 226, 37, 134, 152, 215, 241, 48, 78, 206, 145, 101, 119, 189, 90, 89, 94, 35, 101, 222, 184, 192, 116 }, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "address",
                columns: new[] { "id", "city", "country", "created_at", "house_number", "post_code", "street", "updated_at", "user_id" },
                values: new object[,]
                {
                    { new Guid("6a5201f0-d3d5-4182-ad86-e8e336c69ce4"), "Townsville", "Countryland", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "456", "67890", "Oak St", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("fcd852f8-3750-47d4-a543-424586098dae") },
                    { new Guid("e40c4866-194a-4350-bb41-bcbde62d08d3"), "Cityville", "Countryland", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "123", "12345", "Main St", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("81a7fb9b-4377-4fe2-a1df-a51eb705f0ef") }
                });

            migrationBuilder.InsertData(
                table: "avatar",
                columns: new[] { "id", "created_at", "data", "updated_at", "user_id" },
                values: new object[,]
                {
                    { new Guid("c7e328dc-c647-4f3c-b2e3-e7389ee17e40"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new byte[] { 4, 5, 6 }, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("81a7fb9b-4377-4fe2-a1df-a51eb705f0ef") },
                    { new Guid("f682dd07-b308-4cf3-978e-c11501382158"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new byte[] { 10, 11, 12 }, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("fcd852f8-3750-47d4-a543-424586098dae") }
                });

            migrationBuilder.InsertData(
                table: "product_line",
                columns: new[] { "id", "category_id", "created_at", "description", "price", "title", "updated_at" },
                values: new object[,]
                {
                    { new Guid("5b2cc5f5-474a-48a5-a546-dd9fb73c8d09"), new Guid("75b7846e-18d6-4493-a528-80c5283cee98"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "A comfortable and stylish cotton T-shirt.", 19.99m, "Cotton T-Shirt", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("692f3a64-2715-4757-901c-fd52e6bb5be6"), new Guid("680f59c7-1ba6-469d-a9fd-71453fefb2e5"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Slim fit jeans for a modern look.", 49.99m, "Slim Fit Jeans", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("c9e01bc8-8637-4884-954c-7d894ebe01e8"), new Guid("5f59ad2c-913a-4741-9fb4-b11f5e0316e2"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Classic leather belt to complete your look.", 14.99m, "Leather Belt", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("e0c385e8-5a48-4ad5-8e69-9c5aa1923481"), new Guid("3be32f4e-493d-4ea8-ac87-894133202e63"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "A floral summer dress for any occasion.", 29.99m, "Summer Dress", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("efb7215e-b0b4-4670-8e4e-1147856f09a0"), new Guid("e187928a-e076-401e-a58d-d314c2830e6a"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Casual sneakers for everyday wear.", 39.99m, "Sneakers", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "image",
                columns: new[] { "id", "created_at", "data", "product_line_id", "updated_at" },
                values: new object[,]
                {
                    { new Guid("41c25a13-d82e-48d1-9928-4f25872e9c6e"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new byte[] { 46, 56, 181, 174, 135, 204, 100, 239, 197, 244, 254, 225, 39, 219, 203, 212, 79, 155, 253, 148, 57, 166, 79, 16, 174, 160, 245, 84, 175, 78, 71, 209, 233, 21, 76, 119, 186, 100, 123, 107, 124, 0, 35, 89, 78, 251, 45, 148, 6, 166, 194, 37, 100, 193, 95, 67, 197, 137, 22, 2, 137, 176, 30, 111, 201, 24, 211, 93, 44, 209, 193, 203, 120, 6, 132, 148, 165, 43, 119, 218, 232, 230, 228, 23, 49, 30, 103, 226, 187, 149, 21, 74, 236, 250, 202, 23, 244, 45, 180, 67, 255, 183, 123, 20, 219, 59, 83, 162, 16, 74, 55, 171, 22, 210, 56, 63, 131, 73, 154, 208, 137, 234, 122, 132, 17, 49, 169, 236, 93, 46, 70, 26, 197, 29, 153, 161, 144, 76, 27, 14, 73, 62, 231, 197, 21, 176, 177, 18, 212, 0, 221, 186, 227, 119, 43, 169, 135, 247, 54, 237, 247, 112, 210, 196, 152, 187, 49, 178, 141, 205, 28, 103, 57, 23, 25, 211, 66, 123, 161, 92, 40, 120, 75, 62, 75, 240, 186, 204, 152, 202, 74, 3, 138, 65, 239, 177, 61, 149, 246, 67, 222, 130, 208, 233, 103, 111, 212, 200, 127, 221, 79, 182, 200, 94, 200, 61, 82, 195, 154, 58, 109, 167, 113, 185, 244, 48, 242, 41, 34, 40, 24, 112, 82, 163, 45, 26, 48, 197, 143, 74, 140, 110, 135, 107, 247, 10, 65, 171, 253, 113, 131, 119, 129, 165, 238, 175, 55, 127, 48, 125, 58, 49, 176, 222, 60, 142, 255, 43, 13, 174, 231, 5, 97, 161, 181, 214, 107, 207, 101, 187, 82, 110, 191, 217, 3, 45, 35, 243, 6, 15, 247, 188, 233, 192, 142, 245, 64, 14, 36, 87 }, new Guid("e0c385e8-5a48-4ad5-8e69-9c5aa1923481"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("8b848ee7-1c7e-4d99-b777-05870abd9e1c"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new byte[] { 3, 29, 40, 46, 89, 185, 158, 229, 116, 242, 152, 173, 11, 107, 8, 252, 152, 45, 171, 226, 16, 31, 1, 11, 30, 39, 86, 137, 191, 179, 216, 132, 247, 34, 95, 84, 16, 80, 236, 122, 211, 118, 88, 46, 55, 234, 228, 9, 33, 66, 29, 18, 186, 196, 128, 236, 124, 35, 211, 233, 235, 221, 92, 3, 104, 90, 198, 200, 102, 10, 0, 220, 96, 26, 90, 157, 185, 156, 194, 102, 205, 190, 0, 118, 65, 228, 167, 42, 21, 86, 75, 69, 16, 136, 58, 204, 99, 176, 170, 57, 65, 249, 157, 198, 194, 127, 177, 72, 183, 1, 62, 190, 83, 214, 52, 221, 138, 171, 253, 202, 64, 89, 216, 5, 197, 28, 144, 52, 178, 204, 67, 183, 209, 215, 97, 3, 135, 221, 186, 51, 146, 146, 122, 88, 164, 143, 50, 94, 152, 57, 20, 186, 151, 91, 238, 22, 181, 195, 71, 70, 70, 166, 44, 116, 4, 33, 185, 19, 42, 149, 48, 255, 223, 246, 202, 49, 134, 64, 199, 147, 40, 206, 60, 30, 235, 35, 190, 92, 82, 34, 157, 174, 129, 147, 127, 212, 234, 95, 5, 61, 77, 241, 126, 219, 144, 162, 127, 60, 206, 33, 71, 1, 6, 149, 196, 232, 65, 160, 82, 191, 230, 34, 210, 127, 166, 162, 214, 140, 90, 131, 189, 231, 67, 54, 237, 176, 213, 197, 59, 56, 73, 16, 85, 230, 188, 115, 219, 52, 200, 16, 45, 22, 133, 223, 254, 201, 72, 10, 255, 65, 107, 224, 60, 173, 23, 22, 51, 254, 253, 42, 179, 68, 116, 94, 208, 59, 50, 90, 84, 165, 153, 103, 232, 154, 241, 229, 76, 249, 179, 35, 71, 183, 204, 40, 189, 158, 173, 5, 217, 43 }, new Guid("5b2cc5f5-474a-48a5-a546-dd9fb73c8d09"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("94355b3e-101d-493d-9ca9-b3c0e199e5ca"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new byte[] { 39, 25, 254, 179, 84, 214, 16, 55, 149, 225, 47, 237, 157, 75, 136, 135, 186, 90, 211, 88, 186, 249, 7, 31, 117, 144, 225, 78, 224, 113, 137, 185, 83, 127, 100, 62, 240, 241, 175, 184, 84, 21, 127, 153, 67, 209, 63, 149, 157, 78, 44, 242, 221, 124, 196, 26, 51, 100, 78, 209, 47, 142, 241, 175, 177, 116, 10, 46, 219, 89, 33, 163, 125, 209, 57, 184, 146, 84, 196, 238, 134, 252, 219, 164, 228, 159, 96, 103, 14, 209, 228, 243, 16, 34, 180, 255, 116, 74, 181, 143, 172, 207, 66, 67, 110, 58, 93, 182, 132, 0, 96, 47, 237, 4, 16, 9, 52, 155, 62, 90, 102, 3, 207, 45, 199, 162, 252, 246, 71, 204, 95, 117, 33, 231, 87, 75, 237, 100, 137, 194, 243, 24, 28, 180, 159, 177, 11, 107, 139, 57, 228, 28, 201, 235, 34, 58, 107, 132, 53, 61, 161, 219, 43, 116, 231, 182, 38, 146, 17, 183, 201, 58, 155, 150, 69, 67, 192, 116, 193, 93, 169, 221, 87, 179, 235, 93, 120, 218, 47, 20, 27, 173, 214, 69, 8, 33, 44, 126, 54, 221, 78, 239, 209, 54, 45, 125, 165, 108, 111, 10, 196, 147, 72, 247, 221, 25, 81, 94, 79, 122, 225, 241, 93, 203, 61, 221, 187, 125, 21, 130, 143, 153, 138, 22, 12, 185, 6, 88, 222, 217, 242, 181, 105, 123, 213, 187, 32, 174, 186, 184, 165, 74, 218, 139, 43, 153, 109, 144, 9, 233, 135, 72, 238, 138, 48, 34, 77, 130, 203, 173, 14, 30, 101, 74, 28, 169, 87, 35, 158, 92, 96, 65, 1, 88, 100, 168, 93, 139, 133, 130, 94, 230, 189, 20, 83, 25, 179, 91, 135, 130 }, new Guid("efb7215e-b0b4-4670-8e4e-1147856f09a0"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("9ac13319-6ad4-45e2-b4b2-07f2be0780ff"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new byte[] { 40, 245, 37, 238, 229, 249, 5, 102, 26, 250, 182, 116, 2, 91, 9, 48, 58, 9, 134, 82, 72, 93, 176, 135, 248, 198, 242, 109, 226, 164, 61, 211, 158, 46, 155, 237, 73, 169, 149, 224, 211, 230, 56, 57, 12, 255, 157, 61, 53, 7, 118, 86, 234, 128, 15, 133, 200, 132, 54, 25, 181, 147, 116, 145, 69, 96, 154, 44, 253, 12, 119, 38, 112, 84, 199, 11, 83, 95, 71, 23, 21, 38, 85, 202, 149, 161, 236, 113, 19, 184, 7, 248, 179, 150, 86, 239, 206, 178, 58, 179, 2, 184, 101, 12, 124, 203, 196, 161, 152, 70, 8, 249, 238, 144, 79, 154, 199, 206, 93, 167, 197, 61, 85, 24, 45, 165, 227, 237, 142, 5, 17, 154, 219, 51, 236, 140, 69, 180, 226, 217, 60, 9, 82, 96, 18, 83, 59, 142, 76, 99, 100, 16, 168, 179, 56, 112, 104, 152, 195, 87, 103, 203, 80, 239, 163, 169, 183, 113, 157, 91, 81, 88, 17, 70, 254, 209, 47, 230, 229, 101, 14, 1, 154, 255, 203, 56, 90, 145, 177, 115, 251, 0, 225, 176, 84, 77, 127, 95, 20, 153, 214, 101, 41, 79, 200, 3, 144, 253, 9, 192, 122, 203, 1, 129, 120, 64, 86, 241, 115, 252, 238, 107, 189, 84, 121, 174, 76, 108, 12, 183, 7, 226, 116, 239, 12, 65, 174, 95, 44, 102, 198, 136, 57, 121, 183, 216, 253, 136, 144, 123, 195, 100, 48, 172, 209, 122, 175, 105, 197, 172, 242, 127, 71, 250, 15, 83, 83, 179, 73, 161, 134, 246, 52, 243, 207, 108, 27, 90, 36, 215, 132, 76, 62, 20, 218, 149, 248, 13, 211, 78, 49, 89, 252, 246, 90, 99, 138, 164, 48, 131 }, new Guid("efb7215e-b0b4-4670-8e4e-1147856f09a0"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("aa9bbbd5-99d7-4a1f-93a5-ddc2be8c370c"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new byte[] { 15, 176, 134, 253, 18, 212, 74, 17, 128, 112, 96, 237, 79, 69, 3, 217, 13, 67, 230, 109, 46, 132, 5, 69, 247, 146, 127, 176, 78, 212, 190, 139, 50, 78, 186, 7, 79, 15, 70, 221, 84, 80, 199, 136, 226, 245, 170, 247, 101, 254, 104, 76, 9, 202, 189, 103, 56, 32, 168, 132, 71, 123, 102, 249, 244, 248, 66, 45, 223, 50, 230, 215, 66, 238, 138, 171, 37, 252, 162, 122, 153, 84, 36, 143, 232, 222, 165, 119, 55, 76, 10, 74, 163, 206, 251, 27, 183, 17, 36, 141, 245, 153, 43, 95, 98, 159, 123, 86, 18, 25, 151, 226, 142, 103, 151, 50, 135, 142, 140, 209, 243, 79, 64, 12, 11, 152, 19, 200, 129, 84, 181, 31, 114, 112, 192, 235, 235, 240, 132, 123, 135, 57, 13, 3, 97, 40, 155, 2, 179, 129, 5, 61, 35, 106, 71, 49, 142, 120, 223, 187, 250, 224, 6, 38, 162, 35, 80, 39, 80, 84, 255, 221, 107, 222, 239, 87, 30, 192, 138, 71, 80, 159, 177, 92, 134, 8, 205, 84, 207, 235, 21, 196, 211, 19, 151, 4, 140, 84, 78, 78, 46, 43, 101, 168, 210, 14, 221, 102, 43, 46, 167, 161, 32, 190, 131, 107, 201, 149, 184, 40, 33, 58, 233, 231, 198, 15, 58, 55, 17, 100, 138, 153, 223, 74, 57, 78, 254, 84, 106, 13, 174, 244, 52, 82, 238, 87, 2, 108, 221, 150, 85, 170, 243, 236, 133, 103, 39, 22, 208, 67, 175, 16, 46, 31, 181, 175, 9, 94, 188, 68, 24, 233, 125, 70, 22, 128, 18, 162, 193, 126, 197, 180, 42, 131, 117, 101, 13, 68, 234, 237, 220, 238, 77, 250, 15, 126, 198, 65, 237, 121 }, new Guid("5b2cc5f5-474a-48a5-a546-dd9fb73c8d09"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("ba73c839-3104-43c2-8a81-4dbf16633916"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new byte[] { 99, 126, 252, 211, 137, 88, 195, 115, 205, 214, 245, 122, 232, 235, 241, 12, 51, 191, 44, 168, 8, 205, 5, 161, 226, 184, 231, 215, 228, 154, 194, 128, 47, 47, 90, 137, 160, 251, 91, 200, 240, 170, 220, 33, 134, 111, 222, 113, 45, 131, 20, 144, 80, 55, 148, 73, 204, 203, 81, 86, 243, 214, 19, 178, 165, 199, 128, 18, 11, 106, 206, 191, 39, 248, 8, 94, 235, 12, 82, 165, 51, 129, 13, 217, 132, 215, 165, 46, 54, 58, 144, 62, 153, 16, 80, 5, 233, 234, 64, 174, 68, 197, 190, 7, 23, 179, 136, 194, 71, 142, 0, 15, 49, 131, 149, 27, 8, 207, 190, 145, 108, 139, 255, 215, 16, 121, 74, 1, 219, 105, 13, 234, 153, 188, 27, 208, 67, 96, 199, 70, 218, 172, 110, 65, 168, 137, 78, 96, 219, 7, 164, 22, 33, 19, 18, 231, 77, 161, 252, 189, 206, 155, 160, 114, 2, 119, 201, 34, 187, 111, 223, 184, 17, 70, 168, 60, 174, 173, 32, 55, 202, 57, 247, 150, 214, 172, 185, 147, 44, 147, 53, 144, 76, 70, 203, 85, 92, 116, 203, 37, 179, 74, 93, 144, 85, 103, 225, 78, 7, 136, 127, 135, 13, 4, 145, 181, 182, 89, 50, 218, 151, 106, 29, 120, 120, 47, 95, 99, 154, 174, 162, 215, 81, 4, 69, 13, 211, 184, 36, 210, 240, 197, 147, 17, 24, 155, 193, 23, 58, 17, 143, 87, 210, 237, 37, 127, 6, 143, 22, 247, 49, 169, 45, 71, 76, 73, 254, 40, 148, 40, 164, 180, 255, 7, 22, 63, 6, 36, 200, 31, 67, 36, 231, 207, 182, 101, 191, 153, 169, 22, 125, 108, 149, 107, 249, 124, 176, 78, 130, 150 }, new Guid("692f3a64-2715-4757-901c-fd52e6bb5be6"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("e8e9779f-f911-445e-a23e-2e8a5b893cf1"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new byte[] { 18, 61, 210, 77, 150, 31, 34, 5, 182, 41, 132, 66, 183, 144, 202, 101, 72, 4, 49, 212, 90, 227, 192, 204, 160, 219, 20, 161, 19, 96, 141, 31, 105, 201, 78, 102, 177, 217, 226, 53, 166, 74, 23, 195, 233, 202, 70, 231, 243, 147, 57, 218, 212, 149, 68, 42, 105, 244, 72, 154, 239, 125, 246, 60, 172, 153, 244, 27, 36, 133, 133, 222, 69, 190, 172, 167, 25, 160, 144, 23, 150, 87, 114, 184, 28, 13, 81, 62, 113, 93, 235, 23, 22, 198, 105, 204, 160, 172, 195, 182, 218, 7, 117, 74, 140, 95, 10, 101, 3, 21, 11, 2, 213, 134, 10, 82, 199, 194, 114, 86, 210, 122, 78, 57, 44, 198, 209, 118, 161, 210, 28, 188, 154, 126, 232, 99, 143, 171, 132, 79, 5, 178, 14, 66, 201, 15, 56, 152, 99, 56, 28, 19, 201, 206, 251, 198, 14, 0, 82, 156, 5, 150, 67, 63, 95, 210, 232, 54, 163, 167, 187, 146, 126, 45, 145, 187, 173, 191, 223, 41, 140, 65, 82, 210, 76, 109, 20, 243, 80, 247, 89, 36, 19, 34, 131, 57, 59, 194, 127, 75, 248, 93, 28, 216, 10, 162, 220, 235, 233, 60, 170, 105, 215, 7, 81, 6, 223, 174, 177, 202, 122, 58, 161, 83, 147, 126, 245, 171, 158, 176, 65, 66, 7, 159, 117, 97, 239, 243, 49, 202, 62, 88, 11, 150, 100, 27, 241, 40, 109, 211, 51, 117, 192, 50, 147, 138, 219, 1, 185, 116, 96, 159, 223, 223, 180, 16, 52, 176, 242, 249, 250, 77, 170, 40, 132, 47, 127, 238, 21, 171, 85, 42, 181, 74, 34, 21, 136, 31, 38, 233, 11, 72, 130, 166, 121, 57, 95, 34, 51, 47 }, new Guid("e0c385e8-5a48-4ad5-8e69-9c5aa1923481"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("eae5e943-23b0-4569-81ba-0f1d208dc2b0"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new byte[] { 122, 134, 7, 175, 92, 25, 253, 161, 213, 39, 114, 173, 95, 33, 229, 209, 152, 72, 83, 201, 62, 183, 188, 37, 100, 43, 105, 163, 179, 74, 29, 61, 36, 61, 39, 208, 197, 61, 151, 245, 168, 194, 5, 241, 114, 16, 81, 156, 49, 137, 67, 34, 132, 109, 147, 32, 77, 54, 219, 202, 105, 96, 201, 155, 41, 235, 122, 231, 228, 95, 157, 230, 128, 252, 140, 244, 185, 4, 187, 87, 156, 167, 37, 48, 44, 116, 212, 249, 59, 116, 9, 17, 42, 237, 163, 86, 79, 157, 40, 233, 67, 162, 137, 47, 28, 115, 248, 58, 194, 198, 25, 246, 169, 247, 141, 84, 123, 102, 28, 29, 38, 226, 202, 17, 55, 104, 6, 234, 138, 6, 48, 182, 186, 194, 65, 24, 212, 124, 46, 110, 35, 76, 221, 254, 102, 81, 75, 47, 102, 11, 13, 120, 36, 169, 245, 77, 26, 81, 82, 237, 78, 209, 133, 26, 31, 129, 103, 225, 17, 86, 183, 221, 43, 210, 122, 69, 65, 139, 53, 199, 1, 104, 145, 37, 164, 245, 15, 100, 101, 112, 67, 191, 102, 138, 44, 251, 121, 245, 144, 14, 234, 229, 28, 213, 230, 205, 177, 168, 76, 4, 52, 212, 215, 200, 157, 164, 88, 127, 167, 113, 55, 244, 237, 231, 133, 157, 111, 251, 219, 82, 43, 181, 212, 7, 255, 0, 183, 130, 131, 152, 201, 6, 201, 44, 128, 2, 52, 189, 17, 62, 208, 123, 201, 147, 240, 31, 18, 125, 49, 109, 100, 126, 46, 8, 77, 30, 247, 230, 115, 115, 57, 127, 13, 183, 16, 161, 8, 29, 246, 117, 159, 235, 165, 108, 11, 135, 30, 184, 117, 251, 245, 50, 43, 148, 147, 226, 0, 253, 185, 95 }, new Guid("692f3a64-2715-4757-901c-fd52e6bb5be6"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "products",
                columns: new[] { "id", "created_at", "inventory", "product_line_id", "product_size_id", "updated_at" },
                values: new object[,]
                {
                    { new Guid("1acad2c1-3940-4456-9d5e-b9fb7c2bd0fa"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 100, new Guid("efb7215e-b0b4-4670-8e4e-1147856f09a0"), new Guid("d9f06780-c8db-4b1f-ad86-09b923c3722f"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("40be79d4-2f29-4919-9473-cfca2c31539f"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 200, new Guid("efb7215e-b0b4-4670-8e4e-1147856f09a0"), new Guid("1aba3111-0d53-4789-8dff-c3dac5bd9b12"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("4f455c51-eb27-48de-832a-5e86a4702d72"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 100, new Guid("efb7215e-b0b4-4670-8e4e-1147856f09a0"), new Guid("d6c24717-7e2c-4490-949a-79e5870e0a12"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("5431cce6-b5b3-4c8e-8e1c-93129a050215"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 200, new Guid("efb7215e-b0b4-4670-8e4e-1147856f09a0"), new Guid("1aba3111-0d53-4789-8dff-c3dac5bd9b12"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("5515aafc-680a-475b-8f90-9146e35511bc"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 200, new Guid("5b2cc5f5-474a-48a5-a546-dd9fb73c8d09"), new Guid("38b52562-538f-47ce-ae57-846e7d30be3f"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("5645e3d1-c51a-4af6-adf8-09d54b36e02e"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 200, new Guid("e0c385e8-5a48-4ad5-8e69-9c5aa1923481"), new Guid("38b52562-538f-47ce-ae57-846e7d30be3f"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("6090875e-11df-46e1-adb6-4c499912bbde"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 100, new Guid("efb7215e-b0b4-4670-8e4e-1147856f09a0"), new Guid("d9f06780-c8db-4b1f-ad86-09b923c3722f"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("6e740567-0922-4b5c-82c5-3894cb7e75a9"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 200, new Guid("5b2cc5f5-474a-48a5-a546-dd9fb73c8d09"), new Guid("1aba3111-0d53-4789-8dff-c3dac5bd9b12"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("721530a8-d131-4aa0-86b8-d82de1376bf4"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 100, new Guid("692f3a64-2715-4757-901c-fd52e6bb5be6"), new Guid("d9f06780-c8db-4b1f-ad86-09b923c3722f"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("73260729-c2d7-4fa7-aaa7-642d4e2d643f"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 100, new Guid("e0c385e8-5a48-4ad5-8e69-9c5aa1923481"), new Guid("d6c24717-7e2c-4490-949a-79e5870e0a12"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("8ce26fd6-9b9a-4050-a4f6-6dd243c1cd52"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 100, new Guid("5b2cc5f5-474a-48a5-a546-dd9fb73c8d09"), new Guid("d6c24717-7e2c-4490-949a-79e5870e0a12"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b123fc12-5c66-40d7-ac2a-da1f8944490f"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 200, new Guid("e0c385e8-5a48-4ad5-8e69-9c5aa1923481"), new Guid("1aba3111-0d53-4789-8dff-c3dac5bd9b12"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b16daedf-4a48-454d-9960-7c0f1d59fd3e"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 200, new Guid("692f3a64-2715-4757-901c-fd52e6bb5be6"), new Guid("38b52562-538f-47ce-ae57-846e7d30be3f"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("c84f0d5e-8e0a-46ff-91bb-5b0d28525fa2"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 200, new Guid("efb7215e-b0b4-4670-8e4e-1147856f09a0"), new Guid("38b52562-538f-47ce-ae57-846e7d30be3f"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("e41ec8f2-b38b-494b-b41a-78acd989eb7c"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 200, new Guid("efb7215e-b0b4-4670-8e4e-1147856f09a0"), new Guid("38b52562-538f-47ce-ae57-846e7d30be3f"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("e7905b30-c3a8-4754-ba14-ba9885e736fc"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 100, new Guid("e0c385e8-5a48-4ad5-8e69-9c5aa1923481"), new Guid("d9f06780-c8db-4b1f-ad86-09b923c3722f"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("ee8a8788-b529-4cb7-9f48-80bcd482c189"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 100, new Guid("5b2cc5f5-474a-48a5-a546-dd9fb73c8d09"), new Guid("d9f06780-c8db-4b1f-ad86-09b923c3722f"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("f550e2f4-1a35-41ac-899c-6bbfb0f1dcf9"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 100, new Guid("692f3a64-2715-4757-901c-fd52e6bb5be6"), new Guid("d6c24717-7e2c-4490-949a-79e5870e0a12"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("fed5c01b-79a6-46cf-accc-bc4f426059d7"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 100, new Guid("efb7215e-b0b4-4670-8e4e-1147856f09a0"), new Guid("d6c24717-7e2c-4490-949a-79e5870e0a12"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("ff086aea-7647-4fc7-b292-39ec7a3fdddb"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 200, new Guid("692f3a64-2715-4757-901c-fd52e6bb5be6"), new Guid("1aba3111-0d53-4789-8dff-c3dac5bd9b12"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.CreateIndex(
                name: "ix_address_user_id",
                table: "address",
                column: "user_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_avatar_user_id",
                table: "avatar",
                column: "user_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_image_product_line_id",
                table: "image",
                column: "product_line_id");

            migrationBuilder.CreateIndex(
                name: "ix_order_items_order_id",
                table: "order_items",
                column: "order_id");

            migrationBuilder.CreateIndex(
                name: "ix_orders_user_id",
                table: "orders",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_product_line_category_id",
                table: "product_line",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "ix_products_product_line_id",
                table: "products",
                column: "product_line_id");

            migrationBuilder.CreateIndex(
                name: "ix_products_product_size_id",
                table: "products",
                column: "product_size_id");

            migrationBuilder.CreateIndex(
                name: "ix_reviews_product_id",
                table: "reviews",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "ix_reviews_user_id",
                table: "reviews",
                column: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "address");

            migrationBuilder.DropTable(
                name: "avatar");

            migrationBuilder.DropTable(
                name: "image");

            migrationBuilder.DropTable(
                name: "order_items");

            migrationBuilder.DropTable(
                name: "reviews");

            migrationBuilder.DropTable(
                name: "orders");

            migrationBuilder.DropTable(
                name: "products");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "product_line");

            migrationBuilder.DropTable(
                name: "product_size");

            migrationBuilder.DropTable(
                name: "categories");
        }
    }
}
