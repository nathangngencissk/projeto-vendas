using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VendasMVC.Models
{
    public abstract class Categoria : Enumeration
    {
        public static Categoria Livros = new LivrosCategoria();
        public static Categoria Eletrodomesticos = new CategoriaEletrodomesticos();
        public static Categoria Roupas = new RoupasCategoria();

        protected Categoria(int id, string name)
            : base(id, name)
        { }

        private class LivrosCategoria : Categoria
        {
            public LivrosCategoria() : base(1, "Livros")
            { }
        }

        private class CategoriaEletrodomesticos : Categoria
        {
            public CategoriaEletrodomesticos() : base(2, "Eletrodomésticos")
            { }
        }

        private class RoupasCategoria : Categoria
        {
            public RoupasCategoria() : base(3, "Roupas")
            { }
        }

    }
}