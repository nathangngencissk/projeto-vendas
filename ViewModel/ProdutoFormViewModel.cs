using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VendasMVC.Models;
using VendasMVC.Persistence;

namespace VendasMVC.ViewModel
{
    public class ProdutoFormViewModel
    {
        public Produto Produto { get; set; }
        public List<Categoria> Categorias { get; set; }
        public string Acao { get; set; }

        public ProdutoFormViewModel()
        {
            using (var dao = new CategoriaDaoEntity())
            {
                Categorias = dao.PegarLista() as List<Categoria>;
            }
        }

    }
}