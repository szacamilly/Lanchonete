using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lanchonete.Migrations
{
    public partial class RemoverColunas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UsuarioAbertura",
                table: "RegistrosCaixaDiario");

            migrationBuilder.DropColumn(
                name: "UsuarioFechamento",
                table: "RegistrosCaixaDiario");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UsuarioAbertura",
                table: "RegistrosCaixaDiario",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UsuarioFechamento",
                table: "RegistrosCaixaDiario",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);
        }
    }
}