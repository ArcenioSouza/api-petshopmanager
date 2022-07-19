using System.ComponentModel.DataAnnotations;

namespace PetShopManager.Models
{
    public class AnimalDTO
    {
        [Required(ErrorMessage = "Atributo {0} é obrigatório")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Atributo {0} é obrigatório")]
        public int ClienteID {get; set;}
        [Required(ErrorMessage = "Atributo {0} é obrigatório")]
        public string Sexo { get; set; }
        public double PesoAtual { get; set; }
        public double AlturaAtual { get; set; }
        public string Raca { get; set; }
    }
}