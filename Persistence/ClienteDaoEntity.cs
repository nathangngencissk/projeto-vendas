using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VendasMVC.Models;

namespace VendasMVC.Persistence
{
    public class ClienteDaoEntity : IDao<Cliente>, IDisposable
    {
        public void Adicionar(Cliente item)
        {
            using (var contexto = new LojaContext())
            {
                contexto.Clientes.Add(item);
                contexto.SaveChanges();
            }
        }

        public void Alterar(Cliente item)
        {
            throw new NotImplementedException();
        }


        public void Remover(Cliente item)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            Console.WriteLine("Apagado");
        }
    }
}