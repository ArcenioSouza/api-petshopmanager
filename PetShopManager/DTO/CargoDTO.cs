using System.ComponentModel.DataAnnotations;

namespace PetShopManager.DTO
{
    public class CargoDTO
    {
        [Required(ErrorMessage = "Atributo {0} é obrigatório")]
        public string NomeCargo { get; set; }
    }
}