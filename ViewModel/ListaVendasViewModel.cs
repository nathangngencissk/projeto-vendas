using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VendasMVC.Models;

namespace VendasMVC.ViewModel
{
    public class ListaVendasViewModel
    {
        public List<Venda> Vendas { get; set; }
        public List<ProdutoVenda> Produtos { get; set; }
    }
}