async function filtrarSucursal(nombre) {

    pintar({
        url: "Sucursal/filtrarSucursal/?nombre=" + nombre,
        cabeceras: ["Id", "Nombre", "Dirección"],
        propiedades: ["idSucursal", "nombre", "direccion"],
        editar: true,
        eliminar: true,
    })
};

function limpiarSucursal() {
    limpiarFormulario("formGuardarSucursal");
}

async function buscarSucursal() {
    let form = get("formBusqueda");
    let formData = new FormData(form);

    pintarPost({
        url: "Sucursal/filtrarSucursal",
        cabeceras: ["Id", "Nombre", "Dirección"],
        propiedades: ["idSucursal", "nombre", "direccion"],
        editar: true,
        eliminar: true,
    });

    //const input = document.getElementById("txtNombreBusqueda");
    //await filtrarSucursal(input.value);
}

function listarSucursal() {
    filtrarSucursal("");
}

window.onload = function () {
    listarSucursal();
}

function guardarSucursal() {
    const frmGuardar = document.getElementById("formGuardarSucursal");

    fetchPost("Sucursal/GuardarSucursal", "text", new FormData(frmGuardar), (res) => {
        const resInt = parseInt(res);

        if (resInt == 1) {  
            listarSucursal();
            limpiarSucursal();
        }
    });
}


