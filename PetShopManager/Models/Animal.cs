
using System;

namespace PetShopManager.Models
{
    public class Animal
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public Cliente Cliente {get; set;}
        public string Sexo { get; set; }
        public double PesoAtual { get; set; }
        public double AlturaAtual { get; set; }
        public DateTime DataNascimento { get; set; }  
        public string Raca { get; set; }
        public string TempoDeVida { get; set; }
        public string Temperamento { get; set; }
        public string PesoMedio { get; set; }
        public string AlturaMedia { get; set; }
        public bool IsActive { get; set; }
    }
}