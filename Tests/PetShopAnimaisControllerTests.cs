using Xunit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using PetShopManager.Data;
using PetShopManager.Controllers;
using PetShopManager.DTO;

namespace Tests
{
    public class GetAnimaisTest : BaseTest, IClassFixture<DbTeste>
    {
        private readonly ServiceProvider _serviceProvide;

        public GetAnimaisTest(DbTeste dbTeste)
        {
            _serviceProvide = dbTeste.ServiceProvider;
        }

        [Fact]
        public async Task GetAnimais_RetornaStatusCode200()
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

            var _getRegistroAnimal = await controllerAnimal.Get();

            OkObjectResult result = _getRegistroAnimal as OkObjectResult;

            Assert.Equal(200, result.StatusCode);
        }
    }

    public class GetByIdAnimaisTest : BaseTest, IClassFixture<DbTeste>
    {
        private readonly ServiceProvider _serviceProvide;

        public GetByIdAnimaisTest(DbTeste dbTeste)
        {
            _serviceProvide = dbTeste.ServiceProvider;
        }

        [Fact]
        public async Task GetByIdAnimais_RetornaStatusCode200()
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

            var _getRegistroAnimal = await controllerAnimal.GetById(1);

            OkObjectResult result = _getRegistroAnimal as OkObjectResult;

            Assert.Equal(200, result.StatusCode);
        }
    }

    public class GetRacaAnimaisTest : BaseTest, IClassFixture<DbTeste>
    {
        private readonly ServiceProvider _serviceProvide;

        public GetRacaAnimaisTest(DbTeste dbTeste)
        {
            _serviceProvide = dbTeste.ServiceProvider;
        }

        [Fact]
        public async Task GetRaca_RetornaStatusCode200()
        {
            using var context = _serviceProvide.GetService<ApplicationDbContext>();

            AnimaisController controllerAnimal = new(context);

            var _registroAnimalBuscado = await controllerAnimal.GetRaca("Siberian Husky");

            OkObjectResult result = _registroAnimalBuscado as OkObjectResult;

            Assert.Equal(200, result.StatusCode);
        }
    }

    public class GetRandomAnimaisTest : BaseTest, IClassFixture<DbTeste>
    {
        private readonly ServiceProvider _serviceProvide;

        public GetRandomAnimaisTest(DbTeste dbTeste)
        {
            _serviceProvide = dbTeste.ServiceProvider;
        }

        [Fact]
        public async Task GetRandom_RetornaStatusCode200()
        {
            using var context = _serviceProvide.GetService<ApplicationDbContext>();

            AnimaisController controllerAnimal = new(context);

            var _registroAnimalBuscado = await controllerAnimal.GetDogsRandom();

            OkObjectResult result = _registroAnimalBuscado as OkObjectResult;

            Assert.Equal(200, result.StatusCode);
        }
    }

    public class PostAnimaisTest : BaseTest, IClassFixture<DbTeste>
    {
        private readonly ServiceProvider _serviceProvide;

        public PostAnimaisTest(DbTeste dbTeste)
        {
            _serviceProvide = dbTeste.ServiceProvider;
        }

        [Fact]
        public async Task PostAnimais_RetornaStatusCode201()
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
    }

    public class PatchAnimaisTest : BaseTest, IClassFixture<DbTeste>
    {
        private readonly ServiceProvider _serviceProvide;

        public PatchAnimaisTest(DbTeste dbTeste)
        {
            _serviceProvide = dbTeste.ServiceProvider;
        }

        [Fact]
        public async Task PatchAnimais_RetornaStatusCode201()
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

            var _registroClienteCriado = await controllerCliente.Post(cliente);

            var _registroAnimalCriado = await controllerAnimal.Post(animal);

            var _registroAnimalAtualizado = await controllerAnimal.Patch(1, NovoAnimal);

            CreatedResult result = _registroAnimalAtualizado as CreatedResult;

            Assert.Equal(201, result.StatusCode);
        }
    }

    public class DeleteAnimaisTest : BaseTest, IClassFixture<DbTeste>
    {
        private readonly ServiceProvider _serviceProvide;

        public DeleteAnimaisTest(DbTeste dbTeste)
        {
            _serviceProvide = dbTeste.ServiceProvider;
        }

        [Fact]
        public async Task DeleteAnimais_RetornaStatusCode200()
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

            var _registroDelete = await controllerAnimal.Delete(1);

            OkObjectResult result = _registroDelete as OkObjectResult;

            Assert.Equal(200, result.StatusCode);
        }
    }
}
