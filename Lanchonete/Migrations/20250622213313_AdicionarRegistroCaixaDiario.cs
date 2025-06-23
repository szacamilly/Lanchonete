using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lanchonete.Migrations
{
    public partial class AdicionarRegistroCaixaDiario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RegistrosCaixaDiario",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataCaixa = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SaldoAbertura = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalEntradas = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalSaidas = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SaldoFechamento = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CaixaFechado = table.Column<bool>(type: "bit", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegistrosCaixaDiario", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RegistrosCaixaDiario");
        }
    }
}
