using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVCGaleno.Migrations
{
    /// <inheritdoc />
    public partial class inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Afiliados",
                columns: table => new
                {
                    IdAfiliado = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Dni = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    tipoPlan = table.Column<int>(type: "int", nullable: false),
                    mail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NombreCompleto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    telefono = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Afiliados", x => x.IdAfiliado);
                });

            migrationBuilder.CreateTable(
                name: "Laboratorio",
                columns: table => new
                {
                    IdLaboratorio = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdPrestador = table.Column<int>(type: "int", nullable: false),
                    IdAfiliado = table.Column<int>(type: "int", nullable: false),
                    RutaArchivo = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Laboratorio", x => x.IdLaboratorio);
                });

            migrationBuilder.CreateTable(
                name: "Medicos",
                columns: table => new
                {
                    IdPrestador = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Especialidad = table.Column<int>(type: "int", nullable: false),
                    NombreCompleto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MatriculaProfesional = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MailMedico = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DireccionMedico = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TelefonoMedico = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medicos", x => x.IdPrestador);
                });

            migrationBuilder.CreateTable(
                name: "Citas",
                columns: table => new
                {
                    IdCita = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fechaCita = table.Column<DateTime>(type: "datetime2", nullable: false),
                    estaDisponible = table.Column<bool>(type: "bit", nullable: false),
                    IdPrestador = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Citas", x => x.IdCita);
                    table.ForeignKey(
                        name: "FK_Citas_Medicos_IdPrestador",
                        column: x => x.IdPrestador,
                        principalTable: "Medicos",
                        principalColumn: "IdPrestador",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Turnos",
                columns: table => new
                {
                    IdTurno = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Especialidad = table.Column<int>(type: "int", nullable: false),
                    PrestadorMedicoIdPrestador = table.Column<int>(type: "int", nullable: false),
                    AfiliadoIdAfiliado = table.Column<int>(type: "int", nullable: false),
                    fechaCita = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Turnos", x => x.IdTurno);
                    table.ForeignKey(
                        name: "FK_Turnos_Afiliados_AfiliadoIdAfiliado",
                        column: x => x.AfiliadoIdAfiliado,
                        principalTable: "Afiliados",
                        principalColumn: "IdAfiliado",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Turnos_Medicos_PrestadorMedicoIdPrestador",
                        column: x => x.PrestadorMedicoIdPrestador,
                        principalTable: "Medicos",
                        principalColumn: "IdPrestador",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Citas_IdPrestador",
                table: "Citas",
                column: "IdPrestador");

            migrationBuilder.CreateIndex(
                name: "IX_Turnos_AfiliadoIdAfiliado",
                table: "Turnos",
                column: "AfiliadoIdAfiliado");

            migrationBuilder.CreateIndex(
                name: "IX_Turnos_PrestadorMedicoIdPrestador",
                table: "Turnos",
                column: "PrestadorMedicoIdPrestador");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Citas");

            migrationBuilder.DropTable(
                name: "Laboratorio");

            migrationBuilder.DropTable(
                name: "Turnos");

            migrationBuilder.DropTable(
                name: "Afiliados");

            migrationBuilder.DropTable(
                name: "Medicos");
        }
    }
}
