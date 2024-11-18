using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVCGaleno.Migrations
{
    /// <inheritdoc />
    public partial class mil : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            
           migrationBuilder.DropForeignKey(
                name: "FK_Citas_Medicos_prestadorMedicoIdPrestador",
                table: "Citas");

            migrationBuilder.RenameColumn(
                name: "prestadorMedicoIdPrestador",
                table: "Citas",
                newName: "PrestadorMedicoIdPrestador");

            migrationBuilder.RenameIndex(
                name: "IX_Citas_prestadorMedicoIdPrestador",
                table: "Citas",
                newName: "IX_Citas_PrestadorMedicoIdPrestador");

            migrationBuilder.AddColumn<int>(
                name: "Especialidad",
                table: "Turnos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "PrestadorMedicoIdPrestador",
                table: "Citas",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "IdPrestador",
                table: "Citas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Citas_Medicos_PrestadorMedicoIdPrestador",
                table: "Citas",
                column: "PrestadorMedicoIdPrestador",
                principalTable: "Medicos",
                principalColumn: "IdPrestador");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Citas_Medicos_PrestadorMedicoIdPrestador",
                table: "Citas");

            migrationBuilder.DropColumn(
                name: "Especialidad",
                table: "Turnos");

            migrationBuilder.DropColumn(
                name: "IdPrestador",
                table: "Citas");

            migrationBuilder.RenameColumn(
                name: "PrestadorMedicoIdPrestador",
                table: "Citas",
                newName: "prestadorMedicoIdPrestador");

            migrationBuilder.RenameIndex(
                name: "IX_Citas_PrestadorMedicoIdPrestador",
                table: "Citas",
                newName: "IX_Citas_prestadorMedicoIdPrestador");

            migrationBuilder.AlterColumn<int>(
                name: "prestadorMedicoIdPrestador",
                table: "Citas",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Citas_Medicos_prestadorMedicoIdPrestador",
                table: "Citas",
                column: "prestadorMedicoIdPrestador",
                principalTable: "Medicos",
                principalColumn: "IdPrestador",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
