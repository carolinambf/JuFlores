using Microsoft.EntityFrameworkCore.Migrations;

namespace JuFlores.Migrations
{
    public partial class CorrecaoPecasArtigos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fotografias_Pecas_PecasId",
                table: "Fotografias");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PecasArtigos",
                table: "PecasArtigos");

            migrationBuilder.DropIndex(
                name: "IX_Fotografias_PecasId",
                table: "Fotografias");

            migrationBuilder.DropColumn(
                name: "PecasId",
                table: "Fotografias");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "PecasArtigos",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PecasArtigos",
                table: "PecasArtigos",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a",
                column: "ConcurrencyStamp",
                value: "2df28959-e980-474e-9011-90b3e7da1982");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c",
                column: "ConcurrencyStamp",
                value: "81a4632c-cda5-4e3d-985c-0974cf7ee625");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f",
                column: "ConcurrencyStamp",
                value: "10bf2125-f3f6-4e5b-9e19-cee2ba60e5ec");

            migrationBuilder.CreateIndex(
                name: "IX_PecasArtigos_PecaFK",
                table: "PecasArtigos",
                column: "PecaFK");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PecasArtigos",
                table: "PecasArtigos");

            migrationBuilder.DropIndex(
                name: "IX_PecasArtigos_PecaFK",
                table: "PecasArtigos");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "PecasArtigos");

            migrationBuilder.AddColumn<int>(
                name: "PecasId",
                table: "Fotografias",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PecasArtigos",
                table: "PecasArtigos",
                column: "PecaFK");

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
                name: "IX_Fotografias_PecasId",
                table: "Fotografias",
                column: "PecasId");

            migrationBuilder.AddForeignKey(
                name: "FK_Fotografias_Pecas_PecasId",
                table: "Fotografias",
                column: "PecasId",
                principalTable: "Pecas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
