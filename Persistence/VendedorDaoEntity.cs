using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using VendasMVC.Models;

namespace VendasMVC.Persistence
{
    public class VendedorDaoEntity : IDao<Vendedor>, IDisposable
    {
        public void Adicionar(Vendedor vendedor)
        {
            using (var contexto = new LojaContext())
            {
                contexto.Vendedores.Add(vendedor);
                contexto.SaveChanges();
            }
        }

        public void Alterar(Vendedor vendedor)
        {
            Vendedor vendedorAAlterar;
            using (var contexto = new LojaContext())
            {
                List<Vendedor> lista = contexto.Vendedores.ToList<Vendedor>();
                vendedorAAlterar = (from v in lista where v.IdVendedor == vendedor.IdVendedor select v).First();

                vendedorAAlterar.Nome = vendedor.Nome;
                vendedorAAlterar.Cpf = vendedor.Cpf;


                contexto.SaveChanges();
            }
        }

        public void Dispose()
        {

        }

        public Vendedor Pegar(int id)
        {
            Vendedor vendedor;
            using (var contexto = new LojaContext())
            {
                List<Vendedor> lista = contexto.Vendedores.ToList<Vendedor>();
                vendedor = (from v in lista where v.IdVendedor == id select v).First();
            }

            return vendedor;
        }

        public Vendedor Pegar(string cpf)
        {
            Vendedor vendedor;
            using (var contexto = new LojaContext())
            {
                List<Vendedor> lista = contexto.Vendedores.ToList<Vendedor>();
                vendedor = (from v in lista where v.Cpf == cpf select v).First();
            }

            return vendedor;
        }

        public ICollection<Vendedor> PegarLista()
        {
            List<Vendedor> lista;
            using (var contexto = new LojaContext())
            {
                lista = contexto.Vendedores.ToList<Vendedor>();
            }
            return lista;
        }

        public void Remover(int id)
        {
            using (var contexto = new LojaContext())
            {
                List<Vendedor> lista = contexto.Vendedores.ToList<Vendedor>();
                Vendedor vendedor = (from v in lista where v.IdVendedor == id select v).First();
                contexto.Vendedores.Attach(vendedor);
                contexto.Entry(vendedor).State = EntityState.Deleted;
                contexto.SaveChanges();
            }
        }
    }
}