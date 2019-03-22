using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VendasMVC.Models;
using VendasMVC.Persistence;

namespace VendasMVC.ViewModel
{
    public class VendaFormViewModel
    {
        public Venda Venda { get; set; }
        public List<Categoria> Categorias { get; set; }
        public List<Produto> Produtos { get; set; }
        
        public VendaFormViewModel()
        {
            using (var dao = new CategoriaDaoEntity())
            {
                Categorias = dao.PegarLista() as List<Categoria>;
            }
            using (var dao = new ProdutoDaoEntity())
            {
                Produtos = dao.PegarLista() as List<Produto>;
            }
            Venda = new Venda();
        }
    }
}