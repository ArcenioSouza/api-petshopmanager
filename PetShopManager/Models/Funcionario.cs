
namespace PetShopManager.Models
{
    public class Funcionario
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public Cargo Cargo { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public bool IsActive { get; set; }
    }
}