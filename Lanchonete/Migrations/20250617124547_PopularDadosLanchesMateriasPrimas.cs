using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lanchonete.Migrations
{
    public partial class PopularDadosLanchesMateriasPrimas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO LanchesMateriasPrimas (LancheId, MateriaPrimaId, QuantidadeUtilizada) VALUES (3, 17, 2.0)");
            migrationBuilder.Sql("INSERT INTO LanchesMateriasPrimas (LancheId, MateriaPrimaId, QuantidadeUtilizada) VALUES (3, 18, 1.0)");
            migrationBuilder.Sql("INSERT INTO LanchesMateriasPrimas (LancheId, MateriaPrimaId, QuantidadeUtilizada) VALUES (3, 19, 0.05)");
            migrationBuilder.Sql("INSERT INTO LanchesMateriasPrimas (LancheId, MateriaPrimaId, QuantidadeUtilizada) VALUES (3, 20, 0.2)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM LanchesMateriasPrimas WHERE LancheId = 1 AND MateriaPrimaId IN (5, 6, 7, 8)");
        }
    }
}