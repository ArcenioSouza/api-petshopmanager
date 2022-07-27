using Xunit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using PetShopManager.Data;
using PetShopManager.Controllers;
using PetShopManager.DTO;
using Xunit.Priority;

namespace Tests
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class AtendimentosControllerTests : BaseTest, IClassFixture<DbTeste>
    {
        private readonly ServiceProvider _serviceProvide;
        public AtendimentosControllerTests(DbTeste dbTeste)
        {
            _serviceProvide = dbTeste.ServiceProvider;
        }

        [Fact, Priority(1)]
        public async Task Post_RetornaStatusCode201()
        {
            using var context = _serviceProvide.GetService<ApplicationDbContext>();

            AtendimentosController controllerAtendimento = new(context);
            CargosController controllerCargo = new(context);
            ClientesController controllerCliente = new(context);
            FuncionariosController controllerFuncionario = new(context);
            ServicosController controllerServico = new(context);
            AnimaisController controllerAnimal = new(context);

            AtendimentoDTO Atendimento = new()
            {
                ServicoId = 1,
                ClienteId = 1,
                FuncionarioId = 1,
                AnimalId = 1
            };

            AnimalDTO Animal = new()
            {
                Nome = "TesteNomeAnimal",
                ClienteID = 1,
                Sexo = "Macho",
                PesoAtual = 10,
                AlturaAtual = 50,
                DataNascimento = "10/01/2020",
                Raca = "Tzu"
            };

            ClienteDTO Cliente = new()
            {
                Nome = "ClienteTest",
                Cpf = "11111111111",
                Telefone = "11988887777",
                Email = "clienteteste@gmail.com",
                Senha = "testesenha",
            };

            FuncionarioDTO Funcionario = new()
            {
                Nome = "TesteNomeFuncionario",
                CargoId = 1,
                Cpf = "11111111111",
                Email = "teste@teste",
                Senha = "testeSenha",
            };

            ServicoDTO Servico = new()
            {
                Tipo = "Teste Serviço",
                Valor = 10
            };

            CargoDTO Cargo = new()
            {
                NomeCargo = "CargoTest"
            };

            var _registroCargoCriado = await controllerCargo.Post(Cargo);

            var _registroServicoCriado = await controllerServico.Post(Servico);

            var _registroClienteCriado = await controllerCliente.Post(Cliente);

            var _registroFuncionarioCriado = await controllerFuncionario.Post(Funcionario);

            var _registroAnimalCriado = await controllerAnimal.Post(Animal);

            var _registroAtendimentoCriado = await controllerAtendimento.Post(Atendimento);

            CreatedResult result = _registroAtendimentoCriado as CreatedResult;

            Assert.Equal(201, result.StatusCode);
        }

        [Fact, Priority(2)]
        public async Task Get_RetornaStatusCode200()
        {
            using var context = _serviceProvide.GetService<ApplicationDbContext>();

            AtendimentosController controllerAtendimento = new(context);

            var _getRegistroAtendimento = await controllerAtendimento.Get();

            OkObjectResult result = _getRegistroAtendimento as OkObjectResult;

            Assert.Equal(200, result.StatusCode);
        }

        [Fact, Priority(3)]
        public async Task GetByIdCliente_RetornaStatusCode200()
        {
            using var context = _serviceProvide.GetService<ApplicationDbContext>();

            AtendimentosController controllerAtendimento = new(context);

            var _getRegistroAtendimento = await controllerAtendimento.GetByIdCliente(1);

            OkObjectResult result = _getRegistroAtendimento as OkObjectResult;

            Assert.Equal(200, result.StatusCode);
        }

        [Fact, Priority(4)]
        public async Task GetByNomeCliente_RetornaStatusCode200()
        {
            using var context = _serviceProvide.GetService<ApplicationDbContext>();

            AtendimentosController controllerAtendimento = new(context);

            var _getRegistroAtendimento = await controllerAtendimento.GetByNomeCliente("ClienteTest", "11111111111");

            OkObjectResult result = _getRegistroAtendimento as OkObjectResult;

            Assert.Equal(200, result.StatusCode);
        }

        [Fact, Priority(5)]
        public async Task GetByIdAnimal_RetornaStatusCode200()
        {
            using var context = _serviceProvide.GetService<ApplicationDbContext>();

            AtendimentosController controllerAtendimento = new(context);

            var _getRegistroAtendimento = await controllerAtendimento.GetByIdAnimal(1);

            OkObjectResult result = _getRegistroAtendimento as OkObjectResult;

            Assert.Equal(200, result.StatusCode);
        }

        [Fact, Priority(6)]
        public async Task GetByNomeAnimal_RetornaStatusCode200()
        {
            using var context = _serviceProvide.GetService<ApplicationDbContext>();

            AtendimentosController controllerAtendimento = new(context);

            var _getRegistroAtendimento = await controllerAtendimento.GetByNomeAnimal("TesteNomeAnimal", "10/01/2020");

            OkObjectResult result = _getRegistroAtendimento as OkObjectResult;

            Assert.Equal(200, result.StatusCode);
        }

        [Fact, Priority(7)]
        public async Task Patch_RetornaStatusCode201()
        {
            using var context = _serviceProvide.GetService<ApplicationDbContext>();

            AtendimentosController controllerAtendimento = new(context);

            AtendimentoPatchDTO NovoAtendimento = new()
            {
                Observacoes = "Testando",
                Diagnostico = "Testando",
            };

            var _registroAtendimentoAtualizado = await controllerAtendimento.Patch(1, NovoAtendimento);

            CreatedResult result = _registroAtendimentoAtualizado as CreatedResult;

            Assert.Equal(201, result.StatusCode);
        }
    }
}
