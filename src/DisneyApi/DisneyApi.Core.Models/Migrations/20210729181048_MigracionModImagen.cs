using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DisneyApi.Core.Models.Migrations
{
    public partial class MigracionModImagen : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Genero_Imagen_ImagenId",
                table: "Genero");

            migrationBuilder.DropForeignKey(
                name: "FK_PeliculaSerie_Imagen_ImagenId",
                table: "PeliculaSerie");

            migrationBuilder.DropForeignKey(
                name: "FK_Personaje_Imagen_ImagenId",
                table: "Personaje");

            migrationBuilder.DropTable(
                name: "Imagen");

            migrationBuilder.DropIndex(
                name: "IX_Personaje_ImagenId",
                table: "Personaje");

            migrationBuilder.DropIndex(
                name: "IX_PeliculaSerie_ImagenId",
                table: "PeliculaSerie");

            migrationBuilder.DropIndex(
                name: "IX_Genero_ImagenId",
                table: "Genero");

            migrationBuilder.DropColumn(
                name: "ImagenId",
                table: "Personaje");

            migrationBuilder.DropColumn(
                name: "ImagenId",
                table: "PeliculaSerie");

            migrationBuilder.DropColumn(
                name: "ImagenId",
                table: "Genero");

            migrationBuilder.AddColumn<string>(
                name: "ImagenTitulo",
                table: "Personaje",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImagenUrl",
                table: "Personaje",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImagenTitulo",
                table: "PeliculaSerie",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImagenUrl",
                table: "PeliculaSerie",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImagenTitulo",
                table: "Genero",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImagenUrl",
                table: "Genero",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagenTitulo",
                table: "Personaje");

            migrationBuilder.DropColumn(
                name: "ImagenUrl",
                table: "Personaje");

            migrationBuilder.DropColumn(
                name: "ImagenTitulo",
                table: "PeliculaSerie");

            migrationBuilder.DropColumn(
                name: "ImagenUrl",
                table: "PeliculaSerie");

            migrationBuilder.DropColumn(
                name: "ImagenTitulo",
                table: "Genero");

            migrationBuilder.DropColumn(
                name: "ImagenUrl",
                table: "Genero");

            migrationBuilder.AddColumn<int>(
                name: "ImagenId",
                table: "Personaje",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ImagenId",
                table: "PeliculaSerie",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ImagenId",
                table: "Genero",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Imagen",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContentType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Contentt = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    FileType = table.Column<int>(type: "int", nullable: false),
                    NombreImagen = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Imagen", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Personaje_ImagenId",
                table: "Personaje",
                column: "ImagenId");

            migrationBuilder.CreateIndex(
                name: "IX_PeliculaSerie_ImagenId",
                table: "PeliculaSerie",
                column: "ImagenId");

            migrationBuilder.CreateIndex(
                name: "IX_Genero_ImagenId",
                table: "Genero",
                column: "ImagenId");

            migrationBuilder.AddForeignKey(
                name: "FK_Genero_Imagen_ImagenId",
                table: "Genero",
                column: "ImagenId",
                principalTable: "Imagen",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PeliculaSerie_Imagen_ImagenId",
                table: "PeliculaSerie",
                column: "ImagenId",
                principalTable: "Imagen",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Personaje_Imagen_ImagenId",
                table: "Personaje",
                column: "ImagenId",
                principalTable: "Imagen",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
