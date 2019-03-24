function validateForm() {
    var Valor = document.querySelector("#valor").value;
    var re = new RegExp("^[0-9]{1,},{0,1}[0-9]{0,2}$");
    if (!re.test(Valor)) {
        document.querySelector("#mensagemVlr").innerHTML = "Insira um formato de valor correto (x,xx)";
        document.querySelector("#mensagemVlr").classList.remove("invisible");
        return false;
    } else {
        document.querySelector("#mensagemVlr").classList.add("invisible");
        return true;
    }
} 