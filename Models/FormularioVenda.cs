using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VendasMVC.Models
{
    public class FormularioVenda
    {
        public Venda Venda { get; set; }
        public List<ProdutoVenda> Produtos { get; set; }
        public string Cpf { get; set; }
        public string Senha { get; set; }

        public FormularioVenda()
        {
            Venda = new Venda();
            Produtos = new List<ProdutoVenda>();
        }
    }
}