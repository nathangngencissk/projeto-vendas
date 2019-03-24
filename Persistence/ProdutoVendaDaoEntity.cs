using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using VendasMVC.Models;

namespace VendasMVC.Persistence
{
    public class ProdutoVendaDaoEntity : IDao<ProdutoVenda>, IDisposable
    {
        public void Adicionar(ProdutoVenda produto)
        {
            using (var contexto = new LojaContext())
            {
                contexto.ProdutosVenda.Add(produto);
                contexto.SaveChanges();
            }
        }

        public void Alterar(ProdutoVenda produto)
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

        public ProdutoVenda Pegar(int id)
        {
            ProdutoVenda produto;
            using (var contexto = new LojaContext())
            {               
                produto = (from p in contexto.ProdutosVenda.ToList() where p.IdProduto == id select p).First();
            }

            return produto;
        }

        public ICollection<ProdutoVenda> PegarLista()
        {
            List<ProdutoVenda> lista;
            using (var contexto = new LojaContext())
            {
                lista = contexto.ProdutosVenda.ToList();
                foreach (ProdutoVenda pv in lista)
                {
                    pv.Produto = contexto.Produtos.Find(pv.IdProduto);
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