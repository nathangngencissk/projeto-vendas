using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using VendasMVC.Models;

namespace VendasMVC.Persistence
{
    public class CategoriaDaoEntity : IDao<Categoria>, IDisposable
    {
        public void Adicionar(Categoria item)
        {
            using (var contexto = new LojaContext())
            {
                contexto.Categorias.Add(item);
                contexto.SaveChanges();
            }
        }

        public void Alterar(Categoria item)
        {
            using (var contexto = new LojaContext())
            {
                contexto.Entry(item).State = EntityState.Modified;
                contexto.SaveChanges();
            }                    
        }

        public void Dispose(){}

        public Categoria Pegar(int id)
        {
            Categoria categoria;
            using (var contexto = new LojaContext())
            {
                categoria = (from c in contexto.Categorias.ToList<Categoria>() where c.IdCategoria == id select c).FirstOrDefault();
            }
            return categoria;
        }

        public ICollection<Categoria> PegarLista()
        {
            using (var contexto = new LojaContext())
            {
                return contexto.Categorias.ToList() as ICollection<Categoria>;
            }
        }

        public void Remover(int id)
        {
            using (var contexto = new LojaContext())
            {
                Categoria categoria = contexto.Categorias.Find(id);
                contexto.Categorias.Remove(categoria);
                contexto.SaveChanges();
            }
        }
    }
}