using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using VendasMVC.Models;

namespace VendasMVC.Persistence
{
    public class VendaDaoEntity : IDao<Venda>, IDisposable
    {
        public void Adicionar(Venda item)
        {
            using (var contexto = new LojaContext())
            {
                contexto.Vendas.Add(item);
                contexto.SaveChanges();
            }
        }

        public void Alterar(Venda item)
        {
            using (var contexto = new LojaContext())
            {
                contexto.Entry(item).State = EntityState.Modified;
                contexto.SaveChanges();
            }                    
        }

        public void Dispose(){}

        public Venda Pegar(int id)
        {
            Venda venda;
            using (var contexto = new LojaContext())
            {
                venda = (from v in contexto.Vendas.ToList<Venda>() where v.IdVenda == id select v).FirstOrDefault();
            }
            return venda;
        }

        public ICollection<Venda> PegarLista()
        {
            List<Venda> lista;
            using (var contexto = new LojaContext())
            {
                lista = contexto.Vendas.ToList();
            }
            return lista;
        }

        public void Remover(int id)
        {
            using (var contexto = new LojaContext())
            {
                Venda venda = contexto.Vendas.Find(id);
                contexto.Vendas.Remove(venda);
                contexto.SaveChanges();
            }
        }
    }
}