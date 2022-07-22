using System;

namespace PetShopManager.Models
{
    public class Atendimento
    {
        public int Id { get; set; }
        public Servico Servico { get; set; }
        public Cliente Cliente { get; set; }
        public Funcionario Funcionario { get; set; }
        public Animal Animal { get; set; }
        public double PesoDoAnimalAtualizado { get; set; }
        public double AlturaDoAnimalAtualizado { get; set; }
        public DateTime DataAtendimento { get; set; }
        public string Diagnostico { get; set; }
        public string Observacoes { get; set; }
    }
}