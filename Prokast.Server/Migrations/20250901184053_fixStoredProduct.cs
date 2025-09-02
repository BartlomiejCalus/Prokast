using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Prokast.Server.Migrations
{
    /// <inheritdoc />
    public partial class fixStoredProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_StoredProducts_ProductID",
                table: "StoredProducts");

            migrationBuilder.CreateIndex(
                name: "IX_StoredProducts_ProductID",
                table: "StoredProducts",
                column: "ProductID",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_StoredProducts_ProductID",
                table: "StoredProducts");

            migrationBuilder.CreateIndex(
                name: "IX_StoredProducts_ProductID",
                table: "StoredProducts",
                column: "ProductID");
        }
    }
}
