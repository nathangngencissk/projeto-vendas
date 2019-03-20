using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Web;
using VendasMVC.Models.Util;

namespace VendasMVC.Models
{
    [Table("CATEGORIA")]
    public class Categoria
    {
        [Key]
        public int IdCategoria { get; set; }
        public string Nome { get; set; }

    }
}