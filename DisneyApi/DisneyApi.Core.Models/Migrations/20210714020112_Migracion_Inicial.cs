using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DisneyApi.Core.Models.Migrations
{
    public partial class Migracion_Inicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Imagen",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreImagen = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ContentType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Contentt = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    FileType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Imagen", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Genero",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImagenId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genero", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Genero_Imagen_ImagenId",
                        column: x => x.ImagenId,
                        principalTable: "Imagen",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Personaje",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Edad = table.Column<int>(type: "int", nullable: false),
                    Peso = table.Column<double>(type: "float", nullable: false),
                    Historia = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    ImagenId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personaje", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Personaje_Imagen_ImagenId",
                        column: x => x.ImagenId,
                        principalTable: "Imagen",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PeliculaSerie",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Calificacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdGenero = table.Column<int>(type: "int", nullable: false),
                    ImagenId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PeliculaSerie", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PeliculaSerie_Genero_IdGenero",
                        column: x => x.IdGenero,
                        principalTable: "Genero",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PeliculaSerie_Imagen_ImagenId",
                        column: x => x.ImagenId,
                        principalTable: "Imagen",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

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
                name: "IX_Genero_ImagenId",
                table: "Genero",
                column: "ImagenId");

            migrationBuilder.CreateIndex(
                name: "IX_PeliculaSerie_IdGenero",
                table: "PeliculaSerie",
                column: "IdGenero");

            migrationBuilder.CreateIndex(
                name: "IX_PeliculaSerie_ImagenId",
                table: "PeliculaSerie",
                column: "ImagenId");

            migrationBuilder.CreateIndex(
                name: "IX_Personaje_ImagenId",
                table: "Personaje",
                column: "ImagenId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonajePeliculaSeries_IdPeliculaSerie",
                table: "PersonajePeliculaSeries",
                column: "IdPeliculaSerie");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PersonajePeliculaSeries");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "PeliculaSerie");

            migrationBuilder.DropTable(
                name: "Personaje");

            migrationBuilder.DropTable(
                name: "Genero");

            migrationBuilder.DropTable(
                name: "Imagen");
        }
    }
}
