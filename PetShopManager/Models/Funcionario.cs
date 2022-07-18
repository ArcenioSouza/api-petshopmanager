using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetShopManager.Models
{
    public class Funcionario
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public Cargo Cargo { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
    }
}