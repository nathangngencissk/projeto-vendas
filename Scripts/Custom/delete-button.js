$(document).ready(function () {
    $(".delete").click(function () {
        var getId = $(this).attr("name");
        var pathname = window.location.pathname;
        $.ajax({
            url: pathname + "/remover/" + getId,
            type: "post",
            data: { id: getId },
            success: function () {
                $('table #' + getId).fadeOut(800, function () {
                     this.remove();               
                });
            },
            error: function (resp) {
                console.log(resp);
                console.log(pathname);
            }
        });
    });
});