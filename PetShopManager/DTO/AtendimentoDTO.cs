using System;
using System.ComponentModel.DataAnnotations;

namespace PetShopManager.DTO
{
    public class AtendimentoDTO
    {
        [Required(ErrorMessage = "Atributo {0} é obrigatório")]
        public int ServicoId { get; set; }
        [Required(ErrorMessage = "Atributo {0} é obrigatório")]
        public int ClienteId { get; set; }
        [Required(ErrorMessage = "Atributo {0} é obrigatório")]
        public int FuncionarioId { get; set; }
        [Required(ErrorMessage = "Atributo {0} é obrigatório")]
        public int AnimalId { get; set; }
        public double PesoDoAnimalAtualizado { get; set; }
        public double AlturaDoAnimalAtualizado { get; set; }
        public string Diagnostico { get; set; }
        public string Observacoes { get; set; }
    }

    public class AtendimentoPatchDTO
    {
        public string Diagnostico { get; set; }
        public string Observacoes { get; set; }
    }
}