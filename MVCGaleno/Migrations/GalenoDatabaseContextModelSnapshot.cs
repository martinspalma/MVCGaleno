﻿// <auto-generated />
using System;
using MVCGaleno.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MVCGaleno.Migrations
{
    [DbContext(typeof(GalenoDatabaseContext))]
    partial class GalenoDatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MVCGaleno.Models.Afiliado", b =>
                {
                    b.Property<int>("IdAfiliado")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdAfiliado"));

                    b.Property<string>("Dni")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NombreCompleto")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("mail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("telefono")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("tipoPlan")
                        .HasColumnType("int");

                    b.HasKey("IdAfiliado");

                    b.ToTable("Afiliados");
                });

            modelBuilder.Entity("MVCGaleno.Models.Cita", b =>
                {
                    b.Property<int>("IdCita")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdCita"));

                    b.Property<int>("IdPrestador")
                        .HasColumnType("int");

                    b.Property<bool>("estaDisponible")
                        .HasColumnType("bit");

                    b.Property<DateTime>("fechaCita")
                        .HasColumnType("datetime2");

                    b.HasKey("IdCita");

                    b.HasIndex("IdPrestador");

                    b.ToTable("Citas");
                });

            modelBuilder.Entity("MVCGaleno.Models.Laboratorio", b =>
                {
                    b.Property<int>("IdLaboratorio")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdLaboratorio"));

                    b.Property<int>("IdAfiliado")
                        .HasColumnType("int");

                    b.Property<int>("IdPrestador")
                        .HasColumnType("int");

                    b.Property<string>("RutaArchivo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdLaboratorio");

                    b.ToTable("Laboratorio");
                });

            modelBuilder.Entity("MVCGaleno.Models.PrestadorMedico", b =>
                {
                    b.Property<int>("IdPrestador")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdPrestador"));

                    b.Property<string>("DireccionMedico")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Especialidad")
                        .HasColumnType("int");

                    b.Property<string>("MailMedico")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MatriculaProfesional")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NombreCompleto")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TelefonoMedico")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdPrestador");

                    b.ToTable("Medicos");
                });

            modelBuilder.Entity("MVCGaleno.Models.Turno", b =>
                {
                    b.Property<int>("IdTurno")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdTurno"));

                    b.Property<int>("AfiliadoIdAfiliado")
                        .HasColumnType("int");

                    b.Property<int>("Especialidad")
                        .HasColumnType("int");

                    b.Property<int>("PrestadorMedicoIdPrestador")
                        .HasColumnType("int");

                    b.Property<DateTime?>("fechaCita")
                        .HasColumnType("datetime2");

                    b.HasKey("IdTurno");

                    b.HasIndex("AfiliadoIdAfiliado");

                    b.HasIndex("PrestadorMedicoIdPrestador");

                    b.ToTable("Turnos");
                });

            modelBuilder.Entity("MVCGaleno.Models.Cita", b =>
                {
                    b.HasOne("MVCGaleno.Models.PrestadorMedico", "PrestadorMedico")
                        .WithMany()
                        .HasForeignKey("IdPrestador")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PrestadorMedico");
                });

            modelBuilder.Entity("MVCGaleno.Models.Turno", b =>
                {
                    b.HasOne("MVCGaleno.Models.Afiliado", "Afiliado")
                        .WithMany()
                        .HasForeignKey("AfiliadoIdAfiliado")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MVCGaleno.Models.PrestadorMedico", "PrestadorMedico")
                        .WithMany()
                        .HasForeignKey("PrestadorMedicoIdPrestador")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Afiliado");

                    b.Navigation("PrestadorMedico");
                });
#pragma warning restore 612, 618
        }
    }
}
