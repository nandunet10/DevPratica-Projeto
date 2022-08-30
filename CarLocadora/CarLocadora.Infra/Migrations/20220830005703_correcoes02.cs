using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarLocadora.Infra.Migrations
{
    public partial class correcoes02 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Locacoes_Clientes_ClientesCPF",
                table: "Locacoes");

            migrationBuilder.DropIndex(
                name: "IX_Locacoes_ClientesCPF",
                table: "Locacoes");

            migrationBuilder.DropColumn(
                name: "ClientesCPF",
                table: "Locacoes");

            migrationBuilder.CreateIndex(
                name: "IX_Locacoes_ClienteCPF",
                table: "Locacoes",
                column: "ClienteCPF");

            migrationBuilder.AddForeignKey(
                name: "FK_Locacoes_Clientes_ClienteCPF",
                table: "Locacoes",
                column: "ClienteCPF",
                principalTable: "Clientes",
                principalColumn: "CPF",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Locacoes_Clientes_ClienteCPF",
                table: "Locacoes");

            migrationBuilder.DropIndex(
                name: "IX_Locacoes_ClienteCPF",
                table: "Locacoes");

            migrationBuilder.AddColumn<string>(
                name: "ClientesCPF",
                table: "Locacoes",
                type: "nvarchar(14)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Locacoes_ClientesCPF",
                table: "Locacoes",
                column: "ClientesCPF");

            migrationBuilder.AddForeignKey(
                name: "FK_Locacoes_Clientes_ClientesCPF",
                table: "Locacoes",
                column: "ClientesCPF",
                principalTable: "Clientes",
                principalColumn: "CPF");
        }
    }
}
