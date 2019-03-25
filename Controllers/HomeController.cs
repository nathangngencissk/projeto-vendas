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
            int id = Convert.ToInt32(System.Web.HttpContext.Current.Session["IdVendedor"].ToString());

            Vendedor vendedor;
            List<Venda> vendas = new List<Venda>();
            List<Venda> vendasDoMes = new List<Venda>();
            List<ProdutoVenda> produtos = new List<ProdutoVenda>();
            List<ProdutoVenda> produtosDoMes = new List<ProdutoVenda>();
            decimal ValorTotal = 0;
            decimal ValorMensal = 0;
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
                ValorTotal += pv.Valor;                
            }
            ids.Clear();
            vendasDoMes.AddRange(from v in vendas where v.DataDaVenda.Month.Equals(DateTime.Today.Month) select v);

            foreach (Venda venda in vendasDoMes)
            {
                ids.Add(venda.IdVenda);
            }

            produtosDoMes.AddRange(from p in produtos where ids.Contains(p.IdVenda) select p);
            foreach (ProdutoVenda pv in produtosDoMes)
            {
                ValorMensal += pv.Valor;
            }           

            VisualizarVendasVendedorViewModel vm = new VisualizarVendasVendedorViewModel
            {
                Vendedor = vendedor,
                Vendas = vendas,
                Produtos = produtos,
                ValorTotal = ValorTotal,
                ValorMensal = ValorMensal,
                VolumeVendas = vendas.Count,
                VolumeVendasMes = vendasDoMes.Count
            };

            return View(vm);
        }
    }
}