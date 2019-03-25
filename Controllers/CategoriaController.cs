using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VendasMVC.Models;
using VendasMVC.Models.Util;
using VendasMVC.Persistence;
using VendasMVC.ViewModel;

namespace VendasMVC.Controllers
{
    [SessionTimeout]
    public class CategoriaController : Controller
    {
        [HttpGet]
        [Route("categorias", Name = RouteNames.ListarCategorias)]
        public ActionResult ListarCategorias()
        {
            List<Categoria> lista;

            using (var dao = new CategoriaDaoEntity())
            {
                lista = dao.PegarLista() as List<Categoria>;
            }

            return View(lista);
        }

        [HttpPost]
        [Route("categorias/adicionar", Name = RouteNames.AdicionarCategoria)]
        public ActionResult AdicionarCategoria(Categoria categoria)
        {
            using (var dao = new CategoriaDaoEntity())
            {
                dao.Adicionar(categoria);
            }

            return RedirectToAction("ListarCategorias");
        }

        [Route("categorias/{id}", Name = RouteNames.VisualizarCategoria)]
        public ActionResult VisualizarCategoria(int id)
        {
            Categoria categoria = new Categoria();
            using (var dao = new CategoriaDaoEntity())
            {
                categoria = dao.Pegar(id);
            }
            return View(categoria);
        }

        [Route("categorias/top3/json", Name = "Top3Categorias")]
        public JsonResult Top3Categorias()
        {
            int id = Convert.ToInt32(System.Web.HttpContext.Current.Session["IdVendedor"].ToString());

            Vendedor vendedor;
            List<Venda> vendas = new List<Venda>();
            List<ProdutoVenda> produtos = new List<ProdutoVenda>();
            var categorias = new Dictionary<string, int>();

            using (var dao = new CategoriaDaoEntity())
            {
                List<Categoria> lista = dao.PegarLista() as List<Categoria>;
                foreach (var item in lista)
                {
                    categorias.Add(item.Nome, 0);
                }
            }

            List<int> ids = new List<int>();

            using (var dao = new VendedorDaoEntity())
            {
                vendedor = dao.Pegar(id);
            }
            using (var dao = new VendaDaoEntity())
            {
                List<Venda> lista = dao.PegarLista() as List<Venda>;
                vendas.AddRange(from v in lista where v.IdVendedor == vendedor.IdVendedor select v);

            }

            foreach (Venda venda in vendas)
            {
                ids.Add(venda.IdVenda);
            }
            using (var dao = new ProdutoVendaDaoEntity())
            {
                List<ProdutoVenda> lista = dao.PegarLista() as List<ProdutoVenda>;
                produtos.AddRange(from p in lista where ids.Contains(p.IdVenda) select p);
            }

            foreach (ProdutoVenda pv in produtos)
            {
                categorias[pv.Produto.Categoria.Nome] += 1 * pv.Quantidade;
            }

            Dictionary<string, int> val = new Dictionary<string, int>();
            var valores = from ele in categorias
                      orderby ele.Value descending
                      select ele;

            foreach (var item in valores)
            {
                val.Add(item.Key, item.Value);
            }

            Dictionary<string, int>.KeyCollection keys = val.Keys;

            return Json(new { categoria1 = keys.ElementAt(0),
                              categoria2 = keys.ElementAt(1),
                              categoria3 = keys.ElementAt(2),
                              valor1 = val[keys.ElementAt(0)],
                              valor2 = val[keys.ElementAt(1)],
                              valor3 = val[keys.ElementAt(2)]
            });
        }

        [HttpPost]
        [Route("categorias/alterar", Name = RouteNames.AlterarCategoria)]
        public ActionResult AlterarCategoria(Categoria categoria)
        {
            using (var dao = new CategoriaDaoEntity())
            {
                dao.Alterar(categoria);
            }

            return RedirectToAction("ListarCategorias");
        }

        [Route("categorias/{acao}/{id}", Name = RouteNames.FormCategoria)]
        public ActionResult Form(int id, string acao)
        {

            CategoriaFormViewModel vm = new CategoriaFormViewModel
            {
                Acao = $"../{acao}"
            };

            if (id == 0)
            {
                vm.Categoria = new Categoria();
            }
            else
            {
                using (var dao = new CategoriaDaoEntity())
                {
                    vm.Categoria = dao.Pegar(id);
                }
            }

            return View(vm);

        }

        [Route("categorias/remover/{id}", Name = RouteNames.RemoverCategoria)]
        public JsonResult Remover(int id)
        {
            using (var dao = new CategoriaDaoEntity())
            {
                dao.Remover(id);
            }
            return Json(new { });
        }
    }
}