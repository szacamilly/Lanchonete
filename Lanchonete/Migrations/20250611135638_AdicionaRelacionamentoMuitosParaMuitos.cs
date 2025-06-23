using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lanchonete.Migrations
{
    public partial class AdicionaRelacionamentoMuitosParaMuitos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MateriasPrimas_Lanches_LancheId",
                table: "MateriasPrimas");

            migrationBuilder.DropIndex(
                name: "IX_MateriasPrimas_LancheId",
                table: "MateriasPrimas");

            migrationBuilder.DropColumn(
                name: "LancheId",
                table: "MateriasPrimas");

            migrationBuilder.AlterColumn<string>(
                name: "UnidadeMedida",
                table: "MateriasPrimas",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "MateriasPrimas",
                type: "nvarchar(80)",
                maxLength: 80,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<double>(
                name: "EstoqueMinimo",
                table: "MateriasPrimas",
                type: "float",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<double>(
                name: "EstoqueAtual",
                table: "MateriasPrimas",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

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
            /*migrationBuilder.DropTable(
                name: "LanchesMateriasPrimas");*/

            migrationBuilder.DropColumn(
                name: "EstoqueAtual",
                table: "MateriasPrimas");

            migrationBuilder.AlterColumn<string>(
                name: "UnidadeMedida",
                table: "MateriasPrimas",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "MateriasPrimas",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(80)",
                oldMaxLength: 80);

            migrationBuilder.AlterColumn<int>(
                name: "EstoqueMinimo",
                table: "MateriasPrimas",
                type: "int",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AddColumn<int>(
                name: "LancheId",
                table: "MateriasPrimas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_MateriasPrimas_LancheId",
                table: "MateriasPrimas",
                column: "LancheId");

            migrationBuilder.AddForeignKey(
                name: "FK_MateriasPrimas_Lanches_LancheId",
                table: "MateriasPrimas",
                column: "LancheId",
                principalTable: "Lanches",
                principalColumn: "LancheId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
