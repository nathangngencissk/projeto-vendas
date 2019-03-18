using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using VendasMVC.Models;
using VendasMVC.Persistence;

namespace VendasMVC.Controllers
{
    public class ClienteController : Controller
    {
        [Route("/clientes", Name = "ListarClientes")]
        public ActionResult ListarClientes()
        {
            List<Cliente> lista;
            using (var contexto = new LojaContext())
            {
               lista = contexto.Clientes.ToList<Cliente>();
            }

            return View(lista);
        }

        [Route("/clientes/adiciona", Name = "AdicionarCliente")]
        public ActionResult AdicionarCliente(Cliente cliente)
        {
            using (var dao = new ClienteDaoEntity())
            {
                dao.Adicionar(cliente);
            }

            return RedirectToAction("ListarClientes");
        }

        [Route("/clientes/{id}", Name = "VisualizarCliente")]
        public ActionResult VisualizarCliente(int id)
        {
            Cliente cliente = new Cliente();
            using (var dao = new ClienteDaoEntity())
            {
                cliente = dao.Pegar(id);
            }
            return View(cliente);
        }

        [Route("/clientes/alterarCliente", Name = "AlterarCliente")]
        public ActionResult AlterarCliente(Cliente cliente)
        {
            using (var dao = new ClienteDaoEntity())
            {
                dao.Alterar(cliente);
            }

            return RedirectToAction("ListarClientes");
        }

    }
}