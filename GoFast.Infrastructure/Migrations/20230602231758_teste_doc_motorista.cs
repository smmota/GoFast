using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GoFast.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class teste_doc_motorista : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "Motorista.DocumentoMotorista",
                table: "Motorista",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Motorista_Motorista.DocumentoMotorista",
                table: "Motorista",
                column: "Motorista.DocumentoMotorista");

            migrationBuilder.AddForeignKey(
                name: "FK_Motorista_Documentos_Motorista.DocumentoMotorista",
                table: "Motorista",
                column: "Motorista.DocumentoMotorista",
                principalTable: "Documentos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Motorista_Documentos_Motorista.DocumentoMotorista",
                table: "Motorista");

            migrationBuilder.DropIndex(
                name: "IX_Motorista_Motorista.DocumentoMotorista",
                table: "Motorista");

            migrationBuilder.DropColumn(
                name: "Motorista.DocumentoMotorista",
                table: "Motorista");
        }
    }
}
