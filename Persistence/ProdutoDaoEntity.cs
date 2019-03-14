using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VendasMVC.Models;

namespace VendasMVC.Persistence
{
    public class ProdutoDaoEntity : IDao<Produto>, IDisposable
    {
        public void Adicionar(Produto item)
        {
            using (var contexto = new LojaContext())
            {
                contexto.Produtos.Add(item);
                contexto.SaveChanges();
            }
        }

        public void Alterar(Produto item)
        {
            throw new NotImplementedException();
        }

        public void Remover(Produto item)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            Console.WriteLine("Apagado");
        }
    }
}