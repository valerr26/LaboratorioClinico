using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LaboratorioClinico.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Agregueiddoctorapaciente : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "iddoctor",
                table: "t_paciente",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_t_paciente_iddoctor",
                table: "t_paciente",
                column: "iddoctor");

            migrationBuilder.AddForeignKey(
                name: "FK_t_paciente_t_doctor_iddoctor",
                table: "t_paciente",
                column: "iddoctor",
                principalTable: "t_doctor",
                principalColumn: "iddoctor",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_t_paciente_t_doctor_iddoctor",
                table: "t_paciente");

            migrationBuilder.DropIndex(
                name: "IX_t_paciente_iddoctor",
                table: "t_paciente");

            migrationBuilder.DropColumn(
                name: "iddoctor",
                table: "t_paciente");
        }
    }
}
