using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lanchonete.Migrations
{
    public partial class PopularMateriasPrimas2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO MateriasPrimas (Nome, UnidadeMedida, EstoqueAtual, EstoqueMinimo) " +
                                 "VALUES ('Bacon', 'kg', 50.00, 20.0)");//21

            migrationBuilder.Sql("INSERT INTO MateriasPrimas (Nome, UnidadeMedida, EstoqueAtual, EstoqueMinimo) " +
                                 "VALUES ('Presunto', 'kg', 50.0, 20.00)");//22

            migrationBuilder.Sql("INSERT INTO MateriasPrimas (Nome, UnidadeMedida, EstoqueAtual, EstoqueMinimo) " +
                                 "VALUES ('Queijo', 'kg', 50.00, 20.0)");//23

            migrationBuilder.Sql("INSERT INTO MateriasPrimas (Nome, UnidadeMedida, EstoqueAtual, EstoqueMinimo) " +
                                 "VALUES ('Ovo', 'un', 80.00, 20.00)");//24

            migrationBuilder.Sql("INSERT INTO MateriasPrimas (Nome, UnidadeMedida, EstoqueAtual, EstoqueMinimo) " +
                     "VALUES ('Cabeça de Matheus', 'un', 80.00, 20.00)");//25
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM MateriasPrimas");
        }
    }
}
