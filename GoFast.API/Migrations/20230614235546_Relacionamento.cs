using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GoFast.API.Migrations
{
    /// <inheritdoc />
    public partial class Relacionamento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdAzure",
                table: "BlobStorage");

            migrationBuilder.DropColumn(
                name: "IdUsuario",
                table: "BlobStorage");

            migrationBuilder.DropColumn(
                name: "base64",
                table: "BlobStorage");

            migrationBuilder.RenameColumn(
                name: "IdBlob",
                table: "Documentos",
                newName: "BlobId");

            migrationBuilder.CreateIndex(
                name: "IX_Documentos_BlobId",
                table: "Documentos",
                column: "BlobId");

            migrationBuilder.AddForeignKey(
                name: "FK_Documentos_BlobStorage_BlobId",
                table: "Documentos",
                column: "BlobId",
                principalTable: "BlobStorage",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Documentos_BlobStorage_BlobId",
                table: "Documentos");

            migrationBuilder.DropIndex(
                name: "IX_Documentos_BlobId",
                table: "Documentos");

            migrationBuilder.RenameColumn(
                name: "BlobId",
                table: "Documentos",
                newName: "IdBlob");

            migrationBuilder.AddColumn<string>(
                name: "IdAzure",
                table: "BlobStorage",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "IdUsuario",
                table: "BlobStorage",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "base64",
                table: "BlobStorage",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
