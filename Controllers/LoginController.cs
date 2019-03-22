using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using VendasMVC.Models;
using VendasMVC.Models.Util;
using VendasMVC.Persistence;
using VendasMVC.ViewModel;

namespace VendasMVC.Controllers
{
    public class LoginController : Controller
    {
        [Route("login", Name = "IndexLogin")]
        public ActionResult Index()
        {
            return View();
        }

        [Route("login/logout", Name = "Logout")]
        public ActionResult Logout()
        {
            System.Web.HttpContext.Current.Session["IdVendedor"] = null;
            System.Web.HttpContext.Current.Session["NomeVendedor"] = null;
            System.Web.HttpContext.Current.Session["GrupoDeAcessoVendedor"] = null;
            return View("Index");
        }

        [Route("login/logar", Name = "Login")]
        public ActionResult Login(VendedorLogin vendedorLogin)
        {        
            Vendedor vendedor;
            string salt;
            using (var dao = new VendedorDaoEntity())
            {
                vendedor = dao.Pegar(vendedorLogin.Cpf);
                salt = vendedor.SaltSenha;
            }

            ErroLoginViewModel vm = new ErroLoginViewModel
            {
                Mensagem = "Ocorreu um erro, tente novamente"
            };

            if (vendedor.IdVendedor == 0)
            {
                return View("Index", vm);
            }
            else
            {       
                HttpContext ctx = System.Web.HttpContext.Current;
                if (PasswordEncrypt.CompareHash(vendedorLogin.Senha, vendedor.Senha, salt))
                {
                    System.Web.HttpContext.Current.Session["IdVendedor"] = vendedor.IdVendedor;
                    System.Web.HttpContext.Current.Session["NomeVendedor"] = vendedor.Nome;
                    System.Web.HttpContext.Current.Session["GrupoDeAcessoVendedor"] = vendedor.GrupoDeAcesso;
                    return Redirect("/");
                }
                else
                {
                    return View("Index", vm);
                }             
            }           
        }
    }
}