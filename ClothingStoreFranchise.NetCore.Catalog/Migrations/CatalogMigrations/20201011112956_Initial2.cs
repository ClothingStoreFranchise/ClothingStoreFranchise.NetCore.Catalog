using Microsoft.EntityFrameworkCore.Migrations;

namespace ClothingStoreFranchise.NetCore.Catalog.Migrations.CatalogMigrations
{
    public partial class Initial2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TypeClothingSize",
                table: "Categories",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TypeClothingSize",
                table: "Categories");
        }
    }
}
