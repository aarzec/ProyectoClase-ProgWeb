window.onload = function () {
    listarDocumentos();
}

async function listarDocumentos() {

    pintar({
        url: "Documento/listarDocumentos",
        cabeceras: ["Id Documento", "Nombre", "Bhabilitado"],
        propiedades: ["idTipoDocumento", "nombre", "bhabilitado"]
    })
}