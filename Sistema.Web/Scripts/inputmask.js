$(document).ready(function () {
    $(".cpf").inputmask("mask", { "mask": "999.999.999-99" }, { reverse: true });
    $(".cep").inputmask("mask", { "mask": "99999-999" });
    $(".rg").inputmask("mask", { "mask": "99.999.999-9" });


}); 