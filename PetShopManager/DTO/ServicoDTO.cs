using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PetShopManager.DTO
{
    public class ServicoDTO
    {
        [Required(ErrorMessage = "Atributo {0} é obrigatório")]
        public string Tipo { get; set; }
        [Required(ErrorMessage = "Atributo {0} é obrigatório")]
        public double Valor { get; set; }
    }
}