using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LaboratorioClinico.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ProyectoInicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "t_rol",
                columns: table => new
                {
                    idrol = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    nombre = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    descripcion = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    estado = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_rol", x => x.idrol);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "t_usuario",
                columns: table => new
                {
                    idusuario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    username = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    password = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    idrol = table.Column<int>(type: "int", nullable: false),
                    estado = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_usuario", x => x.idusuario);
                    table.ForeignKey(
                        name: "FK_t_usuario_t_rol_idrol",
                        column: x => x.idrol,
                        principalTable: "t_rol",
                        principalColumn: "idrol",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "t_doctor",
                columns: table => new
                {
                    iddoctor = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    nombre = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    apellido = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    especialidad = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    telefono = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    email = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    licenciamedica = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    idusuario = table.Column<int>(type: "int", nullable: false),
                    estado = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_doctor", x => x.iddoctor);
                    table.ForeignKey(
                        name: "FK_t_doctor_t_usuario_idusuario",
                        column: x => x.idusuario,
                        principalTable: "t_usuario",
                        principalColumn: "idusuario",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "t_paciente",
                columns: table => new
                {
                    idpaciente = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    nombre = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    apellido = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    fechanacimiento = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    telefono = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    email = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    direccion = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    idusuario = table.Column<int>(type: "int", nullable: false),
                    estado = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_paciente", x => x.idpaciente);
                    table.ForeignKey(
                        name: "FK_t_paciente_t_usuario_idusuario",
                        column: x => x.idusuario,
                        principalTable: "t_usuario",
                        principalColumn: "idusuario",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "t_cita",
                columns: table => new
                {
                    idcita = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    fechahora = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    motivo = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    estado = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    notasconsulta = table.Column<string>(type: "varchar(300)", maxLength: 300, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    idpaciente = table.Column<int>(type: "int", nullable: false),
                    iddoctor = table.Column<int>(type: "int", nullable: false),
                    idexamen = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_cita", x => x.idcita);
                    table.ForeignKey(
                        name: "FK_t_cita_t_doctor_iddoctor",
                        column: x => x.iddoctor,
                        principalTable: "t_doctor",
                        principalColumn: "iddoctor",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_t_cita_t_paciente_idpaciente",
                        column: x => x.idpaciente,
                        principalTable: "t_paciente",
                        principalColumn: "idpaciente",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "t_examen",
                columns: table => new
                {
                    idexamen = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    tipoexamen = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    fecha = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    descripcion = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    idpaciente = table.Column<int>(type: "int", nullable: false),
                    idcita = table.Column<int>(type: "int", nullable: false),
                    estado = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    ResultadoId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_examen", x => x.idexamen);
                    table.ForeignKey(
                        name: "FK_t_examen_t_cita_idcita",
                        column: x => x.idcita,
                        principalTable: "t_cita",
                        principalColumn: "idcita",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_t_examen_t_paciente_idpaciente",
                        column: x => x.idpaciente,
                        principalTable: "t_paciente",
                        principalColumn: "idpaciente",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "t_resultado",
                columns: table => new
                {
                    idresultado = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    detalle = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    fechaemision = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    idexamen = table.Column<int>(type: "int", nullable: false),
                    iddoctor = table.Column<int>(type: "int", nullable: false),
                    estado = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CitaId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_resultado", x => x.idresultado);
                    table.ForeignKey(
                        name: "FK_t_resultado_t_cita_CitaId",
                        column: x => x.CitaId,
                        principalTable: "t_cita",
                        principalColumn: "idcita");
                    table.ForeignKey(
                        name: "FK_t_resultado_t_doctor_iddoctor",
                        column: x => x.iddoctor,
                        principalTable: "t_doctor",
                        principalColumn: "iddoctor",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_t_resultado_t_examen_idexamen",
                        column: x => x.idexamen,
                        principalTable: "t_examen",
                        principalColumn: "idexamen",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_t_cita_iddoctor",
                table: "t_cita",
                column: "iddoctor");

            migrationBuilder.CreateIndex(
                name: "IX_t_cita_idexamen",
                table: "t_cita",
                column: "idexamen");

            migrationBuilder.CreateIndex(
                name: "IX_t_cita_idpaciente",
                table: "t_cita",
                column: "idpaciente");

            migrationBuilder.CreateIndex(
                name: "IX_t_doctor_idusuario",
                table: "t_doctor",
                column: "idusuario");

            migrationBuilder.CreateIndex(
                name: "IX_t_examen_idcita",
                table: "t_examen",
                column: "idcita");

            migrationBuilder.CreateIndex(
                name: "IX_t_examen_idpaciente",
                table: "t_examen",
                column: "idpaciente");

            migrationBuilder.CreateIndex(
                name: "IX_t_examen_ResultadoId",
                table: "t_examen",
                column: "ResultadoId");

            migrationBuilder.CreateIndex(
                name: "IX_t_paciente_idusuario",
                table: "t_paciente",
                column: "idusuario");

            migrationBuilder.CreateIndex(
                name: "IX_t_resultado_CitaId",
                table: "t_resultado",
                column: "CitaId");

            migrationBuilder.CreateIndex(
                name: "IX_t_resultado_iddoctor",
                table: "t_resultado",
                column: "iddoctor");

            migrationBuilder.CreateIndex(
                name: "IX_t_resultado_idexamen",
                table: "t_resultado",
                column: "idexamen");

            migrationBuilder.CreateIndex(
                name: "IX_t_usuario_idrol",
                table: "t_usuario",
                column: "idrol");

            migrationBuilder.AddForeignKey(
                name: "FK_t_cita_t_examen_idexamen",
                table: "t_cita",
                column: "idexamen",
                principalTable: "t_examen",
                principalColumn: "idexamen",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_t_examen_t_resultado_ResultadoId",
                table: "t_examen",
                column: "ResultadoId",
                principalTable: "t_resultado",
                principalColumn: "idresultado");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_t_cita_t_doctor_iddoctor",
                table: "t_cita");

            migrationBuilder.DropForeignKey(
                name: "FK_t_resultado_t_doctor_iddoctor",
                table: "t_resultado");

            migrationBuilder.DropForeignKey(
                name: "FK_t_cita_t_examen_idexamen",
                table: "t_cita");

            migrationBuilder.DropForeignKey(
                name: "FK_t_resultado_t_examen_idexamen",
                table: "t_resultado");

            migrationBuilder.DropTable(
                name: "t_doctor");

            migrationBuilder.DropTable(
                name: "t_examen");

            migrationBuilder.DropTable(
                name: "t_resultado");

            migrationBuilder.DropTable(
                name: "t_cita");

            migrationBuilder.DropTable(
                name: "t_paciente");

            migrationBuilder.DropTable(
                name: "t_usuario");

            migrationBuilder.DropTable(
                name: "t_rol");
        }
    }
}
