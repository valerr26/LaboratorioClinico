using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LaboratorioClinico.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class QuiteCitaIdresultado : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_t_resultado_t_cita_CitaId",
                table: "t_resultado");

            migrationBuilder.DropIndex(
                name: "IX_t_resultado_CitaId",
                table: "t_resultado");

            migrationBuilder.DropColumn(
                name: "CitaId",
                table: "t_resultado");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CitaId",
                table: "t_resultado",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_t_resultado_CitaId",
                table: "t_resultado",
                column: "CitaId");

            migrationBuilder.AddForeignKey(
                name: "FK_t_resultado_t_cita_CitaId",
                table: "t_resultado",
                column: "CitaId",
                principalTable: "t_cita",
                principalColumn: "idcita");
        }
    }
}
