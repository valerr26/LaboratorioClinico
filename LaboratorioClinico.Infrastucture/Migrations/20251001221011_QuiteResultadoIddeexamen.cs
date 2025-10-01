using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LaboratorioClinico.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class QuiteResultadoIddeexamen : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_t_examen_t_resultado_ResultadoId",
                table: "t_examen");

            migrationBuilder.DropIndex(
                name: "IX_t_examen_ResultadoId",
                table: "t_examen");

            migrationBuilder.DropColumn(
                name: "ResultadoId",
                table: "t_examen");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ResultadoId",
                table: "t_examen",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_t_examen_ResultadoId",
                table: "t_examen",
                column: "ResultadoId");

            migrationBuilder.AddForeignKey(
                name: "FK_t_examen_t_resultado_ResultadoId",
                table: "t_examen",
                column: "ResultadoId",
                principalTable: "t_resultado",
                principalColumn: "idresultado");
        }
    }
}
