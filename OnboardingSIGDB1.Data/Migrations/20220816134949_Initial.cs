using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OnboardingSIGDB1.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CARGO",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CARGO", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EMPRESA",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cnpj = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataFundacao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EMPRESA", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FUNCIONARIO",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cpf = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataContratacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdEmpresa = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FUNCIONARIO", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FUNCIONARIO_EMPRESA_IdEmpresa",
                        column: x => x.IdEmpresa,
                        principalTable: "EMPRESA",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FUNCIONARIOXCARGO",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdFuncionario = table.Column<int>(type: "int", nullable: false),
                    IdCargo = table.Column<int>(type: "int", nullable: false),
                    DataVinculo = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FUNCIONARIOXCARGO", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FUNCIONARIOXCARGO_CARGO_IdCargo",
                        column: x => x.IdCargo,
                        principalTable: "CARGO",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FUNCIONARIOXCARGO_FUNCIONARIO_IdFuncionario",
                        column: x => x.IdFuncionario,
                        principalTable: "FUNCIONARIO",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FUNCIONARIO_IdEmpresa",
                table: "FUNCIONARIO",
                column: "IdEmpresa");

            migrationBuilder.CreateIndex(
                name: "IX_FUNCIONARIOXCARGO_IdCargo",
                table: "FUNCIONARIOXCARGO",
                column: "IdCargo");

            migrationBuilder.CreateIndex(
                name: "IX_FUNCIONARIOXCARGO_IdFuncionario",
                table: "FUNCIONARIOXCARGO",
                column: "IdFuncionario");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FUNCIONARIOXCARGO");

            migrationBuilder.DropTable(
                name: "CARGO");

            migrationBuilder.DropTable(
                name: "FUNCIONARIO");

            migrationBuilder.DropTable(
                name: "EMPRESA");
        }
    }
}
