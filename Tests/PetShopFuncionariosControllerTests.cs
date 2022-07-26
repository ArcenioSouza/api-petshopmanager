using Xunit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using PetShopManager.Data;
using PetShopManager.Controllers;
using PetShopManager.DTO;

namespace Tests
{
    public class GetFuncionariosTest : BaseTest, IClassFixture<DbTeste>
    {
        private readonly ServiceProvider _serviceProvide;

        public GetFuncionariosTest(DbTeste dbTeste)
        {
            _serviceProvide = dbTeste.ServiceProvider;
        }

        [Fact]
        public async Task GetFuncionarios_RetornaStatusCode200()
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

            var _getRegistroFuncionario = await controllerFuncionario.Get();

            OkObjectResult result = _getRegistroFuncionario as OkObjectResult;

            Assert.Equal(200, result.StatusCode);
        }
    }

    public class GetByIdFuncionariosTest : BaseTest, IClassFixture<DbTeste>
    {
        private readonly ServiceProvider _serviceProvide;

        public GetByIdFuncionariosTest(DbTeste dbTeste)
        {
            _serviceProvide = dbTeste.ServiceProvider;
        }

        [Fact]
        public async Task GetByIdFuncionarios_RetornaStatusCode200()
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

            var _getRegistroFuncionario = await controllerFuncionario.GetById(1);

            OkObjectResult result = _getRegistroFuncionario as OkObjectResult;

            Assert.Equal(200, result.StatusCode);
        }
    }

    public class PostFuncionariosTest : BaseTest, IClassFixture<DbTeste>
    {
        private readonly ServiceProvider _serviceProvide;

        public PostFuncionariosTest(DbTeste dbTeste)
        {
            _serviceProvide = dbTeste.ServiceProvider;
        }

        [Fact]
        public async Task PostFuncionarios_RetornaStatusCode201()
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
    }

    public class PatchFuncionariosTest : BaseTest, IClassFixture<DbTeste>
    {
        private readonly ServiceProvider _serviceProvide;

        public PatchFuncionariosTest(DbTeste dbTeste)
        {
            _serviceProvide = dbTeste.ServiceProvider;
        }

        [Fact]
        public async Task PatchFuncionarios_RetornaStatusCode201()
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

            FuncionarioDTO NovoFuncionario = new()
            {
                Nome = "NovoTesteNomeFuncionario",
                CargoId = 1,
                Cpf = "11111111111",
                Email = "testenovo@teste",
                Senha = "testeSenhaNovo",
            };

            var _registroCargoCriado = await controllerCargo.Post(Cargo);

            var _registroFuncionarioCriado = await controllerFuncionario.Post(Funcionario);

            var _registroFuncionarioAtualizado = await controllerFuncionario.Patch(1, NovoFuncionario);

            CreatedResult result = _registroFuncionarioAtualizado as CreatedResult;

            Assert.Equal(201, result.StatusCode);
        }
    }

    public class DeleteFuncionariosTest : BaseTest, IClassFixture<DbTeste>
    {
        private readonly ServiceProvider _serviceProvide;

        public DeleteFuncionariosTest(DbTeste dbTeste)
        {
            _serviceProvide = dbTeste.ServiceProvider;
        }

        [Fact]
        public async Task DeleteFuncionarios_RetornaStatusCode200()
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

            var _registroDelete = await controllerFuncionario.Delete(1);

            OkObjectResult result = _registroDelete as OkObjectResult;

            Assert.Equal(200, result.StatusCode);
        }
    }
}
