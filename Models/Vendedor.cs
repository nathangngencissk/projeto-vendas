using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VendasMVC.Models
{
    public class Vendedor
    {
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public int IdVendedor { get; set; }
        public List<Venda> Vendas { get; set; }
    }
}