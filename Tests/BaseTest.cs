using System;
using PetShopManager.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Tests
{
    public abstract class BaseTest
    {
        public BaseTest()
        {

        }
    }

    public class DbTeste : IDisposable
    {
        //Gerar um nome para o banco de dados
        private string dataBaseName = $"dbApiTeste_{Guid.NewGuid().ToString().Replace("-", string.Empty)}";

        //Variável que irá guardar a construção do servidor
        public ServiceProvider ServiceProvider {get; private set;}

        public DbTeste()
        {
            var serviceCollection = new ServiceCollection();
             string mySqlConnection = $"server=localhost;port=3306;database={dataBaseName};uid=root;password=Gftbrasil1705";

            serviceCollection.AddDbContext<ApplicationDbContext>(options =>
                options.UseMySql(mySqlConnection, ServerVersion.AutoDetect(mySqlConnection)), ServiceLifetime.Transient
            );

            ServiceProvider = serviceCollection.BuildServiceProvider();

            using var context = ServiceProvider.GetService<ApplicationDbContext>();
            context.Database.EnsureCreated();
        }

        public void Dispose()
        {
            using var context = ServiceProvider.GetService<ApplicationDbContext>();
            context.Database.EnsureDeleted();
        }
    }
}