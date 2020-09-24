using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ClothingStoreFranchise.NetCore.Catalog.Migrations.CatalogMigrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Offers",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Discount = table.Column<decimal>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    CatalogProductId = table.Column<long>(nullable: true),
                    CategoryId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Offers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CatalogProducts",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Stock = table.Column<int>(nullable: false),
                    UnitPrice = table.Column<decimal>(nullable: false),
                    PictureUrl = table.Column<string>(nullable: true),
                    CurrentOfferId = table.Column<long>(nullable: true),
                    CatalogProductId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatalogProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CatalogProducts_Offers_CurrentOfferId",
                        column: x => x.CurrentOfferId,
                        principalTable: "Offers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    Nombre = table.Column<string>(nullable: true),
                    CategoryBelonging = table.Column<long>(nullable: true),
                    CurrentOfferId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Categories_Offers_CurrentOfferId",
                        column: x => x.CurrentOfferId,
                        principalTable: "Offers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CatalogProducts_CurrentOfferId",
                table: "CatalogProducts",
                column: "CurrentOfferId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_CurrentOfferId",
                table: "Categories",
                column: "CurrentOfferId");

            migrationBuilder.CreateIndex(
                name: "IX_Offers_CatalogProductId",
                table: "Offers",
                column: "CatalogProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Offers_CategoryId",
                table: "Offers",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Offers_CatalogProducts_CatalogProductId",
                table: "Offers",
                column: "CatalogProductId",
                principalTable: "CatalogProducts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Offers_Categories_CategoryId",
                table: "Offers",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CatalogProducts_Offers_CurrentOfferId",
                table: "CatalogProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Offers_CurrentOfferId",
                table: "Categories");

            migrationBuilder.DropTable(
                name: "Offers");

            migrationBuilder.DropTable(
                name: "CatalogProducts");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
