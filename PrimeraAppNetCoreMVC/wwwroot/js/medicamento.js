window.onload = function () {
    listarTipoMedicamento();
}


async function listarTipoMedicamento() {

    fetchGet("Medicamento/saludo", "text", function (res) {
        alert(res);
    })



    fetchGet("Medicamento/numeroEntero", "text", function (res) {
        alert(res);
    })
}
