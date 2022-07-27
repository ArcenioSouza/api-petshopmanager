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
    public class ClientesControllerTests : BaseTest, IClassFixture<DbTeste>
    {
        private readonly ServiceProvider _serviceProvide;

        public ClientesControllerTests(DbTeste dbTeste)
        {
            _serviceProvide = dbTeste.ServiceProvider;
        }

        [Fact, Priority(1)]
        public async Task Post_RetornaStatusCode201()
        {
            using var context = _serviceProvide.GetService<ApplicationDbContext>();

            ClientesController controllerCliente = new(context);

            ClienteDTO Cliente = new()
            {
                Nome = "TesteNomeCliente",
                Telefone = "11222222222",
                Cpf = "11111111111",
                Email = "teste@teste",
                Senha = "testeSenha",
            };

            var _registroClienteCriado = await controllerCliente.Post(Cliente);

            CreatedResult result = _registroClienteCriado as CreatedResult;

            Assert.Equal(201, result.StatusCode);
        }

        [Fact, Priority(2)]
        public async Task Get_RetornaStatusCode200()
        {
            using var context = _serviceProvide.GetService<ApplicationDbContext>();

            ClientesController controllerCliente = new(context);

            var _getRegistroCliente = await controllerCliente.Get();

            OkObjectResult result = _getRegistroCliente as OkObjectResult;

            Assert.Equal(200, result.StatusCode);
        }

        [Fact, Priority(3)]
        public async Task GetById_RetornaStatusCode200()
        {
            using var context = _serviceProvide.GetService<ApplicationDbContext>();

            ClientesController controllerCliente = new(context);

            var _getRegistroCliente = await controllerCliente.GetById(1);

            OkObjectResult result = _getRegistroCliente as OkObjectResult;

            Assert.Equal(200, result.StatusCode);
        }

        [Fact, Priority(4)]
        public async Task Patch_RetornaStatusCode201()
        {
            using var context = _serviceProvide.GetService<ApplicationDbContext>();

            ClientesController controllerCliente = new(context);

            ClienteDTO NovoCliente = new()
            {
                Nome = "TesteNomeClienteNovo",
                Telefone = "11222222222",
                Cpf = "11111111111",
                Email = "teste@teste.com",
                Senha = "testeSenhaNovo",
            };

            var _registroClienteAtualizado = await controllerCliente.Patch(1, NovoCliente);

            CreatedResult result = _registroClienteAtualizado as CreatedResult;

            Assert.Equal(201, result.StatusCode);
        }

        [Fact, Priority(5)]
        public async Task Delete_RetornaStatusCode200()
        {
            using var context = _serviceProvide.GetService<ApplicationDbContext>();

            ClientesController controllerCliente = new(context);

            var _registroDelete = await controllerCliente.Delete(1);

            OkObjectResult result = _registroDelete as OkObjectResult;

            Assert.Equal(200, result.StatusCode);
        }
    }
}
