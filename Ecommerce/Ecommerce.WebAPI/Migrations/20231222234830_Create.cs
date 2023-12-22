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
                    { new Guid("0c0ca325-8f6a-4a75-8a1f-a93e55ccec08"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "footwear.jpg", "Footwear", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("28e47718-a072-46d2-ae80-9c660d0581a4"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "accessories.jpg", "Accessories", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("524b7181-5bf1-4259-a2cb-b6cd2f42c41b"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "tshirts.jpg", "T-Shirts", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("99208fac-108f-437c-8fdb-bbc1626a14d1"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "dresses.jpg", "Dresses", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("ad57e412-3531-446a-ae14-f08c046230d4"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "jeans.jpg", "Jeans", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "product_size",
                columns: new[] { "id", "created_at", "updated_at", "value" },
                values: new object[,]
                {
                    { new Guid("01f7ce9b-e369-4ac0-9e15-94c8366d30a1"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 38 },
                    { new Guid("ac9d37f1-d707-4065-a67a-fb7874ad4bc9"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 36 },
                    { new Guid("b8fc3023-e0c2-40af-9af8-72784fce8183"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 34 },
                    { new Guid("fb1c6dfa-052e-4f37-ab06-21e77df227d9"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 32 }
                });

            migrationBuilder.InsertData(
                table: "product_line",
                columns: new[] { "id", "category_id", "created_at", "description", "price", "title", "updated_at" },
                values: new object[,]
                {
                    { new Guid("3e6c79c9-6a73-40cb-ba02-a1e8a68ad595"), new Guid("99208fac-108f-437c-8fdb-bbc1626a14d1"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "A floral summer dress for any occasion.", 29.99m, "Summer Dress", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("4799b78c-1ae9-403b-ab1d-f1be2b9b096a"), new Guid("ad57e412-3531-446a-ae14-f08c046230d4"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Slim fit jeans for a modern look.", 49.99m, "Slim Fit Jeans", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("c3dbaf36-8c72-4996-b053-71510f3d09c5"), new Guid("28e47718-a072-46d2-ae80-9c660d0581a4"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Classic leather belt to complete your look.", 14.99m, "Leather Belt", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("d3dcd26d-f563-4cce-b3c6-1fd1e91d6850"), new Guid("524b7181-5bf1-4259-a2cb-b6cd2f42c41b"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "A comfortable and stylish cotton T-shirt.", 19.99m, "Cotton T-Shirt", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("f70b97ec-9c19-404f-bda1-c41d92c26c1e"), new Guid("0c0ca325-8f6a-4a75-8a1f-a93e55ccec08"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Casual sneakers for everyday wear.", 39.99m, "Sneakers", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "image",
                columns: new[] { "id", "created_at", "data", "product_line_id", "updated_at" },
                values: new object[,]
                {
                    { new Guid("326b98c2-20f6-42c1-ae1c-3f50430b9fec"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new byte[] { 32, 152, 161, 14, 179, 88, 137, 82, 104, 19, 169, 47, 139, 197, 189, 230, 101, 175, 203, 135, 48, 185, 229, 90, 160, 188, 144, 2, 56, 250, 192, 69, 69, 244, 251, 119, 225, 150, 39, 110, 44, 190, 72, 125, 152, 111, 224, 73, 217, 241, 153, 70, 123, 175, 167, 128, 64, 122, 189, 15, 15, 189, 163, 235, 159, 4, 251, 144, 165, 228, 32, 70, 4, 93, 228, 249, 213, 27, 191, 243, 23, 211, 59, 53, 92, 120, 136, 104, 69, 167, 56, 38, 98, 28, 111, 188, 73, 245, 82, 156, 6, 249, 209, 80, 154, 242, 124, 90, 43, 154, 65, 90, 233, 153, 198, 4, 66, 237, 5, 60, 244, 48, 188, 210, 162, 156, 97, 62, 1, 62, 98, 165, 162, 213, 172, 33, 6, 45, 115, 165, 109, 195, 233, 168, 243, 248, 101, 106, 103, 185, 84, 243, 18, 128, 245, 252, 80, 23, 109, 124, 201, 67, 26, 146, 208, 223, 134, 49, 206, 246, 238, 129, 193, 89, 106, 15, 190, 82, 142, 183, 110, 224, 18, 227, 250, 215, 70, 185, 52, 229, 100, 90, 197, 229, 118, 63, 68, 217, 5, 234, 215, 20, 17, 33, 166, 21, 136, 172, 162, 226, 177, 87, 224, 177, 6, 220, 2, 35, 133, 42, 178, 254, 8, 191, 27, 215, 199, 120, 11, 194, 75, 235, 162, 163, 102, 51, 172, 95, 246, 221, 126, 188, 63, 84, 161, 71, 28, 252, 133, 40, 202, 111, 30, 249, 35, 74, 216, 209, 143, 172, 234, 165, 147, 27, 250, 222, 30, 36, 51, 76, 72, 32, 160, 182, 237, 223, 237, 190, 121, 199, 181, 7, 197, 34, 181, 8, 100, 130, 30, 155, 138, 73, 239, 216, 211, 198, 20, 142, 248, 195 }, new Guid("4799b78c-1ae9-403b-ab1d-f1be2b9b096a"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("5b68e66e-41ef-4fbd-8363-24c7a06d8f6a"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new byte[] { 244, 191, 1, 89, 241, 193, 237, 119, 78, 87, 255, 195, 194, 108, 93, 52, 131, 122, 255, 22, 133, 3, 32, 13, 71, 202, 36, 79, 76, 38, 29, 86, 145, 159, 86, 253, 232, 231, 13, 17, 118, 213, 205, 76, 128, 184, 204, 232, 67, 158, 90, 207, 154, 226, 83, 170, 179, 87, 143, 134, 107, 28, 173, 93, 102, 160, 119, 97, 180, 64, 142, 155, 49, 176, 64, 190, 100, 6, 233, 100, 124, 111, 164, 96, 134, 79, 27, 96, 196, 91, 144, 47, 138, 241, 101, 106, 139, 242, 110, 124, 116, 168, 170, 189, 113, 13, 131, 93, 187, 8, 50, 0, 41, 78, 99, 91, 71, 63, 108, 250, 227, 139, 75, 191, 3, 147, 108, 61, 4, 43, 51, 156, 212, 48, 161, 83, 135, 20, 157, 74, 249, 246, 185, 231, 179, 44, 47, 34, 114, 217, 29, 202, 67, 121, 122, 46, 120, 202, 64, 6, 134, 126, 9, 249, 102, 119, 15, 246, 52, 187, 27, 39, 77, 253, 26, 161, 220, 2, 255, 36, 49, 232, 168, 199, 189, 246, 180, 100, 35, 66, 64, 255, 136, 175, 214, 151, 177, 65, 44, 68, 189, 195, 164, 80, 30, 3, 55, 241, 197, 180, 210, 255, 18, 199, 169, 9, 199, 147, 227, 6, 109, 48, 28, 114, 219, 136, 196, 208, 241, 15, 178, 60, 234, 243, 59, 166, 131, 177, 233, 42, 65, 62, 178, 124, 57, 137, 208, 46, 84, 78, 109, 70, 222, 41, 50, 231, 236, 147, 11, 217, 123, 1, 226, 39, 88, 82, 189, 201, 148, 70, 95, 21, 157, 70, 211, 171, 214, 155, 231, 206, 21, 164, 163, 165, 177, 179, 212, 191, 161, 196, 170, 29, 85, 91, 89, 213, 127, 0, 111, 77 }, new Guid("3e6c79c9-6a73-40cb-ba02-a1e8a68ad595"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("9039ba2e-65ee-4955-b41d-ebf855a1b822"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new byte[] { 104, 182, 169, 119, 240, 182, 215, 25, 123, 97, 162, 226, 145, 60, 4, 185, 75, 43, 100, 251, 99, 234, 245, 161, 85, 167, 62, 212, 84, 66, 204, 28, 137, 143, 22, 20, 38, 160, 135, 6, 143, 125, 28, 217, 102, 34, 194, 205, 182, 56, 31, 146, 223, 11, 87, 117, 4, 197, 46, 199, 31, 35, 30, 248, 148, 57, 216, 249, 127, 212, 48, 206, 104, 6, 79, 45, 219, 241, 78, 207, 175, 71, 226, 45, 147, 17, 80, 252, 81, 132, 143, 169, 125, 186, 207, 177, 90, 139, 52, 178, 192, 161, 167, 172, 11, 45, 252, 27, 250, 165, 29, 7, 18, 86, 118, 21, 249, 113, 108, 3, 11, 101, 161, 54, 50, 51, 223, 171, 230, 68, 93, 135, 168, 253, 123, 43, 240, 66, 114, 123, 174, 65, 22, 130, 75, 154, 239, 74, 71, 250, 125, 89, 101, 113, 228, 120, 6, 56, 73, 19, 101, 230, 134, 245, 42, 225, 7, 34, 161, 49, 17, 100, 205, 86, 132, 204, 191, 144, 180, 54, 210, 167, 46, 51, 175, 254, 116, 111, 182, 89, 152, 134, 171, 247, 6, 33, 171, 5, 74, 84, 98, 227, 107, 168, 4, 19, 109, 139, 76, 220, 235, 100, 60, 95, 244, 158, 203, 235, 122, 240, 36, 105, 103, 233, 242, 43, 71, 112, 216, 216, 214, 156, 117, 68, 16, 160, 196, 182, 190, 209, 214, 48, 127, 249, 105, 198, 83, 106, 166, 173, 4, 147, 95, 168, 60, 248, 80, 252, 85, 236, 73, 17, 123, 148, 17, 156, 163, 182, 146, 248, 33, 94, 208, 250, 113, 107, 190, 76, 236, 150, 7, 89, 132, 30, 130, 147, 173, 206, 222, 215, 237, 146, 106, 5, 40, 235, 240, 141, 220, 254 }, new Guid("d3dcd26d-f563-4cce-b3c6-1fd1e91d6850"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("acdae479-a67c-460a-b9e0-85d5a0d91e16"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new byte[] { 153, 19, 90, 67, 59, 7, 12, 207, 239, 69, 6, 95, 137, 231, 30, 134, 90, 169, 87, 173, 158, 123, 148, 33, 90, 102, 184, 131, 241, 35, 98, 132, 180, 239, 21, 97, 17, 75, 88, 18, 159, 39, 26, 227, 45, 87, 188, 63, 229, 98, 221, 198, 99, 100, 234, 36, 217, 210, 150, 99, 182, 2, 100, 2, 249, 122, 51, 170, 229, 154, 87, 236, 149, 91, 248, 202, 97, 151, 40, 212, 203, 45, 246, 31, 174, 42, 221, 118, 40, 246, 52, 197, 11, 235, 198, 212, 139, 105, 78, 39, 52, 169, 199, 57, 254, 178, 223, 197, 20, 221, 4, 161, 43, 98, 187, 207, 199, 114, 2, 142, 17, 90, 71, 208, 148, 176, 210, 169, 170, 50, 203, 177, 53, 255, 126, 240, 39, 57, 169, 205, 202, 56, 19, 146, 164, 67, 88, 135, 100, 85, 110, 230, 97, 160, 236, 92, 196, 30, 68, 178, 58, 254, 35, 229, 195, 169, 201, 244, 162, 56, 243, 150, 13, 244, 188, 246, 231, 95, 4, 29, 221, 200, 61, 124, 149, 87, 42, 12, 202, 228, 191, 88, 219, 212, 38, 208, 102, 240, 185, 28, 207, 8, 104, 48, 219, 21, 250, 77, 247, 245, 94, 172, 38, 136, 244, 93, 34, 249, 78, 19, 68, 195, 253, 180, 239, 147, 229, 242, 207, 96, 214, 233, 245, 85, 15, 252, 224, 7, 173, 31, 130, 87, 107, 183, 114, 32, 201, 197, 142, 77, 93, 52, 151, 14, 14, 194, 78, 255, 158, 31, 133, 70, 99, 5, 64, 156, 21, 96, 30, 213, 21, 252, 34, 19, 136, 213, 68, 155, 19, 202, 107, 175, 123, 56, 40, 191, 241, 180, 181, 74, 231, 117, 175, 22, 40, 228, 153, 103, 57, 53 }, new Guid("3e6c79c9-6a73-40cb-ba02-a1e8a68ad595"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("bf932db0-6855-4842-917e-2636186b10f1"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new byte[] { 134, 225, 241, 202, 36, 146, 240, 163, 30, 178, 137, 146, 21, 66, 16, 122, 96, 202, 135, 40, 211, 234, 51, 241, 155, 182, 190, 195, 212, 179, 196, 104, 78, 55, 164, 41, 175, 48, 151, 152, 161, 191, 92, 129, 188, 33, 90, 85, 221, 99, 156, 190, 24, 56, 166, 238, 21, 38, 190, 254, 158, 164, 51, 50, 11, 226, 77, 42, 183, 109, 191, 168, 49, 92, 246, 51, 140, 37, 143, 74, 226, 217, 58, 43, 64, 118, 191, 185, 205, 35, 244, 120, 72, 238, 92, 136, 190, 10, 247, 9, 195, 185, 140, 7, 55, 5, 182, 203, 95, 20, 158, 224, 243, 1, 22, 238, 75, 121, 43, 184, 198, 153, 76, 232, 163, 30, 199, 191, 1, 165, 122, 118, 216, 132, 207, 182, 240, 16, 162, 127, 200, 167, 151, 31, 221, 32, 251, 229, 116, 115, 161, 98, 90, 144, 221, 251, 68, 123, 114, 151, 23, 131, 105, 137, 243, 142, 113, 47, 254, 137, 69, 94, 125, 85, 135, 141, 96, 142, 111, 53, 47, 59, 174, 237, 143, 226, 251, 210, 94, 147, 177, 51, 177, 176, 237, 219, 14, 39, 85, 248, 232, 215, 54, 43, 112, 251, 61, 228, 75, 73, 126, 89, 232, 150, 62, 87, 72, 161, 2, 237, 175, 175, 159, 180, 102, 39, 220, 212, 37, 198, 200, 48, 31, 102, 103, 197, 182, 113, 19, 247, 170, 24, 224, 203, 176, 58, 166, 124, 228, 79, 181, 173, 201, 203, 134, 217, 249, 203, 96, 217, 240, 49, 69, 41, 156, 179, 87, 160, 184, 226, 67, 203, 106, 91, 108, 37, 142, 72, 155, 218, 87, 136, 205, 191, 125, 51, 94, 18, 183, 101, 72, 245, 213, 73, 80, 224, 114, 150, 147, 93 }, new Guid("4799b78c-1ae9-403b-ab1d-f1be2b9b096a"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("edc95fc8-8384-4319-a204-f8539b46052c"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new byte[] { 105, 16, 178, 9, 183, 183, 55, 78, 183, 100, 95, 22, 219, 200, 137, 64, 85, 77, 245, 212, 152, 28, 81, 65, 191, 202, 201, 163, 125, 234, 193, 131, 80, 200, 133, 15, 235, 137, 90, 148, 88, 34, 154, 122, 228, 79, 175, 53, 110, 110, 144, 107, 1, 16, 6, 134, 4, 165, 56, 191, 36, 110, 221, 198, 71, 184, 168, 150, 60, 107, 16, 101, 167, 27, 167, 169, 143, 189, 225, 195, 217, 94, 37, 29, 191, 93, 59, 76, 1, 107, 106, 216, 242, 5, 246, 195, 193, 245, 198, 160, 246, 216, 127, 7, 250, 118, 113, 64, 83, 67, 243, 146, 15, 4, 175, 132, 209, 225, 70, 210, 37, 190, 130, 228, 149, 145, 229, 60, 126, 119, 208, 46, 145, 61, 103, 106, 135, 198, 233, 122, 28, 113, 88, 239, 48, 139, 117, 105, 141, 91, 167, 228, 191, 163, 142, 195, 149, 20, 188, 181, 114, 237, 130, 109, 146, 100, 246, 109, 13, 171, 227, 0, 73, 17, 238, 211, 207, 16, 121, 0, 60, 149, 96, 18, 75, 126, 37, 75, 65, 134, 124, 176, 46, 116, 157, 242, 251, 84, 156, 162, 108, 168, 187, 255, 47, 123, 92, 124, 55, 74, 87, 127, 132, 179, 144, 239, 249, 182, 133, 189, 123, 147, 102, 160, 157, 165, 180, 191, 131, 86, 9, 247, 8, 171, 197, 169, 164, 25, 192, 242, 253, 38, 59, 229, 65, 86, 83, 207, 96, 182, 78, 76, 5, 251, 62, 68, 145, 122, 89, 250, 140, 216, 102, 245, 1, 106, 75, 56, 232, 21, 130, 178, 60, 60, 216, 237, 82, 156, 42, 235, 27, 182, 154, 58, 101, 6, 139, 226, 147, 184, 85, 253, 96, 183, 121, 16, 220, 204, 105, 96 }, new Guid("f70b97ec-9c19-404f-bda1-c41d92c26c1e"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("f341b215-d32d-4567-8e1d-9ccb0c901a96"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new byte[] { 234, 100, 69, 74, 34, 200, 97, 197, 137, 16, 64, 183, 27, 96, 2, 222, 82, 201, 253, 90, 94, 108, 43, 68, 220, 164, 175, 70, 135, 4, 95, 166, 13, 66, 65, 149, 252, 140, 198, 139, 170, 169, 77, 70, 165, 156, 158, 137, 82, 156, 15, 123, 19, 120, 106, 85, 101, 243, 155, 198, 32, 240, 105, 213, 185, 198, 127, 112, 90, 234, 15, 49, 224, 93, 59, 52, 16, 197, 237, 60, 131, 231, 72, 169, 226, 165, 116, 85, 180, 174, 39, 112, 12, 145, 210, 230, 152, 0, 116, 55, 165, 98, 255, 121, 112, 112, 71, 139, 253, 181, 29, 86, 185, 53, 119, 87, 88, 102, 25, 20, 219, 120, 230, 20, 73, 20, 253, 77, 250, 212, 87, 244, 209, 24, 155, 109, 125, 155, 107, 39, 28, 75, 153, 62, 27, 115, 192, 7, 35, 11, 87, 246, 252, 250, 57, 240, 246, 121, 231, 72, 125, 159, 94, 93, 247, 136, 58, 77, 31, 250, 85, 86, 44, 239, 68, 38, 176, 123, 108, 135, 158, 62, 203, 22, 242, 57, 102, 221, 207, 13, 84, 71, 104, 69, 233, 70, 16, 46, 206, 239, 223, 153, 11, 231, 87, 74, 114, 152, 75, 203, 142, 179, 67, 50, 35, 2, 1, 242, 75, 101, 96, 72, 44, 183, 7, 132, 115, 43, 188, 78, 245, 99, 65, 153, 121, 183, 13, 244, 32, 107, 109, 51, 49, 194, 52, 188, 186, 224, 59, 28, 108, 84, 150, 55, 136, 109, 157, 124, 16, 12, 92, 110, 22, 133, 221, 213, 6, 46, 129, 138, 32, 5, 38, 238, 211, 224, 77, 58, 57, 151, 22, 126, 172, 54, 211, 144, 238, 103, 193, 32, 101, 69, 20, 145, 230, 41, 142, 232, 201, 135 }, new Guid("f70b97ec-9c19-404f-bda1-c41d92c26c1e"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("f53a84c5-2693-424c-911e-252b5da948b1"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new byte[] { 153, 81, 167, 169, 168, 99, 16, 220, 229, 61, 110, 126, 205, 114, 74, 160, 174, 169, 241, 101, 2, 38, 181, 25, 122, 208, 154, 94, 68, 229, 112, 162, 244, 125, 109, 109, 239, 32, 129, 239, 76, 17, 242, 52, 158, 178, 153, 151, 168, 173, 27, 54, 59, 236, 251, 218, 246, 116, 200, 100, 242, 236, 249, 241, 55, 44, 11, 253, 64, 150, 45, 143, 155, 197, 187, 255, 2, 66, 90, 137, 201, 31, 252, 113, 85, 61, 116, 233, 249, 29, 68, 121, 188, 13, 138, 24, 51, 215, 243, 45, 233, 81, 70, 69, 166, 162, 249, 19, 55, 224, 248, 120, 228, 225, 51, 60, 39, 116, 159, 43, 116, 253, 49, 109, 170, 31, 202, 123, 219, 253, 12, 17, 21, 54, 0, 163, 203, 153, 40, 200, 100, 254, 209, 69, 197, 132, 141, 73, 21, 154, 95, 172, 192, 250, 185, 231, 11, 250, 111, 76, 203, 81, 25, 70, 12, 1, 94, 156, 61, 150, 1, 2, 118, 70, 34, 125, 29, 240, 170, 154, 91, 240, 208, 183, 75, 23, 149, 140, 239, 36, 89, 238, 98, 7, 161, 174, 2, 243, 95, 190, 194, 161, 124, 250, 74, 29, 108, 211, 252, 136, 84, 196, 209, 241, 17, 65, 253, 223, 56, 120, 105, 126, 249, 193, 209, 81, 41, 171, 77, 158, 59, 56, 111, 118, 13, 2, 221, 126, 205, 26, 166, 147, 53, 201, 130, 31, 43, 72, 104, 25, 79, 12, 134, 136, 84, 25, 5, 251, 23, 87, 26, 170, 60, 161, 136, 34, 123, 55, 234, 14, 107, 85, 31, 183, 141, 78, 6, 132, 204, 61, 17, 8, 187, 126, 186, 129, 25, 201, 255, 233, 45, 107, 154, 31, 129, 253, 187, 216, 57, 72 }, new Guid("d3dcd26d-f563-4cce-b3c6-1fd1e91d6850"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "products",
                columns: new[] { "id", "created_at", "inventory", "product_line_id", "product_size_id", "updated_at" },
                values: new object[,]
                {
                    { new Guid("03227f3f-7618-4eef-b652-4427a392aa85"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 200, new Guid("f70b97ec-9c19-404f-bda1-c41d92c26c1e"), new Guid("ac9d37f1-d707-4065-a67a-fb7874ad4bc9"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("1217218b-a924-4eb0-bf09-e5934fb68b58"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 100, new Guid("3e6c79c9-6a73-40cb-ba02-a1e8a68ad595"), new Guid("fb1c6dfa-052e-4f37-ab06-21e77df227d9"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("2de30bd5-6526-44a5-855c-e2f7a209ff10"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 100, new Guid("d3dcd26d-f563-4cce-b3c6-1fd1e91d6850"), new Guid("fb1c6dfa-052e-4f37-ab06-21e77df227d9"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("318b716c-54ad-48f5-9436-595b02efc705"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 200, new Guid("f70b97ec-9c19-404f-bda1-c41d92c26c1e"), new Guid("b8fc3023-e0c2-40af-9af8-72784fce8183"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("375b0dc8-cbb4-47dc-96ae-5be43aa05da8"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 200, new Guid("f70b97ec-9c19-404f-bda1-c41d92c26c1e"), new Guid("ac9d37f1-d707-4065-a67a-fb7874ad4bc9"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("3a7835b3-cd40-4297-8331-fd14d43c10cb"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 100, new Guid("d3dcd26d-f563-4cce-b3c6-1fd1e91d6850"), new Guid("01f7ce9b-e369-4ac0-9e15-94c8366d30a1"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("5dae6f46-f489-4169-956c-51e22c74ed24"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 100, new Guid("4799b78c-1ae9-403b-ab1d-f1be2b9b096a"), new Guid("fb1c6dfa-052e-4f37-ab06-21e77df227d9"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("6d7db22c-2164-45ce-9f21-1064805cea96"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 100, new Guid("4799b78c-1ae9-403b-ab1d-f1be2b9b096a"), new Guid("01f7ce9b-e369-4ac0-9e15-94c8366d30a1"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("6f2d612c-07d7-4cd0-b883-5d38a6cf8be9"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 100, new Guid("f70b97ec-9c19-404f-bda1-c41d92c26c1e"), new Guid("01f7ce9b-e369-4ac0-9e15-94c8366d30a1"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("7b2632a3-8fa3-43dc-9195-2eb209d852fe"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 200, new Guid("3e6c79c9-6a73-40cb-ba02-a1e8a68ad595"), new Guid("ac9d37f1-d707-4065-a67a-fb7874ad4bc9"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("85ef2bea-5219-4fc6-a913-769355a7cf02"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 100, new Guid("f70b97ec-9c19-404f-bda1-c41d92c26c1e"), new Guid("fb1c6dfa-052e-4f37-ab06-21e77df227d9"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("8ab4e270-f96f-4508-a748-f63004418503"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 200, new Guid("d3dcd26d-f563-4cce-b3c6-1fd1e91d6850"), new Guid("ac9d37f1-d707-4065-a67a-fb7874ad4bc9"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("8c5c456a-3e01-4d15-9b9d-86e4ffd17a84"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 200, new Guid("3e6c79c9-6a73-40cb-ba02-a1e8a68ad595"), new Guid("b8fc3023-e0c2-40af-9af8-72784fce8183"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("a78178bb-cd9b-4ff7-8e60-17b4bd2d3a08"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 200, new Guid("f70b97ec-9c19-404f-bda1-c41d92c26c1e"), new Guid("b8fc3023-e0c2-40af-9af8-72784fce8183"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("cd78c484-fbcd-4a99-b825-1a296807a35e"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 200, new Guid("4799b78c-1ae9-403b-ab1d-f1be2b9b096a"), new Guid("b8fc3023-e0c2-40af-9af8-72784fce8183"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("d3fc767b-0496-4e9e-9f5b-9ec8aad3f84f"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 200, new Guid("4799b78c-1ae9-403b-ab1d-f1be2b9b096a"), new Guid("ac9d37f1-d707-4065-a67a-fb7874ad4bc9"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("df4eeea4-8720-4822-81e7-4cf68ad2d103"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 100, new Guid("f70b97ec-9c19-404f-bda1-c41d92c26c1e"), new Guid("fb1c6dfa-052e-4f37-ab06-21e77df227d9"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("e458a623-67f3-417f-a948-72bb39afd7d4"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 100, new Guid("3e6c79c9-6a73-40cb-ba02-a1e8a68ad595"), new Guid("01f7ce9b-e369-4ac0-9e15-94c8366d30a1"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("f70d1ef4-4f35-4f99-97f3-277ede14d082"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 200, new Guid("d3dcd26d-f563-4cce-b3c6-1fd1e91d6850"), new Guid("b8fc3023-e0c2-40af-9af8-72784fce8183"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("f9b5da43-3dde-4b20-9ea7-143bed393644"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 100, new Guid("f70b97ec-9c19-404f-bda1-c41d92c26c1e"), new Guid("01f7ce9b-e369-4ac0-9e15-94c8366d30a1"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
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
