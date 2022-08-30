using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarLocadora.Infra.Migrations
{
    public partial class correcoes01 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Locacoes_Categorias_CategoriaModelId",
                table: "Locacoes");

            migrationBuilder.DropForeignKey(
                name: "FK_Locacoes_Clientes_ClienteCPF",
                table: "Locacoes");

            migrationBuilder.DropIndex(
                name: "IX_Locacoes_CategoriaModelId",
                table: "Locacoes");

            migrationBuilder.DropIndex(
                name: "IX_Locacoes_ClienteCPF",
                table: "Locacoes");

            migrationBuilder.DropColumn(
                name: "CategoriaId",
                table: "Locacoes");

            migrationBuilder.DropColumn(
                name: "CategoriaModelId",
                table: "Locacoes");

            migrationBuilder.RenameColumn(
                name: "FormaDePagamentoId",
                table: "Locacoes",
                newName: "CategoriasId");

            migrationBuilder.AddColumn<string>(
                name: "ClientesCPF",
                table: "Locacoes",
                type: "nvarchar(14)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Locacoes_CategoriasId",
                table: "Locacoes",
                column: "CategoriasId");

            migrationBuilder.CreateIndex(
                name: "IX_Locacoes_ClientesCPF",
                table: "Locacoes",
                column: "ClientesCPF");

            migrationBuilder.AddForeignKey(
                name: "FK_Locacoes_Categorias_CategoriasId",
                table: "Locacoes",
                column: "CategoriasId",
                principalTable: "Categorias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Locacoes_Clientes_ClientesCPF",
                table: "Locacoes",
                column: "ClientesCPF",
                principalTable: "Clientes",
                principalColumn: "CPF");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Locacoes_Categorias_CategoriasId",
                table: "Locacoes");

            migrationBuilder.DropForeignKey(
                name: "FK_Locacoes_Clientes_ClientesCPF",
                table: "Locacoes");

            migrationBuilder.DropIndex(
                name: "IX_Locacoes_CategoriasId",
                table: "Locacoes");

            migrationBuilder.DropIndex(
                name: "IX_Locacoes_ClientesCPF",
                table: "Locacoes");

            migrationBuilder.DropColumn(
                name: "ClientesCPF",
                table: "Locacoes");

            migrationBuilder.RenameColumn(
                name: "CategoriasId",
                table: "Locacoes",
                newName: "FormaDePagamentoId");

            migrationBuilder.AddColumn<int>(
                name: "CategoriaId",
                table: "Locacoes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CategoriaModelId",
                table: "Locacoes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Locacoes_CategoriaModelId",
                table: "Locacoes",
                column: "CategoriaModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Locacoes_ClienteCPF",
                table: "Locacoes",
                column: "ClienteCPF");

            migrationBuilder.AddForeignKey(
                name: "FK_Locacoes_Categorias_CategoriaModelId",
                table: "Locacoes",
                column: "CategoriaModelId",
                principalTable: "Categorias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Locacoes_Clientes_ClienteCPF",
                table: "Locacoes",
                column: "ClienteCPF",
                principalTable: "Clientes",
                principalColumn: "CPF",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
