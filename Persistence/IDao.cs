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
        T Pegar(int id);
        ICollection<T> PegarLista();
        void Remover(int id);
    }
}
