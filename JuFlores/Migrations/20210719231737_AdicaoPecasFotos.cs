using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace JuFlores.Migrations
{
    public partial class AdicaoPecasFotos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PecasFotos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PecaFk = table.Column<int>(type: "int", nullable: false),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FotografiaFK = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PecasFotos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PecasFotos_Fotografias_FotografiaFK",
                        column: x => x.FotografiaFK,
                        principalTable: "Fotografias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PecasFotos_Pecas_PecaFk",
                        column: x => x.PecaFk,
                        principalTable: "Pecas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_PecasFotos_FotografiaFK",
                table: "PecasFotos",
                column: "FotografiaFK");

            migrationBuilder.CreateIndex(
                name: "IX_PecasFotos_PecaFk",
                table: "PecasFotos",
                column: "PecaFk");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PecasFotos");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a",
                column: "ConcurrencyStamp",
                value: "cf94b069-05e1-4893-9a87-9d006a036cc5");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c",
                column: "ConcurrencyStamp",
                value: "d08f2b37-8153-429b-8a6b-abf05872afb0");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f",
                column: "ConcurrencyStamp",
                value: "792815bc-d173-4d9f-815a-6239d4eb3c3c");
        }
    }
}
