using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VendasMVC.Models;

namespace VendasMVC.Persistence
{
    public class VendedorDaoEntity : IDao<Vendedor>, IDisposable
    {
        public void Adicionar(Vendedor item)
        {
            using (var contexto = new LojaContext())
            {
                contexto.Vendedores.Add(item);
                contexto.SaveChanges();
            }
        }

        public void Alterar(Vendedor item)
        {
            throw new NotImplementedException();
        }

        public void Remover(Vendedor item)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            Console.WriteLine("Apagado");
        }
    }
}