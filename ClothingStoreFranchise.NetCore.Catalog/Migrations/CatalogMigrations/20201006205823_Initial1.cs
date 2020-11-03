using Microsoft.EntityFrameworkCore.Migrations;

namespace ClothingStoreFranchise.NetCore.Catalog.Migrations.CatalogMigrations
{
    public partial class Initial1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CatalogProducts_Categories_SubategoryId",
                table: "CatalogProducts");

            migrationBuilder.DropIndex(
                name: "IX_CatalogProducts_SubategoryId",
                table: "CatalogProducts");

            migrationBuilder.DropColumn(
                name: "SubategoryId",
                table: "CatalogProducts");

            migrationBuilder.AddColumn<long>(
                name: "SubcategoryId",
                table: "CatalogProducts",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_CatalogProducts_SubcategoryId",
                table: "CatalogProducts",
                column: "SubcategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_CatalogProducts_Categories_SubcategoryId",
                table: "CatalogProducts",
                column: "SubcategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CatalogProducts_Categories_SubcategoryId",
                table: "CatalogProducts");

            migrationBuilder.DropIndex(
                name: "IX_CatalogProducts_SubcategoryId",
                table: "CatalogProducts");

            migrationBuilder.DropColumn(
                name: "SubcategoryId",
                table: "CatalogProducts");

            migrationBuilder.AddColumn<long>(
                name: "SubategoryId",
                table: "CatalogProducts",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_CatalogProducts_SubategoryId",
                table: "CatalogProducts",
                column: "SubategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_CatalogProducts_Categories_SubategoryId",
                table: "CatalogProducts",
                column: "SubategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
