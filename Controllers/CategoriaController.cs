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
        public ActionResult VisualizarCliente(int id)
        {
            Categoria categoria = new Categoria();
            using (var dao = new CategoriaDaoEntity())
            {
                categoria = dao.Pegar(id);
            }
            return View(categoria);
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