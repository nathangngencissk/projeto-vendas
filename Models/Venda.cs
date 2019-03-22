using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace VendasMVC.Models
{
    [Table("VENDA")]
    public class Venda
    {
        [Key]
        public int IdVenda { get; set; }

        [ForeignKey("Cliente")]
        public int IdCliente { get; set; }
        public Cliente Cliente { get; set; }

        [ForeignKey("Vendedor")]
        public int IdVendedor { get; set; }
        public Vendedor Vendedor { get; set; }

        public DateTime DataDaVenda { get; set; }
        
    }
}