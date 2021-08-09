using Microsoft.EntityFrameworkCore.Migrations;

namespace DisneyApi.Core.Models.Migrations
{
    public partial class ActualizaClases : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Personaje_Genero_GeneroId",
                table: "Personaje");

            migrationBuilder.DropIndex(
                name: "IX_Personaje_GeneroId",
                table: "Personaje");

            migrationBuilder.DropColumn(
                name: "GeneroId",
                table: "Personaje");

            migrationBuilder.DropColumn(
                name: "IdGenero",
                table: "Personaje");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GeneroId",
                table: "Personaje",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IdGenero",
                table: "Personaje",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Personaje_GeneroId",
                table: "Personaje",
                column: "GeneroId");

            migrationBuilder.AddForeignKey(
                name: "FK_Personaje_Genero_GeneroId",
                table: "Personaje",
                column: "GeneroId",
                principalTable: "Genero",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
