using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using VendasMVC.Models;
using VendasMVC.Models.Util;
using VendasMVC.Persistence;
using VendasMVC.ViewModel;

namespace VendasMVC.Controllers
{
    [SessionTimeout]
    public class ClienteController : Controller
    {
        [Route("clientes", Name = RouteNames.ListarClientes)]
        public ActionResult ListarClientes()
        {
            List<Cliente> lista;
            using (var dao = new ClienteDaoEntity())
            {
                lista = dao.PegarLista() as List<Cliente>;
            }

            return View(lista);
        }

        [Route("clientes/adicionar", Name = RouteNames.AdicionarCliente)]
        public ActionResult AdicionarCliente(Cliente cliente)
        {
            using (var dao = new ClienteDaoEntity())
            {
                dao.Adicionar(cliente);
            }

            return RedirectToAction("ListarClientes");
        }

        [Route("clientes/{id}", Name = RouteNames.VisualizarCliente)]
        public ActionResult VisualizarCliente(int id)
        {
            Cliente cliente;
            List<Venda> comprasDoCliente = new List<Venda>();
            List<Venda> comprasDoMes = new List<Venda>();
            List<ProdutoVenda> produtos;
            List<ProdutoVenda> produtosDoMes = new List<ProdutoVenda>();
            decimal ValorTotal = 0;
            decimal ValorMensal = 0;
            using (var dao = new ClienteDaoEntity())
            {
                cliente = dao.Pegar(id);
            }
            using (var dao = new VendaDaoEntity())
            {
                List<Venda> lista = dao.PegarLista() as List<Venda>;
                comprasDoCliente.AddRange(from v in lista where v.IdCliente == cliente.IdCliente select v);
            }
            using (var dao = new ProdutoVendaDaoEntity())
            {
                produtos = dao.PegarLista() as List<ProdutoVenda>;
            }

            foreach (ProdutoVenda pv in produtos)
            {
                ValorTotal += pv.Valor;
            }
            
            comprasDoMes.AddRange(from v in comprasDoCliente where v.DataDaVenda.Month.Equals(DateTime.Today.Month) select v);
            List<int> ids = new List<int>();
            foreach(Venda compra in comprasDoMes)
            {
                ids.Add(compra.IdVenda);
            }

            produtosDoMes.AddRange(from p in produtos where ids.Contains(p.IdVenda) select p);
            foreach (ProdutoVenda pv in produtosDoMes)
            {
                ValorMensal += pv.Valor;
            }

            VisualizarComprasClientesViewModel vm = new VisualizarComprasClientesViewModel
            {
                Cliente = cliente,
                Compras = comprasDoCliente,
                Produtos = produtos,
                ValorTotal = ValorTotal,
                ValorMensal = ValorMensal,
                VolumeCompras = comprasDoCliente.Count,
                VolumeComprasMes = comprasDoMes.Count
            };

            return View(vm);
        }

        [Route("clientes/alterar", Name = RouteNames.AlterarCliente)]
        public ActionResult AlterarCliente(Cliente cliente)
        {
            using (var dao = new ClienteDaoEntity())
            {
                dao.Alterar(cliente);
            }

            return RedirectToAction("ListarClientes");
        }

        [Route("clientes/{acao}/{id}", Name = RouteNames.FormCliente)]
        public ActionResult Form(int id, string acao)
        {

            ClienteFormViewModel vm = new ClienteFormViewModel
            {
                Acao = $"../{acao}"
            };

            if (id == 0)
            {
                vm.Cliente = new Cliente();
            } else
            {
                using (var dao = new ClienteDaoEntity())
                {
                    vm.Cliente = dao.Pegar(id);
                }
            }

            return View(vm);
            
        }

        [Route("clientes/remover/{id}", Name = RouteNames.RemoverCliente)]
        public JsonResult Remover(int id)
        {
            using (var dao = new ClienteDaoEntity())
            {
                dao.Remover(id);
            }
            return Json(new { });
        }

    }
}