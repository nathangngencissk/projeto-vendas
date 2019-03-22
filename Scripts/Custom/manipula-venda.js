$('#addProduto').click(function () {
    var novoProduto = $('#formMolde').clone();
    novoProduto.attr('id', '');

    var valor = $('.formProduto').length.toString();

    novoProduto.find('[id*="categoria"]').attr('id', `categoria[${valor}]`);
    novoProduto.find('[id*="produto"]').attr('id', `produto[${valor}]`);
    novoProduto.find('[id*="qtd"]').attr('id', `qtd[${valor}]`);

    novoProduto.find('[id*="produto"]').attr('name', `formularioVenda.Produtos[${valor}].Produto.IdProduto`);
    novoProduto.find('[id*="qtd"]').attr('name', `formularioVenda.Produtos[${valor}].Quantidade`);  

    $('#produtos').append(novoProduto);

    $(".delete").click(function () {
        $(this).parent().fadeOut(800, function () {
            this.remove();
        });
    });
});