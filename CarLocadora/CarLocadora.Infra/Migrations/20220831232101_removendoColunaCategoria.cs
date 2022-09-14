using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarLocadora.Infra.Migrations
{
    public partial class removendoColunaCategoria : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Locacoes_Categorias_CategoriasId",
                table: "Locacoes");

            migrationBuilder.DropIndex(
                name: "IX_Locacoes_CategoriasId",
                table: "Locacoes");

            migrationBuilder.DropColumn(
                name: "CategoriasId",
                table: "Locacoes");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoriasId",
                table: "Locacoes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Locacoes_CategoriasId",
                table: "Locacoes",
                column: "CategoriasId");

            migrationBuilder.AddForeignKey(
                name: "FK_Locacoes_Categorias_CategoriasId",
                table: "Locacoes",
                column: "CategoriasId",
                principalTable: "Categorias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
