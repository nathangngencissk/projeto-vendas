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
            Produto produtoAAlterar;
            using (var contexto = new LojaContext())
            {
                List<Produto> lista = contexto.Produtos.ToList();
                produtoAAlterar = (from p in contexto.Produtos.ToList() where p.IdProduto == produto.IdProduto select p).First();

                produtoAAlterar.Nome = produto.Nome;
                produtoAAlterar.ValorUnitario = produto.ValorUnitario;
                produtoAAlterar.Categoria = produto.Categoria;

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
            }

            return produto;
        }

        public ICollection<Produto> PegarLista()
        {
            List<Produto> lista;
            using (var contexto = new LojaContext())
            {
                lista = contexto.Produtos.ToList();
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