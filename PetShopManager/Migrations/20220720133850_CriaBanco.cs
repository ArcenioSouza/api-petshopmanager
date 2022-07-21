using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PetShopManager.Migrations
{
    public partial class CriaBanco : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Cargos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NomeCargo = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cargos", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Cpf = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Telefone = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Senha = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Servicos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Tipo = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Valor = table.Column<double>(type: "double", nullable: false),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Servicos", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Funcionarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CargoId = table.Column<int>(type: "int", nullable: true),
                    Email = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Senha = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Funcionarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Funcionarios_Cargos_CargoId",
                        column: x => x.CargoId,
                        principalTable: "Cargos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Animais",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ClienteId = table.Column<int>(type: "int", nullable: true),
                    Sexo = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PesoAtual = table.Column<double>(type: "double", nullable: false),
                    AlturaAtual = table.Column<double>(type: "double", nullable: false),
                    Raca = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TempoDeVida = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Temperamento = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PesoMedio = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AlturaMedia = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Animais", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Animais_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Atendimentos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ServicoId = table.Column<int>(type: "int", nullable: true),
                    ClienteId = table.Column<int>(type: "int", nullable: true),
                    FuncionarioId = table.Column<int>(type: "int", nullable: true),
                    AnimalId = table.Column<int>(type: "int", nullable: true),
                    PesoDoAnimalAtualizado = table.Column<double>(type: "double", nullable: false),
                    AlturaDoAnimalAtualizado = table.Column<double>(type: "double", nullable: false),
                    DataAtendimento = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Diagnostico = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Observacoes = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Atendimentos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Atendimentos_Animais_AnimalId",
                        column: x => x.AnimalId,
                        principalTable: "Animais",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Atendimentos_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Atendimentos_Funcionarios_FuncionarioId",
                        column: x => x.FuncionarioId,
                        principalTable: "Funcionarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Atendimentos_Servicos_ServicoId",
                        column: x => x.ServicoId,
                        principalTable: "Servicos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Animais_ClienteId",
                table: "Animais",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Atendimentos_AnimalId",
                table: "Atendimentos",
                column: "AnimalId");

            migrationBuilder.CreateIndex(
                name: "IX_Atendimentos_ClienteId",
                table: "Atendimentos",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Atendimentos_FuncionarioId",
                table: "Atendimentos",
                column: "FuncionarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Atendimentos_ServicoId",
                table: "Atendimentos",
                column: "ServicoId");

            migrationBuilder.CreateIndex(
                name: "IX_Funcionarios_CargoId",
                table: "Funcionarios",
                column: "CargoId");

            //Cliente
            migrationBuilder.Sql(@"INSERT INTO `petshop`.`clientes` (`Id`, `Nome`, `Cpf`, `Telefone`, `Email`, `Senha`, `IsActive`) VALUES ('1', 'Arcenio', '11111111111', '11999999999', 'arcenioneto@gmail.com', 'Arce@123', '1')");
            migrationBuilder.Sql(@"INSERT INTO `petshop`.`clientes` (`Id`, `Nome`, `Cpf`, `Telefone`, `Email`, `Senha`, `IsActive`) VALUES ('2', 'Catarina', '22222222222', '11888888888', 'catarina@gmail.com', 'Cata@123', '1')");
            migrationBuilder.Sql(@"INSERT INTO `petshop`.`clientes` (`Id`, `Nome`, `Cpf`, `Telefone`, `Email`, `Senha`, `IsActive`) VALUES ('3', 'André', '33333333333', '11555555555', 'andre@gmail.com', 'Andre@123', '1')");

            //Cargo
            migrationBuilder.Sql(@"INSERT INTO `petshop`.`cargos` (`Id`, `NomeCargo`) VALUES ('1', 'Veterinário')");
            migrationBuilder.Sql(@"INSERT INTO `petshop`.`cargos` (`Id`, `NomeCargo`) VALUES ('2', 'Atendente')");
            migrationBuilder.Sql(@"INSERT INTO `petshop`.`cargos` (`Id`, `NomeCargo`) VALUES ('3', 'Gerente')");
            migrationBuilder.Sql(@"INSERT INTO `petshop`.`cargos` (`Id`, `NomeCargo`) VALUES ('4', 'Tosador')");

            //Funcionario
            migrationBuilder.Sql(@"INSERT INTO `petshop`.`funcionarios` (`Id`, `Nome`, `CargoId`, `Email`, `Senha`, `IsActive`) VALUES ('1', 'João', '1', 'joao@gmail.com', 'Joao@123', '1')");
            migrationBuilder.Sql(@"INSERT INTO `petshop`.`funcionarios` (`Id`, `Nome`, `CargoId`, `Email`, `Senha`, `IsActive`) VALUES ('2', 'Maria', '2', 'maria@gmail.com', 'Maria@123', '1')");
            migrationBuilder.Sql(@"INSERT INTO `petshop`.`funcionarios` (`Id`, `Nome`, `CargoId`, `Email`, `Senha`, `IsActive`) VALUES ('3', 'Pedro', '3', 'pedro@gmail.com', 'Pedro@123', '1')");
            migrationBuilder.Sql(@"INSERT INTO `petshop`.`funcionarios` (`Id`, `Nome`, `CargoId`, `Email`, `Senha`, `IsActive`) VALUES ('4', 'Caio', '4', 'caio@gmail.com', 'Caio@123', '1')");


            //Animal
            migrationBuilder.Sql(@"INSERT INTO `animais` (`Id`, `Nome`, `ClienteId`, `Sexo`, `PesoAtual`, `AlturaAtual`, `Raca`, `TempoDeVida`, `Temperamento`, `PesoMedio`, `AlturaMedia`, `IsActive`) VALUES ('1', 'Dob', '1', 'Fêmea', '10', '55', 'American Bulldog', '10 - 12 years', 'Friendly, Assertive, Energetic, Loyal, Gentle, Confident, Dominant', '27 - 54', '56 - 69', '1')");
            migrationBuilder.Sql(@"INSERT INTO `animais` (`Id`, `Nome`, `ClienteId`, `Sexo`, `PesoAtual`, `AlturaAtual`, `Raca`, `TempoDeVida`, `Temperamento`, `PesoMedio`, `AlturaMedia`, `IsActive`) VALUES ('2', 'Rufus', '2', 'Macho', '10', '55', 'American Bulldog', '10 - 12 years', 'Friendly, Assertive, Energetic, Loyal, Gentle, Confident, Dominant', '27 - 54', '56 - 69', '1')");
            migrationBuilder.Sql(@"INSERT INTO `animais` (`Id`, `Nome`, `ClienteId`, `Sexo`, `PesoAtual`, `AlturaAtual`, `Raca`, `TempoDeVida`, `Temperamento`, `PesoMedio`, `AlturaMedia`, `IsActive`) VALUES ('3', 'Feroz', '3', 'Macho', '10', '55', 'American Bulldog', '10 - 12 years', 'Friendly, Assertive, Energetic, Loyal, Gentle, Confident, Dominant', '27 - 54', '56 - 69', '1')");

            //Servico
            migrationBuilder.Sql(@"INSERT INTO `servicos` (`Id`, `Tipo`, `Valor`, `IsActive`) VALUES ('1', 'Vacinação', '40', '1')");
            migrationBuilder.Sql(@"INSERT INTO `servicos` (`Id`, `Tipo`, `Valor`, `IsActive`) VALUES ('2', 'Banho e Tosa', '60', '1')");
            migrationBuilder.Sql(@"INSERT INTO `servicos` (`Id`, `Tipo`, `Valor`, `IsActive`) VALUES ('3', 'Consulta', '80', '1')");

            //Atendimento
            migrationBuilder.Sql(@"INSERT INTO `atendimentos` (`Id`, `ServicoId`, `ClienteId`, `FuncionarioId`, `AnimalId`, `PesoDoAnimalAtualizado`, `AlturaDoAnimalAtualizado`, `DataAtendimento`, `Diagnostico`, `Observacoes`) VALUES ('1', '1', '1', '1', '1',  '0', '0', '2022-07-10 00:00:00.000000', 'Vacinação em dia', 'Próxima vacina daqui a dois meses')");
            migrationBuilder.Sql(@"INSERT INTO `atendimentos` (`Id`, `ServicoId`, `ClienteId`, `FuncionarioId`, `AnimalId`, `PesoDoAnimalAtualizado`, `AlturaDoAnimalAtualizado`, `DataAtendimento`, `Diagnostico`, `Observacoes`) VALUES ('2', '2', '2', '4', '2',  '0', '0', '2022-07-12 00:00:00.000000', 'Foram encontrados alguns nó, mas sem dificuldade para tira-lo', 'null')");
            migrationBuilder.Sql(@"INSERT INTO `atendimentos` (`Id`, `ServicoId`, `ClienteId`, `FuncionarioId`, `AnimalId`, `PesoDoAnimalAtualizado`, `AlturaDoAnimalAtualizado`, `DataAtendimento`, `Diagnostico`, `Observacoes`) VALUES ('3', '3', '3', '1', '3', '15', '60', '2022-07-18 00:00:00.000000', 'Houve alteração no peso e altura do animal, mas nada relevante para a saúde', 'Verificar peso e altura na próxima consulta e comparar as alterações')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Atendimentos");

            migrationBuilder.DropTable(
                name: "Animais");

            migrationBuilder.DropTable(
                name: "Funcionarios");

            migrationBuilder.DropTable(
                name: "Servicos");

            migrationBuilder.DropTable(
                name: "Clientes");

            migrationBuilder.DropTable(
                name: "Cargos");
        }
    }
}
