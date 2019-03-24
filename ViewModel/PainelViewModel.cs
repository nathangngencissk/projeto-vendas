using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VendasMVC.Models;

namespace VendasMVC.ViewModel
{
    public class PainelViewModel
    {
        public List<Venda> Vendas { get; set; }
        public List<Produto> Produtos { get; set; }
    }
}