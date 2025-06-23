using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lanchonete.Migrations
{
    public partial class PopularTabelaLanches2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categorias",
                columns: new[] { "CategoriaId", "CategoriaNome", "Descricao" },
                values: new object[,]
                {
                    { 7, "Especiais", "Lanches especiais, com ingredientes exclusivos" }
                });

            migrationBuilder.Sql("INSERT INTO Lanches(CategoriaId, Descricao, EmEstoque, ImagemThumbnailUrl, ImagemUrl, IsLanchePreferido, Nome, Preco)" +
              "VALUES(4,'Pão, hambúrguer, queijo e bacon',1,'/images/X-Bacon.png', '/images/X-Bacon.png',0,'X-Bacon', 20.00)");

            migrationBuilder.Sql("INSERT INTO Lanches(CategoriaId, Descricao, EmEstoque, ImagemThumbnailUrl, ImagemUrl, IsLanchePreferido, Nome, Preco)" +
              "VALUES(4,'Pão, hambúrguer, presunto, queijo, bacon, ovo, alface e tomate',1,'/images/X-Tudo.png', '/images/X-Tudo.png',0,'X-Tudo', 30.00)");

            migrationBuilder.Sql("INSERT INTO Lanches(CategoriaId, Descricao, EmEstoque, ImagemThumbnailUrl, ImagemUrl, IsLanchePreferido, Nome, Preco)" +
              "VALUES(7,'Pão, cabeça do Matheus, presunto, queijo, bacon, ovo, alface e tomate',1,'/images/X-Matheus.png', '/images/X-Matheus.png',0,'X-Matheus', 950.00)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
            table: "Categorias",
            keyColumn: "CategoriaId",
            keyValue: 7);
            migrationBuilder.Sql("DELETE FROM Lanches");
        }
    }
}
