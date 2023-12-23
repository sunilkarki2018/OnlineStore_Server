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
                    { new Guid("02278625-76af-467c-84b3-13201e31773d"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "footwear.jpg", "Footwear", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("357dca0e-8e2b-41e1-bac8-61b69bd29e4d"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "accessories.jpg", "Accessories", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("7bf432b9-6ce3-4b75-8d14-9785a8952913"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "tshirts.jpg", "T-Shirts", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("97fbf90a-5791-47db-b7e4-8b4a955134d4"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "dresses.jpg", "Dresses", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("deb18b25-663d-4453-9915-57dfe1665514"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "jeans.jpg", "Jeans", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "product_size",
                columns: new[] { "id", "created_at", "updated_at", "value" },
                values: new object[,]
                {
                    { new Guid("6ae68ed6-e940-4161-b798-737d5cb5e6d3"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 36 },
                    { new Guid("a6237a5f-49cc-4aa2-bbfe-99e944a928e6"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 34 },
                    { new Guid("d15a5728-8675-436b-87b9-c85c1f83b23f"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 38 },
                    { new Guid("f7abce51-a7f4-497d-9edc-b91fb603b716"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 32 }
                });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id", "created_at", "email", "first_name", "last_name", "password", "role", "salt", "updated_at" },
                values: new object[,]
                {
                    { new Guid("6f91900c-95bb-486c-9b65-f8b703984876"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "customer@mail.com", "Jane", "Doe", "e6c034af2ca62d41fa7dbc1e82fe11fb908d71ab47c37d831f1492a4c865aedc", Role.Customer, new byte[] { 138, 56, 188, 52, 132, 123, 151, 231, 192, 223, 113, 194, 233, 232, 160, 17, 53, 212, 200, 35, 199, 0, 135, 194, 254, 72, 111, 199, 192, 181, 59, 161, 11, 52, 235, 54, 182, 91, 138, 72, 63, 115, 25, 176, 138, 148, 55, 161, 255, 93, 172, 115, 148, 75, 148, 99, 158, 39, 4, 27, 92, 83, 120, 211 }, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("7d9d5dca-3501-4287-b1bc-76959c197fd9"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@mail.com", "JohnAdmin", "Doe", "81f85b239bfd73c6db4c206e8f051ad7fa5bf746d8fdf70f00c733cec5b5a595", Role.Admin, new byte[] { 101, 84, 236, 62, 66, 76, 138, 114, 115, 105, 103, 196, 202, 242, 125, 255, 17, 145, 124, 212, 166, 120, 78, 68, 42, 6, 157, 86, 33, 62, 42, 96, 182, 164, 45, 58, 34, 61, 134, 245, 231, 171, 126, 140, 171, 249, 228, 121, 154, 205, 76, 14, 111, 140, 99, 143, 173, 163, 171, 207, 49, 155, 195, 166 }, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "address",
                columns: new[] { "id", "city", "country", "created_at", "house_number", "post_code", "street", "updated_at", "user_id" },
                values: new object[,]
                {
                    { new Guid("2389938f-e8bb-47a2-af1b-65f3edcd38e3"), "Cityville", "Countryland", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "123", "12345", "Main St", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("7d9d5dca-3501-4287-b1bc-76959c197fd9") },
                    { new Guid("56c57be9-8cba-4296-9914-dc2a3992d59b"), "Townsville", "Countryland", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "456", "67890", "Oak St", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("6f91900c-95bb-486c-9b65-f8b703984876") }
                });

            migrationBuilder.InsertData(
                table: "avatar",
                columns: new[] { "id", "created_at", "data", "updated_at", "user_id" },
                values: new object[,]
                {
                    { new Guid("1fcd3a3e-8e19-4e28-9950-f2e98aa42798"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new byte[] { 10, 11, 12 }, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("6f91900c-95bb-486c-9b65-f8b703984876") },
                    { new Guid("6a71fad2-3e38-43ca-a237-a4f94b40d9ae"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new byte[] { 4, 5, 6 }, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("7d9d5dca-3501-4287-b1bc-76959c197fd9") }
                });

            migrationBuilder.InsertData(
                table: "product_line",
                columns: new[] { "id", "category_id", "created_at", "description", "price", "title", "updated_at" },
                values: new object[,]
                {
                    { new Guid("1ab57a5a-e194-4fba-9dc0-d0c465a3cf57"), new Guid("02278625-76af-467c-84b3-13201e31773d"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Casual sneakers for everyday wear.", 39.99m, "Sneakers", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("5f74d606-f2d7-4f17-a81a-28ff69e0b7dc"), new Guid("357dca0e-8e2b-41e1-bac8-61b69bd29e4d"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Classic leather belt to complete your look.", 14.99m, "Leather Belt", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("eabfdf42-9458-4de0-bba5-5115926b12ac"), new Guid("7bf432b9-6ce3-4b75-8d14-9785a8952913"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "A comfortable and stylish cotton T-shirt.", 19.99m, "Cotton T-Shirt", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("edad0e39-998f-4a67-ac26-f7a763cba2fd"), new Guid("97fbf90a-5791-47db-b7e4-8b4a955134d4"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "A floral summer dress for any occasion.", 29.99m, "Summer Dress", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("f20aacec-90d4-4820-a22c-68e92048d805"), new Guid("deb18b25-663d-4453-9915-57dfe1665514"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Slim fit jeans for a modern look.", 49.99m, "Slim Fit Jeans", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "image",
                columns: new[] { "id", "created_at", "data", "product_line_id", "updated_at" },
                values: new object[,]
                {
                    { new Guid("0cf4e621-5741-4e8a-bc34-374540788538"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new byte[] { 138, 244, 116, 114, 217, 63, 139, 144, 72, 176, 254, 151, 145, 194, 7, 236, 241, 62, 121, 101, 35, 97, 177, 214, 240, 31, 23, 191, 115, 169, 1, 247, 130, 181, 163, 142, 140, 227, 130, 102, 249, 55, 238, 12, 239, 158, 133, 49, 132, 239, 40, 75, 155, 71, 131, 113, 234, 11, 71, 44, 131, 75, 28, 238, 46, 127, 221, 119, 65, 193, 204, 51, 36, 30, 219, 131, 220, 37, 174, 6, 148, 111, 61, 43, 6, 205, 92, 140, 15, 219, 119, 80, 104, 93, 182, 194, 168, 97, 143, 147, 96, 157, 247, 242, 182, 224, 28, 185, 192, 78, 179, 203, 141, 158, 40, 28, 180, 68, 167, 141, 82, 85, 55, 187, 222, 103, 67, 132, 183, 95, 252, 17, 237, 231, 116, 156, 40, 146, 115, 154, 111, 222, 31, 226, 87, 10, 211, 131, 252, 97, 47, 111, 7, 13, 34, 196, 117, 89, 14, 66, 17, 99, 28, 104, 31, 13, 183, 144, 34, 10, 140, 37, 0, 187, 87, 9, 119, 232, 195, 243, 101, 60, 102, 20, 168, 91, 238, 119, 135, 230, 196, 59, 102, 25, 26, 121, 50, 101, 127, 175, 128, 131, 62, 157, 142, 81, 135, 14, 160, 118, 79, 28, 146, 124, 139, 86, 71, 184, 14, 65, 158, 30, 133, 97, 115, 54, 15, 104, 183, 61, 97, 235, 7, 124, 171, 9, 112, 14, 102, 236, 100, 184, 249, 90, 32, 129, 239, 124, 36, 247, 94, 241, 214, 236, 182, 202, 198, 59, 118, 83, 89, 139, 99, 117, 49, 126, 182, 19, 114, 185, 183, 89, 125, 65, 123, 108, 26, 188, 195, 48, 188, 146, 105, 30, 228, 245, 43, 237, 135, 203, 22, 238, 148, 235, 76, 232, 0, 129, 220, 185 }, new Guid("eabfdf42-9458-4de0-bba5-5115926b12ac"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("1dec53c8-d793-4930-b93b-c3e34fa582f8"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new byte[] { 124, 220, 123, 42, 65, 194, 130, 130, 102, 7, 187, 166, 205, 51, 212, 160, 16, 207, 230, 78, 46, 123, 177, 229, 130, 42, 39, 54, 201, 211, 155, 208, 86, 92, 110, 86, 199, 209, 201, 194, 113, 81, 153, 95, 76, 183, 219, 105, 197, 53, 79, 188, 38, 82, 194, 137, 164, 200, 67, 134, 88, 252, 47, 153, 3, 13, 203, 255, 72, 103, 73, 107, 96, 132, 80, 226, 66, 77, 119, 222, 196, 168, 151, 94, 213, 139, 86, 199, 229, 243, 31, 6, 82, 163, 26, 56, 77, 87, 248, 110, 234, 25, 229, 51, 215, 28, 169, 70, 59, 97, 53, 52, 56, 14, 229, 206, 145, 109, 212, 69, 158, 236, 193, 217, 158, 112, 91, 28, 128, 113, 178, 134, 203, 173, 53, 97, 65, 29, 86, 222, 133, 230, 141, 81, 61, 26, 202, 98, 23, 145, 161, 255, 5, 226, 126, 82, 73, 116, 83, 250, 186, 168, 58, 123, 212, 86, 2, 44, 199, 100, 116, 253, 243, 99, 141, 181, 138, 197, 175, 115, 216, 140, 247, 25, 169, 4, 33, 178, 123, 165, 186, 127, 41, 253, 153, 110, 195, 60, 244, 138, 114, 57, 52, 155, 48, 88, 219, 180, 27, 161, 48, 251, 144, 81, 197, 252, 141, 19, 253, 161, 169, 37, 30, 121, 112, 43, 128, 161, 246, 150, 71, 23, 215, 89, 66, 196, 184, 218, 158, 62, 36, 129, 11, 231, 145, 60, 69, 161, 15, 203, 173, 147, 185, 110, 211, 80, 83, 73, 166, 25, 181, 37, 91, 237, 73, 181, 244, 93, 12, 35, 124, 139, 108, 36, 42, 103, 117, 91, 179, 228, 160, 254, 57, 247, 10, 143, 116, 99, 91, 242, 213, 211, 102, 34, 153, 113, 250, 31, 59, 148 }, new Guid("f20aacec-90d4-4820-a22c-68e92048d805"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("42db92d6-1785-4562-917d-df832e3a0fc4"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new byte[] { 170, 211, 90, 242, 117, 46, 207, 254, 206, 41, 118, 78, 105, 75, 39, 50, 145, 47, 16, 85, 70, 3, 124, 162, 211, 249, 6, 142, 117, 86, 126, 54, 121, 201, 176, 225, 41, 218, 33, 179, 89, 51, 232, 110, 228, 152, 187, 62, 147, 187, 224, 18, 137, 159, 119, 76, 148, 1, 203, 35, 209, 150, 124, 3, 134, 7, 165, 239, 226, 200, 198, 181, 107, 47, 129, 5, 51, 27, 158, 66, 68, 21, 67, 231, 80, 117, 24, 73, 112, 76, 116, 199, 172, 52, 87, 223, 98, 138, 217, 71, 211, 203, 8, 186, 68, 130, 67, 134, 188, 17, 126, 245, 191, 197, 209, 30, 197, 60, 240, 91, 139, 162, 56, 156, 235, 148, 112, 43, 122, 253, 0, 222, 237, 13, 166, 114, 43, 188, 203, 134, 210, 170, 65, 225, 125, 53, 160, 124, 222, 211, 72, 158, 189, 93, 231, 71, 232, 220, 46, 52, 33, 36, 82, 103, 172, 132, 195, 49, 48, 253, 238, 17, 43, 125, 71, 140, 174, 101, 191, 198, 134, 191, 15, 93, 41, 29, 79, 38, 89, 168, 226, 152, 206, 96, 75, 37, 236, 150, 131, 215, 213, 176, 101, 255, 243, 213, 196, 253, 98, 98, 152, 60, 58, 50, 177, 250, 1, 23, 254, 88, 19, 126, 184, 251, 128, 176, 75, 224, 57, 185, 187, 190, 35, 33, 65, 240, 186, 25, 130, 88, 169, 25, 196, 154, 41, 207, 111, 128, 211, 32, 4, 246, 149, 232, 169, 92, 47, 29, 162, 250, 97, 28, 238, 182, 215, 207, 72, 210, 253, 78, 149, 226, 76, 153, 217, 245, 144, 34, 142, 100, 212, 165, 217, 0, 249, 193, 147, 123, 245, 188, 195, 129, 65, 21, 18, 200, 239, 125, 102, 213 }, new Guid("edad0e39-998f-4a67-ac26-f7a763cba2fd"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("a77ff04e-e8e0-4edf-bf2a-d4d596a705e7"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new byte[] { 164, 133, 109, 169, 30, 67, 71, 104, 98, 76, 87, 8, 228, 60, 113, 95, 246, 135, 209, 105, 186, 83, 178, 19, 15, 38, 94, 54, 47, 26, 182, 133, 97, 89, 201, 178, 71, 83, 210, 126, 176, 30, 80, 252, 169, 146, 20, 86, 8, 4, 186, 45, 6, 205, 255, 166, 90, 177, 49, 174, 39, 213, 66, 164, 174, 205, 172, 139, 101, 225, 223, 207, 33, 197, 75, 169, 43, 201, 35, 7, 166, 141, 232, 93, 205, 249, 251, 255, 74, 158, 199, 127, 230, 253, 122, 75, 141, 233, 53, 19, 230, 65, 99, 145, 123, 246, 9, 18, 213, 132, 87, 155, 161, 59, 169, 81, 219, 176, 224, 93, 8, 152, 140, 60, 143, 13, 16, 84, 244, 216, 236, 31, 106, 233, 30, 3, 119, 109, 36, 168, 110, 102, 24, 141, 207, 157, 180, 150, 118, 2, 24, 132, 28, 63, 250, 190, 150, 212, 82, 242, 89, 172, 48, 28, 74, 91, 197, 2, 52, 166, 4, 179, 210, 176, 250, 246, 235, 81, 70, 253, 250, 218, 193, 97, 233, 14, 222, 92, 166, 182, 219, 244, 16, 71, 127, 247, 120, 93, 162, 194, 185, 43, 74, 227, 30, 57, 60, 104, 227, 202, 251, 28, 196, 142, 155, 17, 123, 5, 116, 178, 214, 61, 161, 199, 109, 91, 93, 80, 149, 14, 54, 86, 101, 215, 1, 189, 88, 236, 252, 25, 239, 202, 81, 128, 17, 9, 126, 9, 193, 217, 18, 107, 106, 188, 197, 84, 6, 220, 21, 37, 210, 225, 33, 136, 80, 62, 67, 191, 252, 139, 22, 205, 128, 4, 219, 75, 241, 159, 14, 162, 234, 76, 223, 236, 217, 51, 168, 162, 122, 99, 220, 40, 59, 135, 97, 134, 168, 71, 16, 106 }, new Guid("1ab57a5a-e194-4fba-9dc0-d0c465a3cf57"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b97790c4-4aa4-4dde-8aeb-b98d19cba5c7"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new byte[] { 4, 214, 204, 224, 211, 38, 219, 98, 193, 178, 73, 92, 193, 115, 209, 86, 111, 12, 10, 185, 235, 26, 212, 150, 197, 168, 252, 16, 130, 126, 144, 175, 185, 197, 65, 98, 101, 14, 65, 4, 27, 74, 197, 170, 12, 15, 81, 202, 134, 82, 224, 43, 13, 34, 132, 254, 232, 247, 25, 153, 161, 120, 153, 1, 155, 219, 177, 178, 54, 231, 109, 109, 22, 202, 73, 29, 4, 93, 2, 144, 160, 81, 22, 210, 228, 43, 164, 211, 189, 166, 239, 91, 114, 154, 16, 189, 55, 166, 54, 194, 186, 12, 97, 104, 97, 124, 170, 183, 254, 195, 31, 225, 32, 21, 168, 176, 107, 66, 92, 178, 67, 175, 171, 143, 182, 136, 185, 177, 224, 187, 186, 215, 103, 126, 155, 217, 42, 35, 8, 249, 224, 253, 85, 223, 201, 52, 190, 216, 198, 33, 233, 145, 192, 109, 233, 159, 75, 3, 209, 145, 6, 196, 19, 201, 32, 168, 82, 54, 63, 125, 159, 90, 44, 26, 246, 69, 79, 85, 120, 49, 168, 43, 149, 226, 22, 14, 161, 237, 43, 75, 236, 72, 206, 140, 6, 93, 43, 73, 88, 251, 83, 187, 138, 1, 127, 156, 51, 8, 198, 52, 185, 178, 19, 46, 146, 125, 209, 50, 99, 143, 64, 72, 163, 156, 43, 190, 24, 19, 233, 70, 64, 232, 62, 44, 10, 212, 41, 82, 177, 149, 231, 239, 65, 95, 148, 14, 114, 115, 252, 60, 53, 236, 227, 140, 55, 204, 16, 133, 14, 129, 206, 126, 245, 146, 5, 136, 244, 190, 2, 166, 93, 75, 91, 66, 248, 232, 255, 200, 150, 231, 23, 208, 5, 34, 23, 200, 252, 90, 177, 35, 188, 186, 11, 227, 33, 56, 108, 187, 149, 64 }, new Guid("f20aacec-90d4-4820-a22c-68e92048d805"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("d39df881-b0e3-444d-bcd9-71ea5b6e52eb"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new byte[] { 51, 133, 109, 0, 195, 142, 109, 111, 239, 32, 10, 155, 70, 13, 8, 107, 21, 159, 16, 9, 175, 247, 66, 198, 218, 170, 149, 122, 38, 158, 67, 224, 63, 113, 223, 180, 127, 63, 58, 235, 208, 132, 220, 241, 27, 55, 186, 93, 28, 124, 142, 191, 177, 91, 2, 184, 24, 253, 206, 127, 33, 41, 218, 114, 139, 166, 204, 153, 186, 122, 172, 248, 223, 15, 26, 199, 139, 160, 191, 30, 255, 208, 116, 124, 14, 110, 119, 127, 79, 192, 119, 230, 4, 165, 238, 40, 213, 64, 218, 136, 151, 28, 114, 171, 140, 236, 245, 253, 214, 254, 108, 246, 68, 10, 89, 183, 131, 113, 81, 6, 240, 223, 171, 98, 69, 46, 141, 28, 11, 178, 57, 48, 188, 183, 243, 61, 9, 29, 175, 146, 246, 230, 118, 48, 90, 2, 4, 225, 117, 94, 1, 244, 41, 118, 134, 7, 220, 174, 199, 116, 13, 91, 170, 168, 144, 243, 136, 231, 157, 145, 229, 34, 2, 79, 172, 27, 105, 210, 41, 150, 71, 176, 79, 79, 47, 119, 180, 53, 250, 76, 36, 152, 109, 249, 24, 87, 1, 163, 253, 19, 178, 119, 6, 104, 222, 19, 27, 182, 163, 129, 1, 143, 186, 75, 240, 216, 237, 61, 196, 31, 142, 16, 194, 160, 9, 19, 234, 166, 210, 147, 65, 186, 53, 22, 43, 27, 28, 81, 233, 209, 70, 78, 192, 233, 85, 15, 118, 177, 65, 230, 239, 67, 117, 61, 205, 70, 143, 215, 118, 237, 222, 247, 12, 180, 196, 145, 88, 13, 169, 206, 162, 40, 103, 92, 200, 72, 206, 190, 89, 221, 213, 90, 197, 243, 102, 167, 216, 111, 11, 194, 124, 192, 126, 20, 241, 127, 231, 245, 192, 181 }, new Guid("1ab57a5a-e194-4fba-9dc0-d0c465a3cf57"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("dad0925e-f5b9-4120-bd6f-7c14cd77e6c8"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new byte[] { 231, 12, 104, 148, 92, 205, 62, 98, 91, 235, 247, 181, 35, 188, 146, 1, 183, 121, 226, 119, 217, 246, 53, 52, 7, 64, 131, 164, 248, 72, 225, 165, 148, 47, 221, 50, 140, 169, 174, 109, 202, 131, 192, 159, 64, 232, 119, 208, 52, 181, 64, 210, 255, 111, 112, 243, 234, 129, 195, 82, 61, 148, 201, 197, 123, 115, 187, 151, 145, 127, 70, 202, 31, 143, 188, 96, 236, 12, 125, 112, 179, 33, 159, 210, 52, 167, 77, 202, 20, 51, 145, 248, 50, 36, 114, 154, 175, 224, 19, 3, 87, 213, 147, 130, 86, 179, 131, 10, 98, 202, 236, 85, 39, 76, 145, 81, 71, 145, 102, 167, 173, 30, 171, 178, 63, 40, 111, 91, 135, 143, 155, 142, 195, 54, 216, 79, 6, 78, 145, 173, 253, 154, 164, 49, 192, 179, 159, 27, 51, 34, 223, 208, 175, 63, 173, 198, 193, 206, 17, 246, 141, 65, 226, 213, 169, 171, 153, 90, 59, 199, 24, 6, 120, 50, 246, 208, 94, 134, 82, 46, 160, 216, 92, 16, 160, 131, 210, 94, 175, 195, 106, 93, 227, 163, 114, 105, 175, 157, 206, 91, 37, 74, 96, 25, 243, 138, 85, 169, 209, 175, 68, 93, 16, 166, 36, 50, 41, 133, 59, 186, 41, 185, 199, 177, 43, 52, 39, 190, 80, 57, 89, 125, 69, 231, 11, 228, 222, 144, 124, 163, 188, 255, 187, 172, 80, 114, 195, 184, 7, 137, 27, 144, 161, 209, 97, 240, 208, 40, 216, 159, 184, 253, 74, 78, 72, 28, 169, 244, 71, 178, 123, 153, 175, 67, 160, 146, 170, 113, 98, 49, 11, 197, 34, 32, 189, 206, 52, 175, 195, 252, 241, 198, 147, 205, 130, 71, 159, 70, 112, 47 }, new Guid("edad0e39-998f-4a67-ac26-f7a763cba2fd"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("f63be02a-aa58-4b9b-b01b-44ee3a88e6da"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new byte[] { 82, 75, 200, 178, 206, 199, 48, 214, 239, 125, 82, 154, 82, 154, 217, 185, 176, 18, 18, 248, 126, 222, 202, 230, 161, 179, 203, 64, 41, 246, 179, 150, 247, 105, 146, 71, 127, 51, 1, 107, 109, 134, 125, 57, 103, 151, 14, 126, 117, 93, 65, 52, 106, 229, 217, 114, 96, 55, 159, 110, 162, 198, 12, 114, 85, 78, 138, 23, 240, 56, 51, 26, 30, 74, 61, 203, 233, 190, 19, 176, 195, 217, 159, 127, 23, 68, 46, 201, 247, 148, 182, 218, 76, 236, 190, 104, 207, 127, 246, 51, 12, 118, 77, 4, 85, 104, 168, 90, 146, 58, 102, 102, 195, 147, 89, 107, 224, 111, 49, 254, 82, 194, 107, 171, 255, 173, 139, 187, 112, 38, 1, 197, 65, 180, 14, 13, 99, 126, 61, 148, 84, 193, 248, 33, 147, 193, 37, 84, 150, 174, 28, 182, 239, 211, 245, 21, 195, 233, 212, 57, 30, 91, 60, 160, 163, 201, 253, 140, 158, 121, 3, 115, 229, 101, 215, 88, 50, 57, 199, 15, 168, 47, 30, 114, 194, 136, 197, 29, 193, 43, 81, 33, 243, 239, 57, 33, 157, 144, 14, 37, 149, 234, 156, 117, 209, 71, 211, 10, 9, 16, 45, 75, 67, 58, 160, 62, 241, 75, 146, 24, 26, 35, 107, 20, 95, 242, 103, 223, 5, 68, 232, 107, 131, 123, 107, 195, 25, 163, 69, 55, 202, 217, 51, 67, 75, 136, 196, 246, 248, 197, 141, 102, 198, 164, 72, 18, 20, 249, 103, 227, 167, 233, 247, 68, 48, 98, 37, 213, 6, 225, 109, 220, 96, 34, 199, 184, 20, 207, 134, 217, 121, 27, 193, 73, 49, 28, 155, 201, 24, 121, 39, 24, 53, 74, 81, 65, 46, 49, 153, 143 }, new Guid("eabfdf42-9458-4de0-bba5-5115926b12ac"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "products",
                columns: new[] { "id", "created_at", "inventory", "product_line_id", "product_size_id", "updated_at" },
                values: new object[,]
                {
                    { new Guid("00994193-8c85-441f-8594-e0501533ec65"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 200, new Guid("edad0e39-998f-4a67-ac26-f7a763cba2fd"), new Guid("a6237a5f-49cc-4aa2-bbfe-99e944a928e6"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("09dc0a5f-de48-4295-95da-c3f03b6bd0bb"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 100, new Guid("f20aacec-90d4-4820-a22c-68e92048d805"), new Guid("d15a5728-8675-436b-87b9-c85c1f83b23f"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("2835c5e0-12bd-4138-925a-c9aba65ad58a"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 200, new Guid("edad0e39-998f-4a67-ac26-f7a763cba2fd"), new Guid("6ae68ed6-e940-4161-b798-737d5cb5e6d3"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("2e127813-bf54-404d-a54f-423c4c9fa8e5"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 100, new Guid("edad0e39-998f-4a67-ac26-f7a763cba2fd"), new Guid("f7abce51-a7f4-497d-9edc-b91fb603b716"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("30083e9d-ab69-437b-84f3-40cdfc632ee3"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 100, new Guid("1ab57a5a-e194-4fba-9dc0-d0c465a3cf57"), new Guid("f7abce51-a7f4-497d-9edc-b91fb603b716"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("43dca2bc-b4a7-4352-afb1-58d72e691277"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 100, new Guid("1ab57a5a-e194-4fba-9dc0-d0c465a3cf57"), new Guid("d15a5728-8675-436b-87b9-c85c1f83b23f"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("45270208-d5f6-4e92-8854-5bb0ad926432"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 100, new Guid("edad0e39-998f-4a67-ac26-f7a763cba2fd"), new Guid("d15a5728-8675-436b-87b9-c85c1f83b23f"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("477ab241-9379-4f1a-a357-a46ce1595578"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 200, new Guid("eabfdf42-9458-4de0-bba5-5115926b12ac"), new Guid("6ae68ed6-e940-4161-b798-737d5cb5e6d3"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("480a1847-3553-4c52-85d3-d052716defb2"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 200, new Guid("1ab57a5a-e194-4fba-9dc0-d0c465a3cf57"), new Guid("6ae68ed6-e940-4161-b798-737d5cb5e6d3"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("49c9dee1-8177-4d03-8438-808e8995928b"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 200, new Guid("f20aacec-90d4-4820-a22c-68e92048d805"), new Guid("6ae68ed6-e940-4161-b798-737d5cb5e6d3"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("66ed03f7-4ee1-4ffe-b02a-181bd2e2f070"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 200, new Guid("f20aacec-90d4-4820-a22c-68e92048d805"), new Guid("a6237a5f-49cc-4aa2-bbfe-99e944a928e6"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("7597c88b-5ddd-4afc-a822-bf9d9f2450c6"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 200, new Guid("1ab57a5a-e194-4fba-9dc0-d0c465a3cf57"), new Guid("a6237a5f-49cc-4aa2-bbfe-99e944a928e6"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("7f9e223f-ca21-4732-8ccf-315bc05529c9"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 100, new Guid("1ab57a5a-e194-4fba-9dc0-d0c465a3cf57"), new Guid("f7abce51-a7f4-497d-9edc-b91fb603b716"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("a220e4fe-2a5d-4ec8-a4a4-ffefa2048ef3"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 200, new Guid("eabfdf42-9458-4de0-bba5-5115926b12ac"), new Guid("a6237a5f-49cc-4aa2-bbfe-99e944a928e6"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("a6dfbcbf-eeb9-4083-a240-cf7860cbe004"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 100, new Guid("eabfdf42-9458-4de0-bba5-5115926b12ac"), new Guid("f7abce51-a7f4-497d-9edc-b91fb603b716"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("bcd0eca6-010a-4c8f-b0fc-f562d9cb5c44"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 100, new Guid("eabfdf42-9458-4de0-bba5-5115926b12ac"), new Guid("d15a5728-8675-436b-87b9-c85c1f83b23f"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("bdc4f855-76ca-4e13-b279-4992234a9a6c"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 200, new Guid("1ab57a5a-e194-4fba-9dc0-d0c465a3cf57"), new Guid("a6237a5f-49cc-4aa2-bbfe-99e944a928e6"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("cc12ebb6-c434-4fe9-b55d-b2f4737ccc6a"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 200, new Guid("1ab57a5a-e194-4fba-9dc0-d0c465a3cf57"), new Guid("6ae68ed6-e940-4161-b798-737d5cb5e6d3"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("ce223650-40f0-4785-9856-ffb50fa3dd85"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 100, new Guid("f20aacec-90d4-4820-a22c-68e92048d805"), new Guid("f7abce51-a7f4-497d-9edc-b91fb603b716"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("e3f15ed3-3890-4e64-b1f4-2e4f03ff0a05"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 100, new Guid("1ab57a5a-e194-4fba-9dc0-d0c465a3cf57"), new Guid("d15a5728-8675-436b-87b9-c85c1f83b23f"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
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
