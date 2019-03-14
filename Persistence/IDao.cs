using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendasMVC.Persistence
{
    interface IDao<T>
    {
        void Adicionar(T item);
        void Alterar(T item);
        void Remover(T item);
    }
}
