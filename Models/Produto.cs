using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VendasMVC.Models
{
    public class Produto
    {
        public int IdProduto { get; set; }
        public string Nome { get; set; }
        public double Valor { get; set; }
    }
}