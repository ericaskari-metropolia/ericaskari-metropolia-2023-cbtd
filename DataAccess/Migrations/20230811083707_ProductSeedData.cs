using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ProductSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "image_url",
                table: "product",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "product",
                columns: new[] { "id", "category_id", "description", "dozen_price", "half_dozen_price", "image_url", "list_price", "manufacturer_id", "name", "size", "upc", "unit_price" },
                values: new object[,]
                {
                    { 1, 1, "The primary taste of Coca-Cola is thought to come from vanilla and cinnamon, with trace amounts of essential oils, and spices such as nutmeg.", 0.98999999999999999, 1.24, "/images/products/Coke.jpg", 1.99, 1, "Coca Cola Classic", "33cl", "4894034", 1.49 },
                    { 2, 2, "<p>The Yellow Tail Shiraz has a deep red color with bright purple hues, characteristic of fine young wines. It displays impressive <strong>spice, licorice, and black currant aromas</strong>. The palate is perfectly balanced with soft tannins and fine French Oak, further complemented by ripe fruit flavors.</p>", 6.9900000000000002, 7.9900000000000002, "/images/products/YellowTail.png", 9.9900000000000002, 2, "Yellow Tail Shiraz", "750 ml", "031259008943", 8.9900000000000002 },
                    { 3, 2, "Menage a Trois California Red Blend exposes the fresh, ripe, jam-like fruit that is the calling card of California wine. Forward, spicy and soft, this delicious dalliance makes the perfect accompaniment for grilled meats or chicken.", 9.9900000000000002, 10.75, "/images/products/menage.jpg", 12.99, 3, "Menage a Trois Merlot", "750 ml", "099988071096", 11.49 },
                    { 4, 3, "The chip that packs a flavorful punch with the classic crunch. Boldly seasoned with three cheeses, tomatoes, onions, and a savory blend of spices. Indulge yourself or share with large gatherings", 0.68999999999999995, 1.05, "/images/products/doritos.jpg", 1.99, 4, "Doritos", "175 grams", "028400443753", 1.49 },
                    { 5, 3, "The fun, crunchy snack that is made with real cheese. Packed with flavor that satisfies. Always a crowd favorite.", 0.68999999999999995, 1.05, "/images/products/cheetos.jpg", 1.99, 4, "Cheetos", "200 grams", "028400443661", 1.49 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "product",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "product",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "product",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "product",
                keyColumn: "id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "product",
                keyColumn: "id",
                keyValue: 5);

            migrationBuilder.UpdateData(
                table: "product",
                keyColumn: "image_url",
                keyValue: null,
                column: "image_url",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "image_url",
                table: "product",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }
    }
}
