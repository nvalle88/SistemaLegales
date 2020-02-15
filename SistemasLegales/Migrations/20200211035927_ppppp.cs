using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SistemasLegales.Migrations
{
    public partial class ppppp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DocumentoRequisito_Requisito",
                table: "DocumentoRequisito");

            migrationBuilder.DropForeignKey(
                name: "FK_Requisito_Ciudad_IdCiudad",
                table: "Requisito");

            migrationBuilder.DropForeignKey(
                name: "FK_Requisito_Documento_IdDocumento",
                table: "Requisito");

            migrationBuilder.DropForeignKey(
                name: "FK_Requisito_Proceso_IdProceso",
                table: "Requisito");

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "RequisitoLegal",
                type: "varchar(1000)",
                maxLength: 1000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "Observaciones",
                table: "Requisito",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(1000)",
                oldMaxLength: 1000,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaCumplimiento",
                table: "Requisito",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaCaducidad",
                table: "Requisito",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EmailNotificacion2",
                table: "Requisito",
                type: "varchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "EmailNotificacion1",
                table: "Requisito",
                type: "varchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<int>(
                name: "Criticidad",
                table: "Requisito",
                nullable: true,
                defaultValueSql: "0");

            migrationBuilder.AddColumn<bool>(
                name: "Finalizado",
                table: "Requisito",
                nullable: false,
                defaultValueSql: "0");

            migrationBuilder.AddColumn<bool>(
                name: "Habilitado",
                table: "Requisito",
                nullable: false,
                defaultValueSql: "1");

            migrationBuilder.AddColumn<int>(
                name: "IdProyecto",
                table: "Requisito",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "NotificacionEnviadaUltima",
                table: "Requisito",
                nullable: false,
                defaultValueSql: "0");

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "OrganismoControl",
                type: "varchar(1000)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "Documento",
                type: "varchar(1000)",
                maxLength: 1000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AddColumn<int>(
                name: "Cantidad",
                table: "Documento",
                nullable: false,
                defaultValueSql: "0");

            migrationBuilder.AddColumn<int>(
                name: "Tipo",
                table: "Documento",
                nullable: false,
                defaultValueSql: "1");

            migrationBuilder.CreateTable(
                name: "Accion",
                columns: table => new
                {
                    IdAccion = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Detalle = table.Column<string>(type: "text", nullable: false),
                    Fecha = table.Column<DateTime>(nullable: false),
                    IdRequisito = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accion", x => x.IdAccion);
                    table.ForeignKey(
                        name: "FK_Accion_Requisito_IdRequisito",
                        column: x => x.IdRequisito,
                        principalTable: "Requisito",
                        principalColumn: "IdRequisito",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Proyecto",
                columns: table => new
                {
                    IdProyecto = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "varchar(250)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proyecto", x => x.IdProyecto);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Requisito_IdProyecto",
                table: "Requisito",
                column: "IdProyecto");

            migrationBuilder.CreateIndex(
                name: "IX_Accion_IdRequisito",
                table: "Accion",
                column: "IdRequisito");

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentoRequisito_Requisito_IdRequisito",
                table: "DocumentoRequisito",
                column: "IdRequisito",
                principalTable: "Requisito",
                principalColumn: "IdRequisito",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AdminRequisitoLegal_Ciudad",
                table: "Requisito",
                column: "IdCiudad",
                principalTable: "Ciudad",
                principalColumn: "IdCiudad",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Requisito_Documento",
                table: "Requisito",
                column: "IdDocumento",
                principalTable: "Documento",
                principalColumn: "IdDocumento",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AdminRequisitoLegal_Proceso",
                table: "Requisito",
                column: "IdProceso",
                principalTable: "Proceso",
                principalColumn: "IdProceso",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Requisito_Proyecto",
                table: "Requisito",
                column: "IdProyecto",
                principalTable: "Proyecto",
                principalColumn: "IdProyecto",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DocumentoRequisito_Requisito_IdRequisito",
                table: "DocumentoRequisito");

            migrationBuilder.DropForeignKey(
                name: "FK_AdminRequisitoLegal_Ciudad",
                table: "Requisito");

            migrationBuilder.DropForeignKey(
                name: "FK_Requisito_Documento",
                table: "Requisito");

            migrationBuilder.DropForeignKey(
                name: "FK_AdminRequisitoLegal_Proceso",
                table: "Requisito");

            migrationBuilder.DropForeignKey(
                name: "FK_Requisito_Proyecto",
                table: "Requisito");

            migrationBuilder.DropTable(
                name: "Accion");

            migrationBuilder.DropTable(
                name: "Proyecto");

            migrationBuilder.DropIndex(
                name: "IX_Requisito_IdProyecto",
                table: "Requisito");

            migrationBuilder.DropColumn(
                name: "Criticidad",
                table: "Requisito");

            migrationBuilder.DropColumn(
                name: "Finalizado",
                table: "Requisito");

            migrationBuilder.DropColumn(
                name: "Habilitado",
                table: "Requisito");

            migrationBuilder.DropColumn(
                name: "IdProyecto",
                table: "Requisito");

            migrationBuilder.DropColumn(
                name: "NotificacionEnviadaUltima",
                table: "Requisito");

            migrationBuilder.DropColumn(
                name: "Cantidad",
                table: "Documento");

            migrationBuilder.DropColumn(
                name: "Tipo",
                table: "Documento");

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "RequisitoLegal",
                type: "varchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(1000)",
                oldMaxLength: 1000);

            migrationBuilder.AlterColumn<string>(
                name: "Observaciones",
                table: "Requisito",
                type: "varchar(1000)",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaCumplimiento",
                table: "Requisito",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaCaducidad",
                table: "Requisito",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<string>(
                name: "EmailNotificacion2",
                table: "Requisito",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EmailNotificacion1",
                table: "Requisito",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "OrganismoControl",
                type: "varchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(1000)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "Documento",
                type: "varchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(1000)",
                oldMaxLength: 1000);

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentoRequisito_Requisito",
                table: "DocumentoRequisito",
                column: "IdRequisito",
                principalTable: "Requisito",
                principalColumn: "IdRequisito",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Requisito_Ciudad_IdCiudad",
                table: "Requisito",
                column: "IdCiudad",
                principalTable: "Ciudad",
                principalColumn: "IdCiudad",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Requisito_Documento_IdDocumento",
                table: "Requisito",
                column: "IdDocumento",
                principalTable: "Documento",
                principalColumn: "IdDocumento",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Requisito_Proceso_IdProceso",
                table: "Requisito",
                column: "IdProceso",
                principalTable: "Proceso",
                principalColumn: "IdProceso",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
