async function filtrarLaboratorio(nombre) {

    pintar({
        url: "Laboratorio/filtrarLaboratorio/?nombre=" + nombre,
        cabeceras: ["Id", "Nombre", "Dirección", "Persona contacto"],
        propiedades: ["idLaboratorio", "nombre", "direccion", "personaContacto"]
    })
};

function limpiarLaboratorio() {
    limpiarFormulario("formBusqueda");
}

async function buscarLaboratorio() {
    let form = get("formBusqueda");
    let formData = new FormData(form);

    pintarPost({
        url: "Laboratorio/filtrarLaboratorio",
        cabeceras: ["Id", "Nombre", "Dirección", "Persona contacto"],
        propiedades: ["idLaboratorio", "nombre", "direccion", "personaContacto"]
    });

    //const input = document.getElementById("txtNombreBusqueda");
    //await filtrarLaboratorio(input.value);
}

