using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetShopManager.Models
{
    public class Animal
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Sexo { get; set; }
        public double PesoAtual { get; set; }
        public double AlturaAtual { get; set; }
        public string Raca { get; set; }

        //Informações que virão da api a partir da raça
        //Link da requisição: https://api.thedogapi.com/v1/breeds/search?q=husky
        public string TempoDeVida { get; set; }
        public string Temperamento { get; set; }
        public string PesoMedio { get; set; }
        public string AlturaMedia { get; set; }
        public bool IsActive { get; set; }
    }
}