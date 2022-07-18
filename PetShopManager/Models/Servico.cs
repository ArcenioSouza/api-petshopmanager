using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetShopManager.Models
{
    public class Servico
    {
        public int Id { get; set; }
        public string Tipo { get; set; }
        public double Valor { get; set; }
        public bool IsActive { get; set; }
    }
}