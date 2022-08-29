using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarLocadora.Infra.Migrations
{
    public partial class adicionandoModelsLocacaoVistoria : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Locacoes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClienteCPF = table.Column<string>(type: "nvarchar(14)", maxLength: 14, nullable: false),
                    FormaDePagamentoId = table.Column<int>(type: "int", nullable: false),
                    FormasDePagamentoId = table.Column<int>(type: "int", nullable: false),
                    CategoriaId = table.Column<int>(type: "int", nullable: false),
                    CategoriaModelId = table.Column<int>(type: "int", nullable: false),
                    DataHoraReserva = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataHoraRetiradaPrevista = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataHoraDevolucaoPrevista = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VeiculoPlaca = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    DataInclusao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAlteracao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locacoes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Locacoes_Categorias_CategoriaModelId",
                        column: x => x.CategoriaModelId,
                        principalTable: "Categorias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Locacoes_Clientes_ClienteCPF",
                        column: x => x.ClienteCPF,
                        principalTable: "Clientes",
                        principalColumn: "CPF",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Locacoes_FormasDePagamento_FormasDePagamentoId",
                        column: x => x.FormasDePagamentoId,
                        principalTable: "FormasDePagamento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Locacoes_Veiculos_VeiculoPlaca",
                        column: x => x.VeiculoPlaca,
                        principalTable: "Veiculos",
                        principalColumn: "Placa",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Vistorias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LocacaoId = table.Column<int>(type: "int", nullable: false),
                    KmSaida = table.Column<long>(type: "bigint", nullable: false),
                    CombustivelSaida = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ObservacaoSaida = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    DataHoraRetiradaPatio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    KmEntrada = table.Column<long>(type: "bigint", nullable: false),
                    CombustivelEntrada = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ObservacaoEntrada = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    DataHoraDevolucaoPatio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataInclusao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAlteracao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vistorias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vistorias_Locacoes_LocacaoId",
                        column: x => x.LocacaoId,
                        principalTable: "Locacoes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Locacoes_CategoriaModelId",
                table: "Locacoes",
                column: "CategoriaModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Locacoes_ClienteCPF",
                table: "Locacoes",
                column: "ClienteCPF");

            migrationBuilder.CreateIndex(
                name: "IX_Locacoes_FormasDePagamentoId",
                table: "Locacoes",
                column: "FormasDePagamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_Locacoes_VeiculoPlaca",
                table: "Locacoes",
                column: "VeiculoPlaca");

            migrationBuilder.CreateIndex(
                name: "IX_Vistorias_LocacaoId",
                table: "Vistorias",
                column: "LocacaoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Vistorias");

            migrationBuilder.DropTable(
                name: "Locacoes");
        }
    }
}
