using Xunit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using PetShopManager.Data;
using PetShopManager.Controllers;
using PetShopManager.DTO;

namespace Tests
{
    public class GetClientesTest : BaseTest, IClassFixture<DbTeste>
    {
        private readonly ServiceProvider _serviceProvide;

        public GetClientesTest(DbTeste dbTeste)
        {
            _serviceProvide = dbTeste.ServiceProvider;
        }

        [Fact]
        public async Task GetClientes_RetornaStatusCode200()
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

            var _getRegistroCliente = await controllerCliente.Get();

            OkObjectResult result = _getRegistroCliente as OkObjectResult;

            Assert.Equal(200, result.StatusCode);
        }
    }

    public class GetByIdClientesTest : BaseTest, IClassFixture<DbTeste>
    {
        private readonly ServiceProvider _serviceProvide;

        public GetByIdClientesTest(DbTeste dbTeste)
        {
            _serviceProvide = dbTeste.ServiceProvider;
        }

        [Fact]
        public async Task GetByIdClientes_RetornaStatusCode200()
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

            var _getRegistroCliente = await controllerCliente.GetById(1);

            OkObjectResult result = _getRegistroCliente as OkObjectResult;

            Assert.Equal(200, result.StatusCode);
        }
    }

    public class PostClientesTest : BaseTest, IClassFixture<DbTeste>
    {
        private readonly ServiceProvider _serviceProvide;

        public PostClientesTest(DbTeste dbTeste)
        {
            _serviceProvide = dbTeste.ServiceProvider;
        }

        [Fact]
        public async Task PostClientes_RetornaStatusCode201()
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
    }

    public class PatchClientesTest : BaseTest, IClassFixture<DbTeste>
    {
        private readonly ServiceProvider _serviceProvide;

        public PatchClientesTest(DbTeste dbTeste)
        {
            _serviceProvide = dbTeste.ServiceProvider;
        }

        [Fact]
        public async Task PatchClientes_RetornaStatusCode201()
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

            ClienteDTO NovoCliente = new()
            {
                Nome = "TesteNomeClienteNovo",
                Telefone = "11222222222",
                Cpf = "11111111111",
                Email = "teste@teste.com",
                Senha = "testeSenhaNovo",
            };

            var _registroClienteCriado = await controllerCliente.Post(Cliente);

            var _registroClienteAtualizado = await controllerCliente.Patch(1, NovoCliente);

            CreatedResult result = _registroClienteAtualizado as CreatedResult;

            Assert.Equal(201, result.StatusCode);
        }
    }

    public class DeleteClientesTest : BaseTest, IClassFixture<DbTeste>
    {
        private readonly ServiceProvider _serviceProvide;

        public DeleteClientesTest(DbTeste dbTeste)
        {
            _serviceProvide = dbTeste.ServiceProvider;
        }

        [Fact]
        public async Task DeleteClientes_RetornaStatusCode200()
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

            var _registroDelete = await controllerCliente.Delete(1);

            OkObjectResult result = _registroDelete as OkObjectResult;

            Assert.Equal(200, result.StatusCode);
        }
    }
}
