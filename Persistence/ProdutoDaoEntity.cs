using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using VendasMVC.Models;

namespace VendasMVC.Persistence
{
    public class ProdutoDaoEntity : IDao<Produto>, IDisposable
    {
        public void Adicionar(Produto produto)
        {
            using (var contexto = new LojaContext())
            {
                contexto.Produtos.Add(produto);
                contexto.SaveChanges();
            }
        }

        public void Alterar(Produto produto)
        {
            using (var contexto = new LojaContext())
            {
                contexto.Entry(produto).State = EntityState.Modified;
                contexto.SaveChanges();
            }
        }

        public void Dispose()
        {

        }

        public Produto Pegar(int id)
        {
            Produto produto;
            using (var contexto = new LojaContext())
            {               
                produto = (from p in contexto.Produtos.ToList() where p.IdProduto == id select p).First();
                produto.Categoria = contexto.Categorias.Find(produto.IdCategoria);
            }

            return produto;
        }

        public ICollection<Produto> PegarLista()
        {
            List<Produto> lista;
            using (var contexto = new LojaContext())
            {
                lista = contexto.Produtos.ToList();
                foreach(Produto p in lista)
                {
                    p.Categoria = contexto.Categorias.Find(p.IdCategoria);
                }
            }
            return lista;
        }

        public void Remover(int id)
        {
            Produto produto;
            using (var contexto = new LojaContext())
            {
                produto = (from p in contexto.Produtos.ToList() where p.IdProduto == id select p).First();
                contexto.Produtos.Attach(produto);
                contexto.Entry(produto).State = EntityState.Deleted;
                contexto.SaveChanges();
            }
        }
    }
}