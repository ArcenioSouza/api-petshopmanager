using Xunit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using PetShopManager.Data;
using PetShopManager.Controllers;
using PetShopManager.DTO;

namespace Tests
{
    public class GetServicosTest : BaseTest, IClassFixture<DbTeste>
    {
        private readonly ServiceProvider _serviceProvide;

        public GetServicosTest(DbTeste dbTeste)
        {
            _serviceProvide = dbTeste.ServiceProvider;
        }

        [Fact]
        public async Task GetServicos_RetornaStatusCode200()
        {
            using var context = _serviceProvide.GetService<ApplicationDbContext>();

            ServicosController controllerServico = new(context);

            ServicoDTO Servico = new()
            {
                Tipo = "Teste Serviço",
                Valor= 10
            };

            var _registroServicoCriado = await controllerServico.Post(Servico);

            var _getRegistroServico = await controllerServico.Get();

            OkObjectResult result = _getRegistroServico as OkObjectResult;

            Assert.Equal(200, result.StatusCode);
        }
    }

    public class GetByIdServicosTest : BaseTest, IClassFixture<DbTeste>
    {
        private readonly ServiceProvider _serviceProvide;

        public GetByIdServicosTest(DbTeste dbTeste)
        {
            _serviceProvide = dbTeste.ServiceProvider;
        }

        [Fact]
        public async Task GetByIdServicos_RetornaStatusCode200()
        {
            using var context = _serviceProvide.GetService<ApplicationDbContext>();

            ServicosController controllerServico = new(context);

            ServicoDTO Servico = new()
            {
                Tipo = "Teste Serviço",
                Valor= 10
            };

            var _registroServicoCriado = await controllerServico.Post(Servico);

            var _getRegistroServico = await controllerServico.GetById(1);

            OkObjectResult result = _getRegistroServico as OkObjectResult;

            Assert.Equal(200, result.StatusCode);
        }
    }

    public class PostServicosTest : BaseTest, IClassFixture<DbTeste>
    {
        private readonly ServiceProvider _serviceProvide;

        public PostServicosTest(DbTeste dbTeste)
        {
            _serviceProvide = dbTeste.ServiceProvider;
        }

        [Fact]
        public async Task PostServicos_RetornaStatusCode201()
        {
            using var context = _serviceProvide.GetService<ApplicationDbContext>();

            ServicosController controllerServico = new(context);

            ServicoDTO Servico = new()
            {
                Tipo = "Teste Serviço",
                Valor= 10
            };

            var _registroServicoCriado = await controllerServico.Post(Servico);

            CreatedResult result = _registroServicoCriado as CreatedResult;

            Assert.Equal(201, result.StatusCode);
        }
    }
    
    public class PatchServicosTest : BaseTest, IClassFixture<DbTeste>
    {
        private readonly ServiceProvider _serviceProvide;

        public PatchServicosTest(DbTeste dbTeste)
        {
            _serviceProvide = dbTeste.ServiceProvider;
        }

        [Fact]
        public async Task PatchServicos_RetornaStatusCode201()
        {
            using var context = _serviceProvide.GetService<ApplicationDbContext>();

            ServicosController controllerServico = new(context);

            ServicoDTO Servico = new()
            {
                Tipo = "Teste Serviço",
                Valor= 10
            };

            ServicoDTO NovoServico = new()
            {
                Tipo = "Teste Serviço 2",
                Valor= 20
            };

            var _registroServicoCriado = await controllerServico.Post(Servico);

            var _registroServicoAtualizado = await controllerServico.Patch(1, NovoServico);

            CreatedResult result = _registroServicoAtualizado as CreatedResult;

            Assert.Equal(201, result.StatusCode);
        }
    }
    
    public class DeleteServicosTest : BaseTest, IClassFixture<DbTeste>
    {
        private readonly ServiceProvider _serviceProvide;

        public DeleteServicosTest(DbTeste dbTeste)
        {
            _serviceProvide = dbTeste.ServiceProvider;
        }

        [Fact]
        public async Task DeleteServicos_RetornaStatusCode200()
        {
            using var context = _serviceProvide.GetService<ApplicationDbContext>();

            ServicosController controllerServico = new(context);

             ServicoDTO Servico = new()
            {
                Tipo = "Teste Serviço",
                Valor= 10
            };

            var _registroCargoCriado = await controllerServico.Post(Servico);

            var _registroDelete = await controllerServico.Delete(1);

            OkObjectResult result = _registroDelete as OkObjectResult;

            Assert.Equal(200, result.StatusCode);
        }
    }
}
