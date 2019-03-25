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
    public class ProdutoController : Controller
    {
        [HttpGet]
        [Route("produtos", Name = RouteNames.ListarProdutos)]
        public ActionResult ListarProdutos()
        {
            List<Produto> lista;
            using (var dao = new ProdutoDaoEntity())
            {
                lista = dao.PegarLista() as List<Produto>;
            }

            return View(lista);
        }

        [HttpPost]
        [Route("produtos/adicionar", Name = RouteNames.AdicionarProduto)]
        public ActionResult AdicionarProduto(Produto produto)
        {
            if (ModelState.IsValid)
            {
                using (var dao = new ProdutoDaoEntity())
                {
                    dao.Adicionar(produto);
                }
            }

            return RedirectToAction("ListarProdutos");
        }

        [Route("produtos/{id}", Name = RouteNames.VisualizarProduto)]
        public ActionResult VisualizarProduto(int id)
        {
            if (ModelState.IsValid)
            {
                Produto produto = new Produto();
                using (var dao = new ProdutoDaoEntity())
                {
                    produto = dao.Pegar(id);
                }
                return View(produto);
            }
            else
            {
                return RedirectToAction("ListarProdutos");
            }
        }

        [Route("produtos/{id}/json", Name = "VisualizarProdutoJson")]
        public JsonResult VisualizarProdutoJson(int id)
        {
            Produto produto = new Produto();
            using (var dao = new ProdutoDaoEntity())
            {
                produto = dao.Pegar(id);
            }
            return Json(new { qtd = produto.QuantidadeEmEstoque,
                              nome = produto.Nome,
                              id = produto.IdProduto,
                              categoria = produto.Categoria.IdCategoria,
                              valor = produto.ValorUnitario });
        }

        [HttpPost]
        [Route("produtos/alterar", Name = RouteNames.AlterarProduto)]
        public ActionResult AlterarProduto(Produto produto)
        {
            if (ModelState.IsValid)
            {
                using (var dao = new ProdutoDaoEntity())
                {
                    dao.Alterar(produto);
                }
            }

            return RedirectToAction("ListarProdutos");
        }

        [Route("produtos/{acao}/{id}", Name = RouteNames.FormProduto)]
        public ActionResult Form(int id, string acao)
        {
            if (ModelState.IsValid)
            {
                ProdutoFormViewModel vm = new ProdutoFormViewModel
                {
                    Acao = $"../{acao}"
                };

                if (id == 0)
                {
                    vm.Produto = new Produto();
                }
                else
                {
                    using (var dao = new ProdutoDaoEntity())
                    {
                        vm.Produto = dao.Pegar(id);
                    }
                }

                return View(vm);
            }
            else
            {
                return RedirectToAction("ListarProdutos");
            }
        }

        [Route("produtos/remover/{id}", Name = RouteNames.RemoverProduto)]
        public JsonResult Remover(int id)
        {
            using (var dao = new ProdutoDaoEntity())
            {
                dao.Remover(id);
            }
            return Json(new { });
        }
    }
}