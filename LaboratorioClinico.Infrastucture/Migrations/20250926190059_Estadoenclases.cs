using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LaboratorioClinico.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Estadoenclases : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_t_usuario_t_rol_RolId",
                table: "t_usuario");

            migrationBuilder.RenameColumn(
                name: "RolId",
                table: "t_usuario",
                newName: "DoctorId");

            migrationBuilder.RenameIndex(
                name: "IX_t_usuario_RolId",
                table: "t_usuario",
                newName: "IX_t_usuario_DoctorId");

            migrationBuilder.AddColumn<bool>(
                name: "estado",
                table: "t_usuario",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "idrol1",
                table: "t_usuario",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "estado",
                table: "t_rol",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "CitaId",
                table: "t_resultado",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "estado",
                table: "t_resultado",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "iddoctor1",
                table: "t_resultado",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "idexamen1",
                table: "t_resultado",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "estado",
                table: "t_paciente",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "idcita",
                table: "t_paciente",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "idcita1",
                table: "t_paciente",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "idusuario1",
                table: "t_paciente",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "estado",
                table: "t_examen",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "idcita",
                table: "t_examen",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "idcita1",
                table: "t_examen",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "idpaciente1",
                table: "t_examen",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "idresultado",
                table: "t_examen",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "idresultado1",
                table: "t_examen",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "estado",
                table: "t_doctor",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "idcita",
                table: "t_doctor",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "idcita1",
                table: "t_doctor",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "idusuario1",
                table: "t_doctor",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "iddoctor1",
                table: "t_cita",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "idexamen",
                table: "t_cita",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "idexamen1",
                table: "t_cita",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "idpaciente1",
                table: "t_cita",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_t_usuario_idrol",
                table: "t_usuario",
                column: "idrol");

            migrationBuilder.CreateIndex(
                name: "IX_t_resultado_CitaId",
                table: "t_resultado",
                column: "CitaId");

            migrationBuilder.CreateIndex(
                name: "IX_t_resultado_iddoctor1",
                table: "t_resultado",
                column: "iddoctor1");

            migrationBuilder.CreateIndex(
                name: "IX_t_resultado_idexamen1",
                table: "t_resultado",
                column: "idexamen1");

            migrationBuilder.CreateIndex(
                name: "IX_t_paciente_idcita1",
                table: "t_paciente",
                column: "idcita1");

            migrationBuilder.CreateIndex(
                name: "IX_t_paciente_idusuario1",
                table: "t_paciente",
                column: "idusuario1");

            migrationBuilder.CreateIndex(
                name: "IX_t_examen_idcita",
                table: "t_examen",
                column: "idcita");

            migrationBuilder.CreateIndex(
                name: "IX_t_examen_idpaciente1",
                table: "t_examen",
                column: "idpaciente1");

            migrationBuilder.CreateIndex(
                name: "IX_t_examen_idresultado",
                table: "t_examen",
                column: "idresultado");

            migrationBuilder.CreateIndex(
                name: "IX_t_doctor_idcita1",
                table: "t_doctor",
                column: "idcita1");

            migrationBuilder.CreateIndex(
                name: "IX_t_doctor_idusuario1",
                table: "t_doctor",
                column: "idusuario1");

            migrationBuilder.CreateIndex(
                name: "IX_t_cita_iddoctor",
                table: "t_cita",
                column: "iddoctor");

            migrationBuilder.CreateIndex(
                name: "IX_t_cita_idexamen1",
                table: "t_cita",
                column: "idexamen1");

            migrationBuilder.CreateIndex(
                name: "IX_t_cita_idpaciente",
                table: "t_cita",
                column: "idpaciente");

            migrationBuilder.AddForeignKey(
                name: "FK_t_cita_t_doctor_iddoctor",
                table: "t_cita",
                column: "iddoctor",
                principalTable: "t_doctor",
                principalColumn: "iddoctor",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_t_cita_t_examen_idexamen1",
                table: "t_cita",
                column: "idexamen1",
                principalTable: "t_examen",
                principalColumn: "idexamen",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_t_cita_t_paciente_idpaciente",
                table: "t_cita",
                column: "idpaciente",
                principalTable: "t_paciente",
                principalColumn: "idpaciente",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_t_doctor_t_cita_idcita1",
                table: "t_doctor",
                column: "idcita1",
                principalTable: "t_cita",
                principalColumn: "idcita",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_t_doctor_t_usuario_idusuario1",
                table: "t_doctor",
                column: "idusuario1",
                principalTable: "t_usuario",
                principalColumn: "idusuario",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_t_examen_t_cita_idcita",
                table: "t_examen",
                column: "idcita",
                principalTable: "t_cita",
                principalColumn: "idcita",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_t_examen_t_paciente_idpaciente1",
                table: "t_examen",
                column: "idpaciente1",
                principalTable: "t_paciente",
                principalColumn: "idpaciente",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_t_examen_t_resultado_idresultado",
                table: "t_examen",
                column: "idresultado",
                principalTable: "t_resultado",
                principalColumn: "idresultado",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_t_paciente_t_cita_idcita1",
                table: "t_paciente",
                column: "idcita1",
                principalTable: "t_cita",
                principalColumn: "idcita",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_t_paciente_t_usuario_idusuario1",
                table: "t_paciente",
                column: "idusuario1",
                principalTable: "t_usuario",
                principalColumn: "idusuario",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_t_resultado_t_cita_CitaId",
                table: "t_resultado",
                column: "CitaId",
                principalTable: "t_cita",
                principalColumn: "idcita");

            migrationBuilder.AddForeignKey(
                name: "FK_t_resultado_t_doctor_iddoctor1",
                table: "t_resultado",
                column: "iddoctor1",
                principalTable: "t_doctor",
                principalColumn: "iddoctor",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_t_resultado_t_examen_idexamen1",
                table: "t_resultado",
                column: "idexamen1",
                principalTable: "t_examen",
                principalColumn: "idexamen",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_t_usuario_t_doctor_DoctorId",
                table: "t_usuario",
                column: "DoctorId",
                principalTable: "t_doctor",
                principalColumn: "iddoctor");

            migrationBuilder.AddForeignKey(
                name: "FK_t_usuario_t_rol_idrol",
                table: "t_usuario",
                column: "idrol",
                principalTable: "t_rol",
                principalColumn: "idrol",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_t_cita_t_doctor_iddoctor",
                table: "t_cita");

            migrationBuilder.DropForeignKey(
                name: "FK_t_cita_t_examen_idexamen1",
                table: "t_cita");

            migrationBuilder.DropForeignKey(
                name: "FK_t_cita_t_paciente_idpaciente",
                table: "t_cita");

            migrationBuilder.DropForeignKey(
                name: "FK_t_doctor_t_cita_idcita1",
                table: "t_doctor");

            migrationBuilder.DropForeignKey(
                name: "FK_t_doctor_t_usuario_idusuario1",
                table: "t_doctor");

            migrationBuilder.DropForeignKey(
                name: "FK_t_examen_t_cita_idcita",
                table: "t_examen");

            migrationBuilder.DropForeignKey(
                name: "FK_t_examen_t_paciente_idpaciente1",
                table: "t_examen");

            migrationBuilder.DropForeignKey(
                name: "FK_t_examen_t_resultado_idresultado",
                table: "t_examen");

            migrationBuilder.DropForeignKey(
                name: "FK_t_paciente_t_cita_idcita1",
                table: "t_paciente");

            migrationBuilder.DropForeignKey(
                name: "FK_t_paciente_t_usuario_idusuario1",
                table: "t_paciente");

            migrationBuilder.DropForeignKey(
                name: "FK_t_resultado_t_cita_CitaId",
                table: "t_resultado");

            migrationBuilder.DropForeignKey(
                name: "FK_t_resultado_t_doctor_iddoctor1",
                table: "t_resultado");

            migrationBuilder.DropForeignKey(
                name: "FK_t_resultado_t_examen_idexamen1",
                table: "t_resultado");

            migrationBuilder.DropForeignKey(
                name: "FK_t_usuario_t_doctor_DoctorId",
                table: "t_usuario");

            migrationBuilder.DropForeignKey(
                name: "FK_t_usuario_t_rol_idrol",
                table: "t_usuario");

            migrationBuilder.DropIndex(
                name: "IX_t_usuario_idrol",
                table: "t_usuario");

            migrationBuilder.DropIndex(
                name: "IX_t_resultado_CitaId",
                table: "t_resultado");

            migrationBuilder.DropIndex(
                name: "IX_t_resultado_iddoctor1",
                table: "t_resultado");

            migrationBuilder.DropIndex(
                name: "IX_t_resultado_idexamen1",
                table: "t_resultado");

            migrationBuilder.DropIndex(
                name: "IX_t_paciente_idcita1",
                table: "t_paciente");

            migrationBuilder.DropIndex(
                name: "IX_t_paciente_idusuario1",
                table: "t_paciente");

            migrationBuilder.DropIndex(
                name: "IX_t_examen_idcita",
                table: "t_examen");

            migrationBuilder.DropIndex(
                name: "IX_t_examen_idpaciente1",
                table: "t_examen");

            migrationBuilder.DropIndex(
                name: "IX_t_examen_idresultado",
                table: "t_examen");

            migrationBuilder.DropIndex(
                name: "IX_t_doctor_idcita1",
                table: "t_doctor");

            migrationBuilder.DropIndex(
                name: "IX_t_doctor_idusuario1",
                table: "t_doctor");

            migrationBuilder.DropIndex(
                name: "IX_t_cita_iddoctor",
                table: "t_cita");

            migrationBuilder.DropIndex(
                name: "IX_t_cita_idexamen1",
                table: "t_cita");

            migrationBuilder.DropIndex(
                name: "IX_t_cita_idpaciente",
                table: "t_cita");

            migrationBuilder.DropColumn(
                name: "estado",
                table: "t_usuario");

            migrationBuilder.DropColumn(
                name: "idrol1",
                table: "t_usuario");

            migrationBuilder.DropColumn(
                name: "estado",
                table: "t_rol");

            migrationBuilder.DropColumn(
                name: "CitaId",
                table: "t_resultado");

            migrationBuilder.DropColumn(
                name: "estado",
                table: "t_resultado");

            migrationBuilder.DropColumn(
                name: "iddoctor1",
                table: "t_resultado");

            migrationBuilder.DropColumn(
                name: "idexamen1",
                table: "t_resultado");

            migrationBuilder.DropColumn(
                name: "estado",
                table: "t_paciente");

            migrationBuilder.DropColumn(
                name: "idcita",
                table: "t_paciente");

            migrationBuilder.DropColumn(
                name: "idcita1",
                table: "t_paciente");

            migrationBuilder.DropColumn(
                name: "idusuario1",
                table: "t_paciente");

            migrationBuilder.DropColumn(
                name: "estado",
                table: "t_examen");

            migrationBuilder.DropColumn(
                name: "idcita",
                table: "t_examen");

            migrationBuilder.DropColumn(
                name: "idcita1",
                table: "t_examen");

            migrationBuilder.DropColumn(
                name: "idpaciente1",
                table: "t_examen");

            migrationBuilder.DropColumn(
                name: "idresultado",
                table: "t_examen");

            migrationBuilder.DropColumn(
                name: "idresultado1",
                table: "t_examen");

            migrationBuilder.DropColumn(
                name: "estado",
                table: "t_doctor");

            migrationBuilder.DropColumn(
                name: "idcita",
                table: "t_doctor");

            migrationBuilder.DropColumn(
                name: "idcita1",
                table: "t_doctor");

            migrationBuilder.DropColumn(
                name: "idusuario1",
                table: "t_doctor");

            migrationBuilder.DropColumn(
                name: "iddoctor1",
                table: "t_cita");

            migrationBuilder.DropColumn(
                name: "idexamen",
                table: "t_cita");

            migrationBuilder.DropColumn(
                name: "idexamen1",
                table: "t_cita");

            migrationBuilder.DropColumn(
                name: "idpaciente1",
                table: "t_cita");

            migrationBuilder.RenameColumn(
                name: "DoctorId",
                table: "t_usuario",
                newName: "RolId");

            migrationBuilder.RenameIndex(
                name: "IX_t_usuario_DoctorId",
                table: "t_usuario",
                newName: "IX_t_usuario_RolId");

            migrationBuilder.AddForeignKey(
                name: "FK_t_usuario_t_rol_RolId",
                table: "t_usuario",
                column: "RolId",
                principalTable: "t_rol",
                principalColumn: "idrol");
        }
    }
}
