using Xunit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using PetShopManager.Data;
using PetShopManager.Controllers;
using PetShopManager.DTO;

namespace Tests
{
    public class  GetAnimaisTest: BaseTest, IClassFixture<DbTeste>
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
}
