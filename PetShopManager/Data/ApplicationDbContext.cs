using Microsoft.EntityFrameworkCore;
using PetShopManager.Models;

namespace PetShopManager.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Animal> Animais { get; set; }
        public DbSet<Atendimento> Atendimentos { get; set; }
        public DbSet<Cargo> Cargos { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Funcionario> Funcionarios { get; set; }
        public DbSet<Servico> Servicos { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

    }
}