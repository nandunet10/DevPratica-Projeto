using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarLocadora.Infra.Migrations
{
    public partial class CreateTabelaManutencaoVeiculo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ManutencaoVeiculos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    DataServico = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Garantia = table.Column<int>(type: "int", nullable: false),
                    ValorServico = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DataInclusao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAlteracao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    VeiculoPlaca = table.Column<string>(type: "nvarchar(8)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ManutencaoVeiculos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ManutencaoVeiculos_Veiculos_VeiculoPlaca",
                        column: x => x.VeiculoPlaca,
                        principalTable: "Veiculos",
                        principalColumn: "Placa",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ManutencaoVeiculos_VeiculoPlaca",
                table: "ManutencaoVeiculos",
                column: "VeiculoPlaca");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ManutencaoVeiculos");
        }
    }
}
