using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace VendasMVC.Models
{
    [Table("VENDEDOR")]
    public class Vendedor
    {
        [Key]
        public int IdVendedor { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public byte[] Senha { get; set; }
        public string SaltSenha { get; set; }
        public int GrupoDeAcesso { get; set; }
    }
}