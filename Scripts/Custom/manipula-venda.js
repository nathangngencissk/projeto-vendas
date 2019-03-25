$('#addProduto').click(function () {
    var novoProduto = $('#formMolde').clone();
    novoProduto.attr('id', '');

    var valor = $('.formProduto').length.toString();

    novoProduto.find('[id*="categoria"]').attr('id', `categoria[${valor}]`);
    novoProduto.find('[id*="produto"]').attr('id', `produto[${valor}]`);
    novoProduto.find('[id*="qtd"]').attr('id', `qtd[${valor}]`);

    novoProduto.find('[id*="produto"]').attr('name', `formularioVenda.Produtos[${valor}].IdProduto`);
    novoProduto.find('[id*="qtd"]').attr('name', `formularioVenda.Produtos[${valor}].Quantidade`);

    var val = novoProduto.find('[id*="produto"]').children("option:selected").val();        

    var produtoId;
    var produtoNome;
    var produtoValor;
    var produtoCategoria;
    var produtoQtd;

    $.ajax({
        url: "/produtos/" + val + "/json",
        type: "post",
        async: true,
        data: { id: val },
        success: function (res) {
            produtoId = res.id;
            produtoNome = res.nome;
            produtoCategoria = res.categoria;
            produtoValor = res.valor;
            produtoQtd = res.qtd;

            novoProduto.find('[id*="estoque"]').html(`${produtoQtd} disponíveis`);        
        },
        error: function (resp) {
            console.log(resp);
        }
    });

    novoProduto.find('[id*="estoque"]').html(`${produtoQtd} disponíveis`);

    $('#produtos').append(novoProduto);

    $('.formProduto').find('[id*="produto"]').change(function () {
        var campo = $(this);
        $.ajax({
            url: "/produtos/" + $(this).val() + "/json",
            type: "post",
            async: true,
            data: { id: $(this).val() },
            success: function (res) {
                if (res.qtd <= 5) {
                    campo.parent().parent().find('[id*="estoque"]').html(`somente ${res.qtd} em estoque`);
                    campo.parent().parent().find('[id*="estoque"]').removeClass('invisible');
                }
                else
                {
                    campo.parent().parent().find('[id*="estoque"]').addClass('invisible');
                }
            },
            error: function (resp) {
                console.log(resp);
            }
        });
    })

    $('.formProduto').find('[id*="categoria"]').change(function () {
        var campo = $(this);
        $.ajax({
            url: "/produtos/" + $(this).val() + "/json",
            type: "post",
            async: true,
            data: { id: $(this).val() },
            success: function (res) {
                campo.parent().parent().find('[id*="produto"]').children().each(function () {
                    if ($(this).attr("data-categoria") != res.categoria) {
                        $(this).hide();
                    } else {
                        $(this).show();
                    }
                });
            },
            error: function (resp) {
                console.log(resp);
            }
        });
    })

    $(".delete").click(function () {
        if ($(this).parent().parent().attr("id") != "formMolde") {
            $(this).parent().fadeOut(800, function () {
                this.remove();
            });
        }
             
    });
});

$('.formProduto').find('[id*="produto"]').change(function () {
    var campo = $(this);
    $.ajax({
        url: "/produtos/" + $(this).val() + "/json",
        type: "post",
        async: true,
        data: { id: $(this).val() },
        success: function (res) {
            if (res.qtd <= 5) {
                campo.parent().parent().find('[id*="estoque"]').html(`somente ${res.qtd} em estoque`);
                campo.parent().parent().find('[id*="estoque"]').removeClass('invisible');
            }
            else {
                campo.parent().parent().find('[id*="estoque"]').addClass('invisible');
            }         
        },
        error: function (resp) {
            console.log(resp);
        }
    });
})

$('.formProduto').find('[id*="categoria"]').change(function () {
    var campo = $(this);
    $.ajax({
        url: "/produtos/" + $(this).val() + "/json",
        type: "post",
        async: true,
        data: { id: $(this).val() },
        success: function (res) {
            campo.parent().parent().find('[id*="produto"]').children().each(function () {
                if ($(this).attr("data-categoria") != res.categoria) {
                    $(this).hide();
                } else {
                    $(this).show();
                }
            });
        },
        error: function (resp) {
            console.log(resp);
        }
    });
})