using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lanchonete.Migrations
{
    public partial class PopularMateriasPrimas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO MateriasPrimas (Nome, UnidadeMedida, EstoqueAtual, EstoqueMinimo) " +
                                 "VALUES ('Pão', 'unidade', 100.0, 20.0)");

            migrationBuilder.Sql("INSERT INTO MateriasPrimas (Nome, UnidadeMedida, EstoqueAtual, EstoqueMinimo) " +
                                 "VALUES ('Carne de Hambúrguer', 'unidade', 50.0, 10.0)");

            migrationBuilder.Sql("INSERT INTO MateriasPrimas (Nome, UnidadeMedida, EstoqueAtual, EstoqueMinimo) " +
                                 "VALUES ('Tomate', 'kg', 5.0, 1.0)");

            migrationBuilder.Sql("INSERT INTO MateriasPrimas (Nome, UnidadeMedida, EstoqueAtual, EstoqueMinimo) " +
                                 "VALUES ('Alface', 'kg', 10.0, 2.0)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM MateriasPrimas");
        }
    }
}