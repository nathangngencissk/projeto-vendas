using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VendasMVC.Models
{
    public abstract class Categorias : Enumeration
    {
        public static Categorias Livros = new LivrosCategoria();
        public static Categorias Eletrodomesticos = new CategoriaEletrodomesticos();
        public static Categorias Roupas = new RoupasCategoria();

        protected Categorias(int id, string name)
            : base(id, name)
        { }

        private class LivrosCategoria : Categorias
        {
            public LivrosCategoria() : base(1, "Livros")
            { }
        }

        private class CategoriaEletrodomesticos : Categorias
        {
            public CategoriaEletrodomesticos() : base(2, "Eletrodomésticos")
            { }
        }

        private class RoupasCategoria : Categorias
        {
            public RoupasCategoria() : base(3, "Roupas")
            { }
        }

    }
}