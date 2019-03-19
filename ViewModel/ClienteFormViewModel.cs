using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VendasMVC.Models;

namespace VendasMVC.ViewModel
{
    public class ClienteFormViewModel
    {
        public Cliente Cliente { get; set; }
        public string Acao { get; set; }
    }
}