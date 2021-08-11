using Microsoft.EntityFrameworkCore.Migrations;

namespace DisneyApi.Core.Models.Migrations
{
    public partial class CodeFirstRefactor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PeliculaSerie_Genero_IdGenero",
                table: "PeliculaSerie");

            migrationBuilder.DropTable(
                name: "PersonajePeliculaSeries");

            migrationBuilder.DropIndex(
                name: "IX_PeliculaSerie_IdGenero",
                table: "PeliculaSerie");

            migrationBuilder.AddColumn<int>(
                name: "GeneroId",
                table: "PeliculaSerie",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PeliculaSeriePersonaje",
                columns: table => new
                {
                    PeliculasSeriesId = table.Column<int>(type: "int", nullable: false),
                    PersonajesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PeliculaSeriePersonaje", x => new { x.PeliculasSeriesId, x.PersonajesId });
                    table.ForeignKey(
                        name: "FK_PeliculaSeriePersonaje_PeliculaSerie_PeliculasSeriesId",
                        column: x => x.PeliculasSeriesId,
                        principalTable: "PeliculaSerie",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PeliculaSeriePersonaje_Personaje_PersonajesId",
                        column: x => x.PersonajesId,
                        principalTable: "Personaje",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PeliculaSerie_GeneroId",
                table: "PeliculaSerie",
                column: "GeneroId");

            migrationBuilder.CreateIndex(
                name: "IX_PeliculaSeriePersonaje_PersonajesId",
                table: "PeliculaSeriePersonaje",
                column: "PersonajesId");

            migrationBuilder.AddForeignKey(
                name: "FK_PeliculaSerie_Genero_GeneroId",
                table: "PeliculaSerie",
                column: "GeneroId",
                principalTable: "Genero",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PeliculaSerie_Genero_GeneroId",
                table: "PeliculaSerie");

            migrationBuilder.DropTable(
                name: "PeliculaSeriePersonaje");

            migrationBuilder.DropIndex(
                name: "IX_PeliculaSerie_GeneroId",
                table: "PeliculaSerie");

            migrationBuilder.DropColumn(
                name: "GeneroId",
                table: "PeliculaSerie");

            migrationBuilder.CreateTable(
                name: "PersonajePeliculaSeries",
                columns: table => new
                {
                    IdPersonaje = table.Column<int>(type: "int", nullable: false),
                    IdPeliculaSerie = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonajePeliculaSeries", x => new { x.IdPersonaje, x.IdPeliculaSerie });
                    table.ForeignKey(
                        name: "FK_PersonajePeliculaSeries_PeliculaSerie_IdPeliculaSerie",
                        column: x => x.IdPeliculaSerie,
                        principalTable: "PeliculaSerie",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonajePeliculaSeries_Personaje_IdPersonaje",
                        column: x => x.IdPersonaje,
                        principalTable: "Personaje",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PeliculaSerie_IdGenero",
                table: "PeliculaSerie",
                column: "IdGenero");

            migrationBuilder.CreateIndex(
                name: "IX_PersonajePeliculaSeries_IdPeliculaSerie",
                table: "PersonajePeliculaSeries",
                column: "IdPeliculaSerie");

            migrationBuilder.AddForeignKey(
                name: "FK_PeliculaSerie_Genero_IdGenero",
                table: "PeliculaSerie",
                column: "IdGenero",
                principalTable: "Genero",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
