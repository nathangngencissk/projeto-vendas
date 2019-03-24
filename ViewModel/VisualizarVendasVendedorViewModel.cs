using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VendasMVC.Models;

namespace VendasMVC.ViewModel
{
    public class VisualizarVendasVendedorViewModel
    {
        public Vendedor Vendedor { get; set; }
        public List<Venda> Vendas { get; set; }
        public List<ProdutoVenda> Produtos { get; set; }
        public decimal ValorTotal { get; set; }
        public decimal ValorMensal { get; set; }
        public int VolumeVendas { get; set; }
        public int VolumeVendasMes { get; set; }

    }
}