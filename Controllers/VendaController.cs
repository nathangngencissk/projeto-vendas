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
    public class VendaController : Controller
    {
        [Route("vendas", Name = RouteNames.ListarVendas)]
        public ActionResult ListarVendas()
        {
            List<Venda> lista;
            using (var dao = new VendaDaoEntity())
            {
                lista = dao.PegarLista() as List<Venda>;
            }

            return View(lista);
        }

        [Route("vendas/adicionar", Name = RouteNames.AdicionarVenda)]
        public ActionResult AdicionarVenda(Venda venda)
        {
            using (var dao = new VendaDaoEntity())
            {
                dao.Adicionar(venda);
            }

            return RedirectToAction("ListarVendas");
        }

        [Route("vendas/{id}", Name = RouteNames.VisualizarVenda)]
        public ActionResult VisualizarVenda(int id)
        {
            Venda venda = new Venda();
            using (var dao = new VendaDaoEntity())
            {
                venda = dao.Pegar(id);
            }
            return View(venda);
        }

        [Route("vendas/alterar", Name = RouteNames.AlterarVenda)]
        public ActionResult AlterarCliente(Venda venda)
        {
            using (var dao = new VendaDaoEntity())
            {
                dao.Alterar(venda);
            }

            return RedirectToAction("ListarVendas");
        }

        [Route("vendas/{acao}/{id}", Name = RouteNames.FormVenda)]
        public ActionResult Form(int id, string acao)
        {

            VendaFormViewModel vm = new VendaFormViewModel
            {
                Acao = $"../{acao}"
            };

            if (id == 0)
            {
                vm.Venda = new Venda();
            }
            else
            {
                using (var dao = new VendaDaoEntity())
                {
                    vm.Venda = dao.Pegar(id);
                }
            }

            return View(vm);

        }
    }
}