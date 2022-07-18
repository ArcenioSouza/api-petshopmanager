using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetShopManager.Models
{
    public class Atendimento
    {
        public int Id { get; set; }
        public string Diagnostico { get; set; }
        public string Observacoes { get; set; }
        public Cliente Cliente { get; set; }
        public Funcionario Funcionario { get; set; }
        public Animal Animal { get; set; }
        public DateTime DataAtendimento { get; set; }
    }
}