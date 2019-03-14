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
        [Route("/clientes", Name = "ListaClientes")]
        public ActionResult Index()
        {
            List<Cliente> lista;
            using (var contexto = new LojaContext())
            {
               lista = contexto.Clientes.ToList<Cliente>();
            }

            return View(lista);
        }

        [Route("/clientes/adicionaCliente", Name = "AdicionaCliente")]
        public ActionResult AdicionaCliente(Cliente cliente)
        {
            ViewBag.Cliente = new Cliente();
            ViewBag.Action = "../adiciona";
            return View("Form");
        }

        [Route("/clientes/adiciona", Name = "Adiciona")]
        public ActionResult Adiciona(Cliente cliente)
        {
            using (var contexto = new LojaContext())
            {
                contexto.Clientes.Add(cliente);
            }

            return RedirectToAction("Index");
        }

        [Route("/clientes/{id}", Name = "VisualizaCliente")]
        public ActionResult VisualizaCliente(int id)
        {
            Cliente cliente;
            using (var contexto = new LojaContext())
            {
                cliente = (from c in contexto.Clientes.ToList<Cliente>() where c.IdCliente == id select c).First();
            }
            return View(cliente);
        }

        [Route("/clientes/{id}/alterarCliente", Name = "AlterarCliente")]
        public ActionResult AlterarCliente(int id)
        {
            Cliente cliente;
            using (var contexto = new LojaContext())
            {
                cliente = (from c in contexto.Clientes.ToList<Cliente>() where c.IdCliente == id select c).First();
            }
            ViewBag.Cliente = cliente;
            ViewBag.Action = "../alterar";
            return View("Form");
        }

        [Route("/clientes/alterar", Name = "Alterar")]
        public ActionResult Alterar(Cliente cliente)
        {
            Cliente clienteAAlterar;
            using (var contexto = new LojaContext())
            {
                clienteAAlterar = (from c in contexto.Clientes.ToList<Cliente>() where c.IdCliente == cliente.IdCliente select c).First();

                clienteAAlterar.Nome = cliente.Nome;
                clienteAAlterar.Cpf = cliente.Cpf;


                contexto.Clientes.Attach(clienteAAlterar);

                // EF now tracks the object, any new changes will be applied

                foreach (PropertyInfo property in typeof(Cliente).GetProperties())
                {
                    if (property.)
                    {
                        // Set the value to view in debugger - should be dynamic cast eventually
                        var value = Convert.ToInt16(dbFields[property.Name]);
                        property.SetValue(objToUpdate, value);
                    }
                }

                // Will only perform an UPDATE query, no SELECT at all
                db.SaveChanges();
                typeof(Cliente).GetProperties());
            }

            return RedirectToAction("Index");
        }
    }
}