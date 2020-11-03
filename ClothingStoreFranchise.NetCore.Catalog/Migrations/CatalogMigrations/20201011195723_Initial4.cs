using Microsoft.EntityFrameworkCore.Migrations;

namespace ClothingStoreFranchise.NetCore.Catalog.Migrations.CatalogMigrations
{
    public partial class Initial4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CatalogProductId",
                table: "CatalogProducts");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CatalogProductId",
                table: "CatalogProducts",
                type: "bigint",
                nullable: true);
        }
    }
}
