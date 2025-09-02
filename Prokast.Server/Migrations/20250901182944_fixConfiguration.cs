using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Prokast.Server.Migrations
{
    /// <inheritdoc />
    public partial class fixConfiguration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_StoredProducts_StoredProductID",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_StoredProductID",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "StoredProductID",
                table: "Products");

            migrationBuilder.AddColumn<int>(
                name: "ProductID",
                table: "StoredProducts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_StoredProducts_ProductID",
                table: "StoredProducts",
                column: "ProductID");

            migrationBuilder.AddForeignKey(
                name: "FK_StoredProducts_Products_ProductID",
                table: "StoredProducts",
                column: "ProductID",
                principalTable: "Products",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StoredProducts_Products_ProductID",
                table: "StoredProducts");

            migrationBuilder.DropIndex(
                name: "IX_StoredProducts_ProductID",
                table: "StoredProducts");

            migrationBuilder.DropColumn(
                name: "ProductID",
                table: "StoredProducts");

            migrationBuilder.AddColumn<int>(
                name: "StoredProductID",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_StoredProductID",
                table: "Products",
                column: "StoredProductID",
                unique: true,
                filter: "[StoredProductID] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_StoredProducts_StoredProductID",
                table: "Products",
                column: "StoredProductID",
                principalTable: "StoredProducts",
                principalColumn: "ID");
        }
    }
}
