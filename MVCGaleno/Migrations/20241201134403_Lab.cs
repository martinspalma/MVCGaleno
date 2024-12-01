using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVCGaleno.Migrations
{
    /// <inheritdoc />
    public partial class Lab : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Laboratorio");
        }
    }
}
