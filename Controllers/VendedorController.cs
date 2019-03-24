using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using VendasMVC.Models;
using VendasMVC.Models.Util;
using VendasMVC.Persistence;
using VendasMVC.ViewModel;

namespace VendasMVC.Controllers
{
    [SessionTimeout]
    public class VendedorController : Controller
    {
        [Route("vendedores", Name = RouteNames.ListarVendedores)]
        public ActionResult ListarVendedores()
        {
            List<Vendedor> lista;
            using (var dao = new VendedorDaoEntity())
            {
                lista = dao.PegarLista() as List<Vendedor>;
            }

            return View(lista);
        }

        [Route("vendedores/adicionar", Name = RouteNames.AdicionarVendedor)]
        public ActionResult AdicionarVendedor(Vendedor vendedor, string senha)
        {
            vendedor.SaltSenha = PasswordEncrypt.GetSalt();
            vendedor.Senha = PasswordEncrypt.GetHash(senha, vendedor.SaltSenha);

            using (var dao = new VendedorDaoEntity())
            {
                dao.Adicionar(vendedor);
            }

            return RedirectToAction("ListarVendedores");
        }

        [Route("vendedores/{id}", Name = RouteNames.VisualizarVendedor)]
        public ActionResult VisualizarVendedor(int id)
        {
            Vendedor vendedor;
            List<Venda> vendas = new List<Venda>();
            List<Venda> vendasDoMes = new List<Venda>();
            List<ProdutoVenda> produtos = new List<ProdutoVenda>();
            List<ProdutoVenda> produtosDoMes = new List<ProdutoVenda>();
            decimal ValorTotal = 0;
            decimal ValorMensal = 0;

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

            foreach(Venda venda in vendas)
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

        [Route("vendedores/alterar", Name = RouteNames.AlterarVendedor)]
        public ActionResult AlterarProduto(Vendedor vendedor)
        {
            using (var dao = new VendedorDaoEntity())
            {
                dao.Alterar(vendedor);
            }

            return RedirectToAction("ListarVendedores");
        }

        [Route("vendedores/{acao}/{id}", Name = RouteNames.FormVendedor)]
        public ActionResult Form(int id, string acao)
        {

            VendedorFormViewModel vm = new VendedorFormViewModel
            {
                Acao = $"../{acao}"
            };

            if (id == 0)
            {
                vm.Vendedor = new Vendedor();
            }
            else
            {
                using (var dao = new VendedorDaoEntity())
                {
                    vm.Vendedor = dao.Pegar(id);
                }
            }

            return View(vm);

        }

        [Route("vendedores/remover/{id}", Name = RouteNames.RemoverVendedor)]
        public JsonResult Remover(int id)
        {
            using (var dao = new VendedorDaoEntity())
            {
                dao.Remover(id);
            }
            return Json(new { });
        }
    }
}