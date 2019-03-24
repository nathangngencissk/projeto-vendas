$('#addProduto').click(function () {
    var novoProduto = $('#formMolde').clone();
    novoProduto.attr('id', '');

    var valor = $('.formProduto').length.toString();

    novoProduto.find('[id*="categoria"]').attr('id', `categoria[${valor}]`);
    novoProduto.find('[id*="produto"]').attr('id', `produto[${valor}]`);
    novoProduto.find('[id*="qtd"]').attr('id', `qtd[${valor}]`);

    novoProduto.find('[id*="produto"]').attr('name', `formularioVenda.Produtos[${valor}].Produto.IdProduto`);
    novoProduto.find('[id*="qtd"]').attr('name', `formularioVenda.Produtos[${valor}].Quantidade`);

    var val = novoProduto.find('[id*="produto"]').children("option:selected").val();

    $.ajax({
        url: "/produtos/" + val + "/json",
        type: "post",
        data: { id: val },
        success: function (res) {
            novoProduto.find('[id*="estoque"]').html(`${res.qtd} disponíveis`);
        },
        error: function (resp) {
            console.log(resp);
        }
    });

    

    $('#produtos').append(novoProduto);

    $(".delete").click(function () {
        $(this).parent().fadeOut(800, function () {
            
                console.log(this);
                this.remove();
                    
        });
    });
});