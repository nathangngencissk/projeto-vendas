using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VendasMVC.Models
{
    public class Cliente
    {
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public int IdCliente { get; set; }
        public List<Venda> Compras { get; set; }
    }
}