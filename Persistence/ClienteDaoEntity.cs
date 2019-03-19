using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using VendasMVC.Models;

namespace VendasMVC.Persistence
{
    public class ClienteDaoEntity : IDao<Cliente>, IDisposable
    {
        public void Adicionar(Cliente cliente)
        {
            using (var contexto = new LojaContext())
            {
                contexto.Clientes.Add(cliente);
                contexto.SaveChanges();
            }
        }

        public void Alterar(Cliente cliente)
        {
            Cliente clienteAAlterar;
            using (var contexto = new LojaContext())
            {
                List<Cliente> lista = contexto.Clientes.ToList<Cliente>();
                clienteAAlterar = (from c in lista where c.IdCliente == cliente.IdCliente select c).First();

                clienteAAlterar.Nome = cliente.Nome;
                clienteAAlterar.Cpf = cliente.Cpf;


                contexto.SaveChanges();
            }
        }

        public void Dispose()
        {
            
        }

        public Cliente Pegar(int id)
        {
            Cliente cliente;
            using (var contexto = new LojaContext())
            {
                cliente = (from c in contexto.Clientes.ToList<Cliente>() where c.IdCliente == id select c).First();
            }

            return cliente;
        }

        public ICollection<Cliente> PegarLista()
        {
            List<Cliente> lista;
            using (var contexto = new LojaContext())
            {
                lista = contexto.Clientes.ToList();
            }
            return lista;
        }

        public void Remover(int id)
        {
            Cliente cliente;
            using (var contexto = new LojaContext())
            {
                cliente = (from c in contexto.Clientes.ToList<Cliente>() where c.IdCliente == id select c).First();
                contexto.Clientes.Attach(cliente);
                contexto.Entry(cliente).State = EntityState.Deleted;
                contexto.SaveChanges();
            }
        }
    }
}