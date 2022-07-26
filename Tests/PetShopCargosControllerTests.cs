using Xunit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using PetShopManager.Data;
using PetShopManager.Controllers;
using PetShopManager.DTO;

namespace Tests
{
    public class GetCargosTest : BaseTest, IClassFixture<DbTeste>
    {
        private readonly ServiceProvider _serviceProvide;

        public GetCargosTest(DbTeste dbTeste)
        {
            _serviceProvide = dbTeste.ServiceProvider;
        }

        [Fact]
        public async Task GetCargos_RetornaStatusCode200()
        {
            using var context = _serviceProvide.GetService<ApplicationDbContext>();

            CargosController controllerCargo = new(context);

            CargoDTO Cargo = new()
            {
                NomeCargo = "TesteNomeCargo",
            };

            var _registroCargoCriado = await controllerCargo.Post(Cargo);

            var _getRegistroCargo = await controllerCargo.Get();

            OkObjectResult result = _getRegistroCargo as OkObjectResult;

            Assert.Equal(200, result.StatusCode);
        }
    }

    public class GetByIdCargosTest : BaseTest, IClassFixture<DbTeste>
    {
        private readonly ServiceProvider _serviceProvide;

        public GetByIdCargosTest(DbTeste dbTeste)
        {
            _serviceProvide = dbTeste.ServiceProvider;
        }

        [Fact]
        public async Task GetByIdCargos_RetornaStatusCode200()
        {
            using var context = _serviceProvide.GetService<ApplicationDbContext>();

            CargosController controllerCargo = new(context);

            CargoDTO Cargo = new()
            {
                NomeCargo = "TesteNomeCargo",
            };

            var _registroCargoCriado = await controllerCargo.Post(Cargo);

            var _getRegistroCargo = await controllerCargo.GetById(1);

            OkObjectResult result = _getRegistroCargo as OkObjectResult;

            Assert.Equal(200, result.StatusCode);
        }
    }

    public class PostCargosTest : BaseTest, IClassFixture<DbTeste>
    {
        private readonly ServiceProvider _serviceProvide;

        public PostCargosTest(DbTeste dbTeste)
        {
            _serviceProvide = dbTeste.ServiceProvider;
        }

        [Fact]
        public async Task PostCargos_RetornaStatusCode201()
        {
            using var context = _serviceProvide.GetService<ApplicationDbContext>();

            CargosController controllerCargo = new(context);

            CargoDTO Cargo = new()
            {
                NomeCargo = "TesteNomeCargo",
            };

            var _registroCargoCriado = await controllerCargo.Post(Cargo);

            CreatedResult result = _registroCargoCriado as CreatedResult;

            Assert.Equal(201, result.StatusCode);
        }
    }

    public class PatchCargosTest : BaseTest, IClassFixture<DbTeste>
    {
        private readonly ServiceProvider _serviceProvide;

        public PatchCargosTest(DbTeste dbTeste)
        {
            _serviceProvide = dbTeste.ServiceProvider;
        }

        [Fact]
        public async Task PatchCargos_RetornaStatusCode201()
        {
            using var context = _serviceProvide.GetService<ApplicationDbContext>();

            CargosController controllerCargo = new(context);

            CargoDTO Cargo = new()
            {
                NomeCargo = "TesteNomeCargo",
            };

            CargoDTO NovoCargo = new()
            {
                NomeCargo = "TesteNomeCargoAtualizado",
            };

            var _registroCargoCriado = await controllerCargo.Post(Cargo);

            var _registroCargoAtualizado = await controllerCargo.Patch(1, NovoCargo);

            CreatedResult result = _registroCargoAtualizado as CreatedResult;

            Assert.Equal(201, result.StatusCode);
        }
    }
    
    public class DeleteCargosTest : BaseTest, IClassFixture<DbTeste>
    {
        private readonly ServiceProvider _serviceProvide;

        public DeleteCargosTest(DbTeste dbTeste)
        {
            _serviceProvide = dbTeste.ServiceProvider;
        }

        [Fact]
        public async Task DeleteCargos_RetornaStatusCode200()
        {
            using var context = _serviceProvide.GetService<ApplicationDbContext>();

            CargosController controllerCargo = new(context);

            CargoDTO Cargo = new()
            {
                NomeCargo = "TesteNomeCargo",
            };

            var _registroCargoCriado = await controllerCargo.Post(Cargo);

            var _registroDelete = await controllerCargo.Delete(1);

            OkObjectResult result = _registroDelete as OkObjectResult;

            Assert.Equal(200, result.StatusCode);
        }
    }
}
