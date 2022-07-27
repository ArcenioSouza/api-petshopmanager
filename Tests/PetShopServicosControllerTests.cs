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
    public class ServicosControllerTests : BaseTest, IClassFixture<DbTeste>
    {
        private readonly ServiceProvider _serviceProvide;

        public ServicosControllerTests(DbTeste dbTeste)
        {
            _serviceProvide = dbTeste.ServiceProvider;
        }

        [Fact, Priority(1)]
        public async Task Post_RetornaStatusCode201()
        {
            using var context = _serviceProvide.GetService<ApplicationDbContext>();

            ServicosController controllerServico = new(context);

            ServicoDTO Servico = new()
            {
                Tipo = "Teste Serviço",
                Valor = 10
            };

            var _registroServicoCriado = await controllerServico.Post(Servico);

            CreatedResult result = _registroServicoCriado as CreatedResult;

            Assert.Equal(201, result.StatusCode);
        }

        [Fact, Priority(2)]
        public async Task Get_RetornaStatusCode200()
        {
            using var context = _serviceProvide.GetService<ApplicationDbContext>();

            ServicosController controllerServico = new(context);

            var _getRegistroServico = await controllerServico.Get();

            OkObjectResult result = _getRegistroServico as OkObjectResult;

            Assert.Equal(200, result.StatusCode);
        }

        [Fact, Priority(3)]
        public async Task GetById_RetornaStatusCode200()
        {
            using var context = _serviceProvide.GetService<ApplicationDbContext>();

            ServicosController controllerServico = new(context);

            var _getRegistroServico = await controllerServico.GetById(1);

            OkObjectResult result = _getRegistroServico as OkObjectResult;

            Assert.Equal(200, result.StatusCode);
        }

        [Fact, Priority(4)]
        public async Task Patch_RetornaStatusCode201()
        {
            using var context = _serviceProvide.GetService<ApplicationDbContext>();

            ServicosController controllerServico = new(context);

            ServicoDTO NovoServico = new()
            {
                Tipo = "Teste Serviço 2",
                Valor = 20
            };

            var _registroServicoAtualizado = await controllerServico.Patch(1, NovoServico);

            CreatedResult result = _registroServicoAtualizado as CreatedResult;

            Assert.Equal(201, result.StatusCode);
        }

        [Fact, Priority(5)]
        public async Task Delete_RetornaStatusCode200()
        {
            using var context = _serviceProvide.GetService<ApplicationDbContext>();

            ServicosController controllerServico = new(context);

            var _registroDelete = await controllerServico.Delete(1);

            OkObjectResult result = _registroDelete as OkObjectResult;

            Assert.Equal(200, result.StatusCode);
        }
    }
}
