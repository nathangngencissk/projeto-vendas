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
            Cliente cliente = new Cliente();
            using (var dao = new ClienteDaoEntity())
            {
                cliente = dao.Pegar(id);
            }
            return View(cliente);
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

    }
}