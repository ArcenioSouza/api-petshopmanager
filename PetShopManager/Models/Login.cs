using System.ComponentModel.DataAnnotations;

namespace PetShopManager.Models
{
    public class Login
    {
        [Required(ErrorMessage = "Atributo {0} é obrigatório")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Atributo {0} é obrigatório")]
        public string Senha { get; set; }
    }
}