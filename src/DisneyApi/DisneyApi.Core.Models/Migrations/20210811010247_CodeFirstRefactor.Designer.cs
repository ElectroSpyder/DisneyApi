﻿// <auto-generated />
using System;
using DisneyApi.Core.Models.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DisneyApi.Core.Models.Migrations
{
    [DbContext(typeof(DisneyDBContext))]
    [Migration("20210811010247_CodeFirstRefactor")]
    partial class CodeFirstRefactor
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.8")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DisneyApi.Core.Models.Entities.Genero", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ImagenTitulo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImagenUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Genero");
                });

            modelBuilder.Entity("DisneyApi.Core.Models.Entities.PeliculaSerie", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Calificacion")
                        .HasColumnType("int");

                    b.Property<DateTime?>("FechaCreacion")
                        .HasColumnType("datetime2");

                    b.Property<int?>("GeneroId")
                        .HasColumnType("int");

                    b.Property<int>("IdGenero")
                        .HasColumnType("int");

                    b.Property<string>("ImagenTitulo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImagenUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.HasKey("Id");

                    b.HasIndex("GeneroId");

                    b.ToTable("PeliculaSerie");
                });

            modelBuilder.Entity("DisneyApi.Core.Models.Entities.Personaje", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Edad")
                        .HasColumnType("int");

                    b.Property<string>("Historia")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("ImagenTitulo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImagenUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<double>("Peso")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("Personaje");
                });

            modelBuilder.Entity("PeliculaSeriePersonaje", b =>
                {
                    b.Property<int>("PeliculasSeriesId")
                        .HasColumnType("int");

                    b.Property<int>("PersonajesId")
                        .HasColumnType("int");

                    b.HasKey("PeliculasSeriesId", "PersonajesId");

                    b.HasIndex("PersonajesId");

                    b.ToTable("PeliculaSeriePersonaje");
                });

            modelBuilder.Entity("DisneyApi.Core.Models.Entities.PeliculaSerie", b =>
                {
                    b.HasOne("DisneyApi.Core.Models.Entities.Genero", "Genero")
                        .WithMany("PeliculaSeries")
                        .HasForeignKey("GeneroId");

                    b.Navigation("Genero");
                });

            modelBuilder.Entity("PeliculaSeriePersonaje", b =>
                {
                    b.HasOne("DisneyApi.Core.Models.Entities.PeliculaSerie", null)
                        .WithMany()
                        .HasForeignKey("PeliculasSeriesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DisneyApi.Core.Models.Entities.Personaje", null)
                        .WithMany()
                        .HasForeignKey("PersonajesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DisneyApi.Core.Models.Entities.Genero", b =>
                {
                    b.Navigation("PeliculaSeries");
                });
#pragma warning restore 612, 618
        }
    }
}