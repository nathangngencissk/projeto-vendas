﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VendasMVC.Models
{
    public class Venda
    {
        public int IdVenda { get; set; }
        public int IdCliente { get; set; }
        public int IdVendedor { get; set; }
        public DateTime DataDaVenda { get; set; }
        public List<Produto> Produtos { get; set; }
        public decimal ValorTotal { get; set; }
        
    }
}