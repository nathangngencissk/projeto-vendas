using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace VendasMVC.Models
{
    [Table("CLIENTE")]
    public class Cliente
    {
        [Key]
        public int IdCliente { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public List<Venda> Compras { get; set; }

        public Cliente()
        {
            Compras = new List<Venda>();
        }
    }   
}