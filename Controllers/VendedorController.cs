using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using VendasMVC.Models;
using VendasMVC.Models.Util;
using VendasMVC.Persistence;
using VendasMVC.ViewModel;

namespace VendasMVC.Controllers
{
    [SessionTimeout]
    public class VendedorController : Controller
    {
        [Route("vendedores", Name = RouteNames.ListarVendedores)]
        public ActionResult ListarVendedores()
        {
            List<Vendedor> lista;
            using (var dao = new VendedorDaoEntity())
            {
                lista = dao.PegarLista() as List<Vendedor>;
            }

            return View(lista);
        }

        [Route("vendedores/adicionar", Name = RouteNames.AdicionarVendedor)]
        public ActionResult AdicionarVendedor(Vendedor vendedor, string senha)
        {
            vendedor.SaltSenha = PasswordEncrypt.GetSalt();
            vendedor.Senha = PasswordEncrypt.GetHash(senha, vendedor.SaltSenha);

            using (var dao = new VendedorDaoEntity())
            {
                dao.Adicionar(vendedor);
            }

            return RedirectToAction("ListarVendedores");
        }

        [Route("vendedores/{id}", Name = RouteNames.VisualizarVendedor)]
        public ActionResult VisualizarProduto(int id)
        {
            Vendedor vendedor = new Vendedor();
            using (var dao = new VendedorDaoEntity())
            {
                vendedor = dao.Pegar(id);
            }
            return View(vendedor);
        }

        [Route("vendedores/alterar", Name = RouteNames.AlterarVendedor)]
        public ActionResult AlterarProduto(Vendedor vendedor)
        {
            using (var dao = new VendedorDaoEntity())
            {
                dao.Alterar(vendedor);
            }

            return RedirectToAction("ListarVendedores");
        }

        [Route("vendedores/{acao}/{id}", Name = RouteNames.FormVendedor)]
        public ActionResult Form(int id, string acao)
        {

            VendedorFormViewModel vm = new VendedorFormViewModel
            {
                Acao = $"../{acao}"
            };

            if (id == 0)
            {
                vm.Vendedor = new Vendedor();
            }
            else
            {
                using (var dao = new VendedorDaoEntity())
                {
                    vm.Vendedor = dao.Pegar(id);
                }
            }

            return View(vm);

        }
    }
}