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
    public class CargosControllerTests : BaseTest, IClassFixture<DbTeste>
    {
        private readonly ServiceProvider _serviceProvide;

        public CargosControllerTests(DbTeste dbTeste)
        {
            _serviceProvide = dbTeste.ServiceProvider;
        }

        [Fact, Priority(1)]
        public async Task Post_RetornaStatusCode201()
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

        [Fact, Priority(2)]
        public async Task Get_RetornaStatusCode200()
        {
            using var context = _serviceProvide.GetService<ApplicationDbContext>();

            CargosController controllerCargo = new(context);

            var _getRegistroCargo = await controllerCargo.Get();

            OkObjectResult result = _getRegistroCargo as OkObjectResult;

            Assert.Equal(200, result.StatusCode);
        }

        [Fact, Priority(3)]
        public async Task GetById_RetornaStatusCode200()
        {
            using var context = _serviceProvide.GetService<ApplicationDbContext>();

            CargosController controllerCargo = new(context);

            var _getRegistroCargo = await controllerCargo.GetById(1);

            OkObjectResult result = _getRegistroCargo as OkObjectResult;

            Assert.Equal(200, result.StatusCode);
        }

        [Fact, Priority(4)]
        public async Task Patch_RetornaStatusCode201()
        {
            using var context = _serviceProvide.GetService<ApplicationDbContext>();

            CargosController controllerCargo = new(context);

            CargoDTO NovoCargo = new()
            {
                NomeCargo = "TesteNomeCargoAtualizado",
            };

            var _registroCargoAtualizado = await controllerCargo.Patch(1, NovoCargo);

            CreatedResult result = _registroCargoAtualizado as CreatedResult;

            Assert.Equal(201, result.StatusCode);
        }

        [Fact, Priority(5)]
        public async Task Delete_RetornaStatusCode200()
        {
            using var context = _serviceProvide.GetService<ApplicationDbContext>();

            CargosController controllerCargo = new(context);

            var _registroDelete = await controllerCargo.Delete(1);

            OkObjectResult result = _registroDelete as OkObjectResult;

            Assert.Equal(200, result.StatusCode);
        }
    }
}
