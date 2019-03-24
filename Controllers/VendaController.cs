using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VendasMVC.Models;
using VendasMVC.Models.Util;
using VendasMVC.Persistence;
using VendasMVC.ViewModel;

namespace VendasMVC.Controllers
{
    [SessionTimeout]
    public class VendaController : Controller
    {
        [Route("vendas", Name = RouteNames.ListarVendas)]
        public ActionResult ListarVendas()
        {
            List<Venda> vendas;
            List<ProdutoVenda> produtos;
            using (var dao = new VendaDaoEntity())
            {
                vendas = dao.PegarLista() as List<Venda>;
            }
            using (var dao = new ProdutoVendaDaoEntity())
            {
                produtos = dao.PegarLista() as List<ProdutoVenda>;
            }

            ListaVendasViewModel vm = new ListaVendasViewModel
            {
                Vendas = vendas,
                Produtos = produtos       
            };

            return View(vm);
        }

        [Route("vendas/adicionar", Name = RouteNames.AdicionarVenda)]
        public ActionResult AdicionarVenda(FormularioVenda formularioVenda)
        {      
            Cliente cliente;
            using (var dao = new ClienteDaoEntity())
            {
                cliente = dao.Pegar(formularioVenda.Cpf);
            }
            Vendedor vendedor;
            using (var dao = new VendedorDaoEntity())
            {
                vendedor = dao.Pegar(Convert.ToInt32(System.Web.HttpContext.Current.Session["IdVendedor"].ToString()));
            }

            formularioVenda.Venda.IdCliente = cliente.IdCliente;
            formularioVenda.Venda.IdVendedor = vendedor.IdVendedor;
            formularioVenda.Venda.DataDaVenda = DateTime.Now;

            if (PasswordEncrypt.CompareHash(formularioVenda.Senha, vendedor.Senha, vendedor.SaltSenha))
            {
                int idDaVenda;

                using (var dao = new VendaDaoEntity())
                {
                    dao.Adicionar(formularioVenda.Venda);
                    List<Venda>lista = dao.PegarLista() as List<Venda>;
                    idDaVenda = lista.Last().IdVenda;
                }

                Produto p;
                using (var daoProduto = new ProdutoDaoEntity())
                {
                    using (var dao = new ProdutoVendaDaoEntity())
                    {
                        foreach (var produto in formularioVenda.Produtos)
                        {
                            p = daoProduto.Pegar(produto.IdProduto);
                            p.QuantidadeEmEstoque--;
                            daoProduto.Alterar(p);
                            produto.IdVenda = idDaVenda;
                            produto.Valor = produto.Quantidade * p.ValorUnitario;
                            dao.Adicionar(produto);
                        }
                    }
                }


                
                return RedirectToAction("ListarVendas");
            }
            return RedirectToAction("Form");

            
        }

        [Route("vendas/{id}", Name = RouteNames.VisualizarVenda)]
        public ActionResult VisualizarVenda(int id)
        {
            Venda venda = new Venda();
            using (var dao = new VendaDaoEntity())
            {
                venda = dao.Pegar(id);
            }
            return View(venda);
        }

        [Route("vendas/form", Name = RouteNames.FormVenda)]
        public ActionResult Form()
        {
            return View(new VendaFormViewModel());
        }
    }
}