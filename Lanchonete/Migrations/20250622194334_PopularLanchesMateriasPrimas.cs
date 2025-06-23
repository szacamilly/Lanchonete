using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lanchonete.Migrations
{
    public partial class PopularLanchesMateriasPrimas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //x-bacon
            migrationBuilder.Sql("INSERT INTO LanchesMateriasPrimas (LancheId, MateriaPrimaId, QuantidadeUtilizada) VALUES (7, 17, 2.0)");
            migrationBuilder.Sql("INSERT INTO LanchesMateriasPrimas (LancheId, MateriaPrimaId, QuantidadeUtilizada) VALUES (7, 18, 1.0)");
            migrationBuilder.Sql("INSERT INTO LanchesMateriasPrimas (LancheId, MateriaPrimaId, QuantidadeUtilizada) VALUES (7, 23, 0.08)");
            migrationBuilder.Sql("INSERT INTO LanchesMateriasPrimas (LancheId, MateriaPrimaId, QuantidadeUtilizada) VALUES (7, 21, 0.08)");

            //x-tudo
            migrationBuilder.Sql("INSERT INTO LanchesMateriasPrimas (LancheId, MateriaPrimaId, QuantidadeUtilizada) VALUES (8, 17, 2.0)");
            migrationBuilder.Sql("INSERT INTO LanchesMateriasPrimas (LancheId, MateriaPrimaId, QuantidadeUtilizada) VALUES (8, 18, 1.0)");
            migrationBuilder.Sql("INSERT INTO LanchesMateriasPrimas (LancheId, MateriaPrimaId, QuantidadeUtilizada) VALUES (8, 22, 0.08)");
            migrationBuilder.Sql("INSERT INTO LanchesMateriasPrimas (LancheId, MateriaPrimaId, QuantidadeUtilizada) VALUES (8, 23, 0.08)");
            migrationBuilder.Sql("INSERT INTO LanchesMateriasPrimas (LancheId, MateriaPrimaId, QuantidadeUtilizada) VALUES (8, 21, 0.08)");
            migrationBuilder.Sql("INSERT INTO LanchesMateriasPrimas (LancheId, MateriaPrimaId, QuantidadeUtilizada) VALUES (8, 24, 1.00)");
            migrationBuilder.Sql("INSERT INTO LanchesMateriasPrimas (LancheId, MateriaPrimaId, QuantidadeUtilizada) VALUES (8, 20, 0.05)");
            migrationBuilder.Sql("INSERT INTO LanchesMateriasPrimas (LancheId, MateriaPrimaId, QuantidadeUtilizada) VALUES (8, 19, 0.08)");

            //x-matheus
            migrationBuilder.Sql("INSERT INTO LanchesMateriasPrimas (LancheId, MateriaPrimaId, QuantidadeUtilizada) VALUES (9, 17, 2.0)");
            migrationBuilder.Sql("INSERT INTO LanchesMateriasPrimas (LancheId, MateriaPrimaId, QuantidadeUtilizada) VALUES (9, 25, 1.0)");
            migrationBuilder.Sql("INSERT INTO LanchesMateriasPrimas (LancheId, MateriaPrimaId, QuantidadeUtilizada) VALUES (9, 22, 0.08)");
            migrationBuilder.Sql("INSERT INTO LanchesMateriasPrimas (LancheId, MateriaPrimaId, QuantidadeUtilizada) VALUES (9, 23, 0.08)");
            migrationBuilder.Sql("INSERT INTO LanchesMateriasPrimas (LancheId, MateriaPrimaId, QuantidadeUtilizada) VALUES (9, 21, 0.08)");
            migrationBuilder.Sql("INSERT INTO LanchesMateriasPrimas (LancheId, MateriaPrimaId, QuantidadeUtilizada) VALUES (9, 24, 1.00)");
            migrationBuilder.Sql("INSERT INTO LanchesMateriasPrimas (LancheId, MateriaPrimaId, QuantidadeUtilizada) VALUES (9, 20, 0.05)");
            migrationBuilder.Sql("INSERT INTO LanchesMateriasPrimas (LancheId, MateriaPrimaId, QuantidadeUtilizada) VALUES (9, 19, 0.08)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM LanchesMateriasPrimas WHERE LancheId = 1 AND MateriaPrimaId IN (5, 6, 7, 8)");
        }
    }
}
