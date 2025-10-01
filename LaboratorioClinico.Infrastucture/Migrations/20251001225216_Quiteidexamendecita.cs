using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LaboratorioClinico.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Quiteidexamendecita : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_t_cita_t_examen_idexamen",
                table: "t_cita");

            migrationBuilder.DropIndex(
                name: "IX_t_cita_idexamen",
                table: "t_cita");

            migrationBuilder.DropColumn(
                name: "idexamen",
                table: "t_cita");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "idexamen",
                table: "t_cita",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_t_cita_idexamen",
                table: "t_cita",
                column: "idexamen");

            migrationBuilder.AddForeignKey(
                name: "FK_t_cita_t_examen_idexamen",
                table: "t_cita",
                column: "idexamen",
                principalTable: "t_examen",
                principalColumn: "idexamen",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
