using System.ComponentModel.DataAnnotations;

namespace PetShopManager.DTO
{
    public class ClienteDTO
    {        
        [Required(ErrorMessage = "Atributo {0} é obrigatório")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Atributo {0} é obrigatório")]
        public string Cpf { get; set; }
        [Required(ErrorMessage = "Atributo {0} é obrigatório")]
        public string Telefone { get; set; }
        [Required(ErrorMessage = "Atributo {0} é obrigatório")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required(ErrorMessage = "Atributo {0} é obrigatório")]
        [DataType(DataType.Password)]
        public string Senha { get; set; }  
    }
}