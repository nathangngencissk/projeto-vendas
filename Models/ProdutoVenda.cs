using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace VendasMVC.Models
{
    [Table("PRODUTOVENDA")]
    public class ProdutoVenda
    {
        [Key]
        public int IdProdutoVenda { get; set; }

        [ForeignKey("Venda")]
        public int IdVenda { get; set; }
        public Venda Venda { get; set; }

        [ForeignKey("Produto")]
        public int IdProduto { get; set; }
        public Produto Produto { get; set; }

        public int Quantidade { get; set; }
        public decimal Valor { get; set; }

    }
}