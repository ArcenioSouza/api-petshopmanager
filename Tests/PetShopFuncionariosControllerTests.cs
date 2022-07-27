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
    public class FuncionariosControllerTests : BaseTest, IClassFixture<DbTeste>
    {
        private readonly ServiceProvider _serviceProvide;

        public FuncionariosControllerTests(DbTeste dbTeste)
        {
            _serviceProvide = dbTeste.ServiceProvider;
        }

        [Fact, Priority(1)]
        public async Task Post_RetornaStatusCode201()
        {
            using var context = _serviceProvide.GetService<ApplicationDbContext>();

            FuncionariosController controllerFuncionario = new(context);
            CargosController controllerCargo = new(context);

            FuncionarioDTO Funcionario = new()
            {
                Nome = "TesteNomeFuncionario",
                CargoId = 1,
                Cpf = "11111111111",
                Email = "teste@teste",
                Senha = "testeSenha",
            };

            CargoDTO Cargo = new()
            {
                NomeCargo = "CargoTest"
            };

            var _registroCargoCriado = await controllerCargo.Post(Cargo);

            var _registroFuncionarioCriado = await controllerFuncionario.Post(Funcionario);

            CreatedResult result = _registroFuncionarioCriado as CreatedResult;

            Assert.Equal(201, result.StatusCode);
        }

        [Fact, Priority(2)]
        public async Task Get_RetornaStatusCode200()
        {
            using var context = _serviceProvide.GetService<ApplicationDbContext>();

            FuncionariosController controllerFuncionario = new(context);

            var _getRegistroFuncionario = await controllerFuncionario.Get();

            OkObjectResult result = _getRegistroFuncionario as OkObjectResult;

            Assert.Equal(200, result.StatusCode);
        }

        [Fact, Priority(3)]
        public async Task GetById_RetornaStatusCode200()
        {
            using var context = _serviceProvide.GetService<ApplicationDbContext>();

            FuncionariosController controllerFuncionario = new(context);

            var _getRegistroFuncionario = await controllerFuncionario.GetById(1);

            OkObjectResult result = _getRegistroFuncionario as OkObjectResult;

            Assert.Equal(200, result.StatusCode);
        }

        [Fact, Priority(4)]
        public async Task Patch_RetornaStatusCode201()
        {
            using var context = _serviceProvide.GetService<ApplicationDbContext>();

            FuncionariosController controllerFuncionario = new(context);

            FuncionarioDTO NovoFuncionario = new()
            {
                Nome = "NovoTesteNomeFuncionario",
                CargoId = 1,
                Cpf = "11111111111",
                Email = "testenovo@teste",
                Senha = "testeSenhaNovo",
            };

            var _registroFuncionarioAtualizado = await controllerFuncionario.Patch(1, NovoFuncionario);

            CreatedResult result = _registroFuncionarioAtualizado as CreatedResult;

            Assert.Equal(201, result.StatusCode);
        }

        [Fact, Priority(5)]
        public async Task Delete_RetornaStatusCode200()
        {
            using var context = _serviceProvide.GetService<ApplicationDbContext>();

            FuncionariosController controllerFuncionario = new(context);

            var _registroDelete = await controllerFuncionario.Delete(1);

            OkObjectResult result = _registroDelete as OkObjectResult;

            Assert.Equal(200, result.StatusCode);
        }
    }
}
