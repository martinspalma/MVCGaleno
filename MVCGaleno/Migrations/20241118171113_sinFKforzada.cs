using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVCGaleno.Migrations
{
    /// <inheritdoc />
    public partial class sinFKforzada : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Citas_Medicos_IdPrestador",
                table: "Citas");

            migrationBuilder.DropIndex(
                name: "IX_Citas_IdPrestador",
                table: "Citas");

            migrationBuilder.AddColumn<int>(
                name: "PrestadorMedicoIdPrestador",
                table: "Citas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Citas_PrestadorMedicoIdPrestador",
                table: "Citas",
                column: "PrestadorMedicoIdPrestador");

            migrationBuilder.AddForeignKey(
                name: "FK_Citas_Medicos_PrestadorMedicoIdPrestador",
                table: "Citas",
                column: "PrestadorMedicoIdPrestador",
                principalTable: "Medicos",
                principalColumn: "IdPrestador",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Citas_Medicos_PrestadorMedicoIdPrestador",
                table: "Citas");

            migrationBuilder.DropIndex(
                name: "IX_Citas_PrestadorMedicoIdPrestador",
                table: "Citas");

            migrationBuilder.DropColumn(
                name: "PrestadorMedicoIdPrestador",
                table: "Citas");

            migrationBuilder.CreateIndex(
                name: "IX_Citas_IdPrestador",
                table: "Citas",
                column: "IdPrestador");

            migrationBuilder.AddForeignKey(
                name: "FK_Citas_Medicos_IdPrestador",
                table: "Citas",
                column: "IdPrestador",
                principalTable: "Medicos",
                principalColumn: "IdPrestador",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
