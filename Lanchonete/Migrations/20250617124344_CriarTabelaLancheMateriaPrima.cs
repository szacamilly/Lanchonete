using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lanchonete.Migrations
{
    public partial class CriarTabelaLancheMateriaPrima : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LanchesMateriasPrimas",
                columns: table => new
                {
                    LancheId = table.Column<int>(type: "int", nullable: false),
                    MateriaPrimaId = table.Column<int>(type: "int", nullable: false),

                    QuantidadeUtilizada = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LanchesMateriasPrimas", x => new { x.LancheId, x.MateriaPrimaId });

                    table.ForeignKey(
                        name: "FK_LanchesMateriasPrimas_Lanches_LancheId",
                        column: x => x.LancheId,
                        principalTable: "Lanches",
                        principalColumn: "LancheId",
                        onDelete: ReferentialAction.Cascade);

                    table.ForeignKey(
                        name: "FK_LanchesMateriasPrimas_MateriasPrimas_MateriaPrimaId",
                        column: x => x.MateriaPrimaId,
                        principalTable: "MateriasPrimas",
                        principalColumn: "MateriaPrimaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LanchesMateriasPrimas_MateriaPrimaId",
                table: "LanchesMateriasPrimas",
                column: "MateriaPrimaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LanchesMateriasPrimas");
        }
    }
}