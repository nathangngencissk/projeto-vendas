using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VendasMVC.Models.Util
{
    public abstract class RouteNames
    {
        //Rotas para Cliente
        public const string ListarClientes = nameof(ListarClientes);
        public const string AdicionarCliente = nameof(AdicionarCliente);
        public const string VisualizarCliente = nameof(VisualizarCliente);
        public const string AlterarCliente = nameof(AlterarCliente);
        public const string FormCliente = nameof(FormCliente);

        //Rotas para Vendedor
        public const string ListarVendedores = nameof(ListarVendedores);
        public const string AdicionarVendedor = nameof(AdicionarVendedor);
        public const string VisualizarVendedor = nameof(VisualizarVendedor);
        public const string AlterarVendedor = nameof(AlterarVendedor);
        public const string FormVendedor = nameof(FormVendedor);

        //Rotas para Produto
        public const string ListarProdutos = nameof(ListarProdutos);
        public const string AdicionarProduto = nameof(AdicionarProduto);
        public const string VisualizarProduto = nameof(VisualizarProduto);
        public const string AlterarProdutos = nameof(AlterarProdutos);
        public const string FormProduto = nameof(FormProduto);

        //Rotas para Venda
        public const string ListarVendas = nameof(ListarVendas);
        public const string AdicionarVenda = nameof(AdicionarVenda);
        public const string VisualizarVenda = nameof(VisualizarVenda);
        public const string AlterarVenda = nameof(AlterarVenda);
        public const string FormVenda = nameof(FormVenda);
    }
}