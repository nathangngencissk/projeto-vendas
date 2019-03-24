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
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            List<Venda> vendas;
            using (var dao = new VendaDaoEntity())
            {
                vendas = dao.PegarLista() as List<Venda>;
            }

            //Se for vendedor filtra a lista para apenas as vendas daquele vendedor
            //Se for gerente recebe a lista de vendas completa
            if (System.Web.HttpContext.Current.Session["GrupoDeAcessoVendedor"].Equals(1)) {
                vendas = (from v in vendas where v.IdVendedor.Equals(System.Web.HttpContext.Current.Session["IdVendedor"]) select v) as List<Venda>;
            }

            List<Produto> produtos;

            using (var dao = new ProdutoDaoEntity())
            {
                produtos = dao.PegarLista() as List<Produto>;
            }

            PainelViewModel vm = new PainelViewModel
            {
                Vendas = vendas,
                Produtos = produtos
            };

            return View(vm);
        }
    }
}