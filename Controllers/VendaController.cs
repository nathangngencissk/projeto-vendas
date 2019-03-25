using System;
using System.Collections.Generic;
using System.Globalization;
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

        [Route("vendas/anual/json", Name = "VendasAnuais")]
        public JsonResult VendasAnuais()
        {
            int id = Convert.ToInt32(System.Web.HttpContext.Current.Session["IdVendedor"].ToString());

            Vendedor vendedor;
            List<Venda> vendas = new List<Venda>();
            List<ProdutoVenda> produtos = new List<ProdutoVenda>();
            List<ProdutoVenda> produtosDoMes = new List<ProdutoVenda>();
            List<List<Venda>> vendasAnuais = new List<List<Venda>>();
            Dictionary<string, decimal> vendasAnual = new Dictionary<string, decimal>();

            List<int> ids = new List<int>();

            using (var dao = new VendedorDaoEntity())
            {
                vendedor = dao.Pegar(id);
            }
            using (var dao = new VendaDaoEntity())
            {
                List<Venda> lista = dao.PegarLista() as List<Venda>;
                vendas.AddRange(from v in lista where v.IdVendedor == vendedor.IdVendedor select v);
            }

            foreach (Venda venda in vendas)
            {
                ids.Add(venda.IdVenda);
            }
            using (var dao = new ProdutoVendaDaoEntity())
            {
                List<ProdutoVenda> lista = dao.PegarLista() as List<ProdutoVenda>;
                produtos.AddRange(from p in lista where ids.Contains(p.IdVenda) select p);
            }

            ids.Clear();

            var dataAtual = DateTime.Now;

            for (int i = 0; i < 12; i++)
            {
                vendasAnuais.Add(new List<Venda>());
                vendasAnuais[i].AddRange(from v in vendas where v.DataDaVenda.Month.Equals(dataAtual.AddMonths(i-11).Month) && v.DataDaVenda.Year.Equals(dataAtual.AddMonths(i - 11).Year) select v);
                vendasAnual.Add(CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(dataAtual.AddMonths(i-11).Month), 0);

                foreach (Venda venda in vendasAnuais[i])
                {
                    ids.Add(venda.IdVenda);
                }

                produtosDoMes.AddRange(from p in produtos where ids.Contains(p.IdVenda) select p);
                foreach (ProdutoVenda pv in produtosDoMes)
                {
                    vendasAnual[CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(dataAtual.AddMonths(i - 11).Month)] += pv.Valor;
                }
                ids.Clear();
                produtosDoMes.Clear();
            }

            Dictionary<string, decimal> val = new Dictionary<string, decimal>();
            var valores = from ele in vendasAnual                
                          select ele;

            foreach (var item in valores)
            {
                val.Add(item.Key, item.Value);
            }

            Dictionary<string, decimal>.KeyCollection keys = val.Keys;



            return Json(new { mes1 = val[keys.ElementAt(0)], nMes1 = keys.ElementAt(0),
                              mes2 = val[keys.ElementAt(1)], nMes2 = keys.ElementAt(1),
                              mes3 = val[keys.ElementAt(2)], nMes3 = keys.ElementAt(2),
                              mes4 = val[keys.ElementAt(3)], nMes4 = keys.ElementAt(3),
                              mes5 = val[keys.ElementAt(4)], nMes5 = keys.ElementAt(4),
                              mes6 = val[keys.ElementAt(5)], nMes6 = keys.ElementAt(5),
                              mes7 = val[keys.ElementAt(6)], nMes7 = keys.ElementAt(6),
                              mes8 = val[keys.ElementAt(7)], nMes8 = keys.ElementAt(7),
                              mes9 = val[keys.ElementAt(8)], nMes9 = keys.ElementAt(8),
                              mes10 = val[keys.ElementAt(9)], nMes10 = keys.ElementAt(9),
                              mes11 = val[keys.ElementAt(10)], nMes11 = keys.ElementAt(10),
                              mes12 = val[keys.ElementAt(11)], nMes12 = keys.ElementAt(11), });
        }

        [Route("vendas/form", Name = RouteNames.FormVenda)]
        public ActionResult Form()
        {
            return View(new VendaFormViewModel());
        }
    }
}