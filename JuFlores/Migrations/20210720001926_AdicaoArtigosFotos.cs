using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace JuFlores.Migrations
{
    public partial class AdicaoArtigosFotos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ArtigosFotos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArtigoFk = table.Column<int>(type: "int", nullable: false),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FotografiaFK = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArtigosFotos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ArtigosFotos_Artigos_ArtigoFk",
                        column: x => x.ArtigoFk,
                        principalTable: "Artigos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArtigosFotos_Fotografias_FotografiaFK",
                        column: x => x.FotografiaFK,
                        principalTable: "Fotografias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a",
                column: "ConcurrencyStamp",
                value: "adcfa539-cf9a-4916-8fe5-10fcc3bb92e1");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c",
                column: "ConcurrencyStamp",
                value: "3275fb16-9ae5-4d2a-8fd7-ca02755646f7");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f",
                column: "ConcurrencyStamp",
                value: "914270d3-5a01-4373-bdb6-09aff89aeaa4");

            migrationBuilder.CreateIndex(
                name: "IX_ArtigosFotos_ArtigoFk",
                table: "ArtigosFotos",
                column: "ArtigoFk");

            migrationBuilder.CreateIndex(
                name: "IX_ArtigosFotos_FotografiaFK",
                table: "ArtigosFotos",
                column: "FotografiaFK");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArtigosFotos");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a",
                column: "ConcurrencyStamp",
                value: "d4f6b48c-dc81-411e-b3c6-12ffe94e8356");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c",
                column: "ConcurrencyStamp",
                value: "afe968b3-3fde-4ea5-9602-9b12340cbdb8");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f",
                column: "ConcurrencyStamp",
                value: "212f2738-466f-4c52-af4f-3569a11aacd3");
        }
    }
}
