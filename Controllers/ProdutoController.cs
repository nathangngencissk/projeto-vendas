using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VendasMVC.Models;
using VendasMVC.Persistence;
using VendasMVC.ViewModel;

namespace VendasMVC.Controllers
{
    public class ProdutoController : Controller
    {
        [Route("produtos", Name = "ListarProdutos")]
        public ActionResult ListarProdutos()
        {
            List<Produto> lista;
            using (var dao = new ProdutoDaoEntity())
            {
                lista = dao.PegarLista() as List<Produto>;
            }

            return View(lista);
        }

        [Route("produtos/adicionar", Name = "AdicionarProduto")]
        public ActionResult AdicionarProduto(Produto produto)
        {
            using (var dao = new ProdutoDaoEntity())
            {
                dao.Adicionar(produto);
            }

            return RedirectToAction("ListarProdutos");
        }

        [Route("produtos/{id}", Name = "VisualizarProduto")]
        public ActionResult VisualizarProduto(int id)
        {
            Produto produto = new Produto();
            using (var dao = new ProdutoDaoEntity())
            {
                produto = dao.Pegar(id);
            }
            return View(produto);
        }

        [Route("produtos/alterar", Name = "AlterarProduto")]
        public ActionResult AlterarProduto(Produto produto)
        {
            using (var dao = new ProdutoDaoEntity())
            {
                dao.Alterar(produto);
            }

            return RedirectToAction("ListarProdutos");
        }

        [Route("produtos/{acao}/{id}", Name = RouteNames.FormProduto)]
        public ActionResult Form(int id, string acao)
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
    }
}