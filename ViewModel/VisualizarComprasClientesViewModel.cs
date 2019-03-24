using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VendasMVC.Models;

namespace VendasMVC.ViewModel
{
    public class VisualizarComprasClientesViewModel
    {
        public Cliente Cliente { get; set; }
        public List<Venda> Compras { get; set; }
        public List<ProdutoVenda> Produtos { get; set; }
        public decimal ValorTotal { get; set; }
        public decimal ValorMensal { get; set; }
        public int VolumeCompras { get; set; }
        public int VolumeComprasMes { get; set; }

    }
}