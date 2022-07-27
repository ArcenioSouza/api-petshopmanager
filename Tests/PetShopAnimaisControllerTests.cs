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
    public class AnimaisControllerTests : BaseTest, IClassFixture<DbTeste>
    {
        private readonly ServiceProvider _serviceProvide;

        public AnimaisControllerTests(DbTeste dbTeste)
        {
            _serviceProvide = dbTeste.ServiceProvider;
        }

        [Fact, Priority(1)]
        public async Task Post_RetornaStatusCode201()
        {
            using var context = _serviceProvide.GetService<ApplicationDbContext>();

            AnimaisController controllerAnimal = new(context);
            ClientesController controllerCliente = new(context);

            AnimalDTO animal = new()
            {
                Nome = "TesteNomeAnimal",
                ClienteID = 1,
                Sexo = "Macho",
                PesoAtual = 10,
                AlturaAtual = 50,
                DataNascimento = "10/01/2020",
                Raca = "Tzu"
            };

            ClienteDTO cliente = new()
            {
                Nome = "ClienteTest",
                Cpf = "11111111111",
                Telefone = "11988887777",
                Email = "clienteteste@gmail.com",
                Senha = "testesenha",
            };

            var _registroClienteCriado = await controllerCliente.Post(cliente);

            var _registroAnimalCriado = await controllerAnimal.Post(animal);

            CreatedResult result = _registroAnimalCriado as CreatedResult;

            Assert.Equal(201, result.StatusCode);
        }

        [Fact, Priority(2)]
        public async Task Get_RetornaStatusCode200()
        {
            using var context = _serviceProvide.GetService<ApplicationDbContext>();

            AnimaisController controllerAnimal = new(context);

            var _getRegistroAnimal = await controllerAnimal.Get();

            OkObjectResult result = _getRegistroAnimal as OkObjectResult;

            Assert.Equal(200, result.StatusCode);
        }

        [Fact, Priority(3)]
        public async Task GetById_RetornaStatusCode200()
        {
            using var context = _serviceProvide.GetService<ApplicationDbContext>();

            AnimaisController controllerAnimal = new(context);

            var _getRegistroAnimal = await controllerAnimal.GetById(1);

            OkObjectResult result = _getRegistroAnimal as OkObjectResult;

            Assert.Equal(200, result.StatusCode);
        }

        [Fact, Priority(4)]
        public async Task GetRaca_RetornaStatusCode200()
        {
            using var context = _serviceProvide.GetService<ApplicationDbContext>();

            AnimaisController controllerAnimal = new(context);

            var _registroAnimalBuscado = await controllerAnimal.GetRaca("Siberian Husky");

            OkObjectResult result = _registroAnimalBuscado as OkObjectResult;

            Assert.Equal(200, result.StatusCode);
        }

        [Fact, Priority(5)]
        public async Task GetRandom_RetornaStatusCode200()
        {
            using var context = _serviceProvide.GetService<ApplicationDbContext>();

            AnimaisController controllerAnimal = new(context);

            var _registroAnimalBuscado = await controllerAnimal.GetDogsRandom();

            OkObjectResult result = _registroAnimalBuscado as OkObjectResult;

            Assert.Equal(200, result.StatusCode);
        }

        [Fact, Priority(6)]
        public async Task Patch_RetornaStatusCode201()
        {
            using var context = _serviceProvide.GetService<ApplicationDbContext>();

            AnimaisController controllerAnimal = new(context);

            AnimalDTO NovoAnimal = new()
            {
                Nome = "TesteNomeAnimalAtualizado",
                ClienteID = 1,
                Sexo = "Femea",
                PesoAtual = 10,
                AlturaAtual = 50,
                DataNascimento = "10/05/2020",
                Raca = "Tzu"
            };

            var _registroAnimalAtualizado = await controllerAnimal.Patch(1, NovoAnimal);

            CreatedResult result = _registroAnimalAtualizado as CreatedResult;

            Assert.Equal(201, result.StatusCode);
        }

        [Fact, Priority(7)]
        public async Task Delete_RetornaStatusCode200()
        {
            using var context = _serviceProvide.GetService<ApplicationDbContext>();

            AnimaisController controllerAnimal = new(context);

            var _registroDelete = await controllerAnimal.Delete(1);

            OkObjectResult result = _registroDelete as OkObjectResult;

            Assert.Equal(200, result.StatusCode);
        }
    }
}
