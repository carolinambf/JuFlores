using Microsoft.EntityFrameworkCore.Migrations;

namespace JuFlores.Migrations
{
    public partial class Update1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PecasArtigos",
                columns: table => new
                {
                    PecaFK = table.Column<int>(type: "int", nullable: false),
                    Qtd = table.Column<float>(type: "real", nullable: false),
                    ArtigoFK = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PecasArtigos", x => x.PecaFK);
                    table.ForeignKey(
                        name: "FK_PecasArtigos_Artigos_ArtigoFK",
                        column: x => x.ArtigoFK,
                        principalTable: "Artigos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PecasArtigos_Pecas_PecaFK",
                        column: x => x.PecaFK,
                        principalTable: "Pecas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PecasArtigos_ArtigoFK",
                table: "PecasArtigos",
                column: "ArtigoFK");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PecasArtigos");
        }
    }
}
