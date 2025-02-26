async function fetchGet(url, tipoRespuesta, callback) {
    try {
        let raiz = document.getElementById("hdfOculto").value;

        //http://localhost
        let urlCompleta = window.location.protocol + "//" + window.location.host + "/" + raiz + url;

        let res = await fetch(urlCompleta)

        if (tipoRespuesta == "json") {
            res = await res.json();
        }
        else if (tipoRespuesta == "text") {
            res = await res.text();
        }

        //JSON (object)
        console.log("Datos recibidos del backend:", res);

        callback(res)

    } catch (e) {
        console.error(e)
        alert("Ocurrió un problema");
    }
}


async function fetchPost(url, tipoRespuesta, frm, callback) {
    try {
        let raiz = document.getElementById("hdfOculto").value;

        //http://localhost
        let urlCompleta = window.location.protocol + "//" + window.location.host + "/" + raiz + url;

        let res = await fetch(urlCompleta, {
            method: "POST",
            body: frm,
        })

        if (tipoRespuesta == "json") {
            res = await res.json();
        }
        else if (tipoRespuesta == "text") {
            res = await res.text();
        }

        //JSON (object)
        console.log("Datos recibidos del backend:", res);

        callback(res)

    } catch (e) {
        console.error(e)
        alert("Ocurrió un problema");
    }
}

async function fetchPut(url, tipoRespuesta, frm, callback) {
    try {
        let raiz = document.getElementById("hdfOculto").value;
        //http://localhost
        let urlCompleta = window.location.protocol + "//" + window.location.host + "/" + raiz + url;
        let res = await fetch(urlCompleta, {
            method: "PUT",
            body: frm,
        })
        if (tipoRespuesta == "json") {
            res = await res.json();
        }
        else if (tipoRespuesta == "text") {
            res = await res.text();
        }
        //JSON (object)
        console.log("Datos recibidos del backend:", res);
        callback(res);
    } catch (e) {
        console.error(e)
        alert("Ocurrió un problema");
    }
}

async function fetchDelete(url, tipoRespuesta, callback) {
    try {
        let raiz = document.getElementById("hdfOculto").value;
        //http://localhost
        let urlCompleta = window.location.protocol + "//" + window.location.host + "/" + raiz + url;
        let res = await fetch(urlCompleta, {
            method: "DELETE",
        })
        if (tipoRespuesta == "json") {
            res = await res.json();
        }
        else if (tipoRespuesta == "text") {
            res = await res.text();
        }
        //JSON (object)
        console.log("Datos recibidos del backend:", res);
        callback(res);
    } catch (e) {
        console.error(e)
        alert("Ocurrió un problema");
    }
}

let objConfiguracionGlobal;

//(url:"", nombreColumnas[], nombrePropiedades: [])
function pintar(objConfiguracion, idTabla = "divtabla") {
    objConfiguracionGlobal = objConfiguracion;

    fetchGet(objConfiguracion.url, "json", function (res) {
        let contenido = "";

        contenido += "<div id='divContenedorTabla'>"

        contenido += generarTabla(res);

        contenido += "</div>";

        document.getElementById(idTabla).innerHTML = contenido;
    })
}

function limpiarFormulario(id) {
    let form = document.getElementById(id);
    form.reset();
}

function pintarPost(objConfiguracion, idTabla = "divtabla") {
    objConfiguracionGlobal = objConfiguracion;
    let form = get("formBusqueda");
    let formData = new FormData(form);
    fetchPost(
        objConfiguracion.url,
        "json",
        formData,
        (data) => {
            let contenido = "";
            contenido += "<div id='divContenedorTabla'>"
            contenido += generarTabla(data);
            contenido += "</div>";
            document.getElementById(idTabla).innerHTML = contenido;
        }
    );
}

function get(id) {
    return document.getElementById(id);
}

function setValue(id, value) {
    get(id).value = value;
}

function generarTabla(res) {

    let contenido = " ";



    //cabeceras: ["id Tipo Medicamento", "Nombre", "Descripcion"],
    let cabeceras = objConfiguracionGlobal.cabeceras;

    //propiedades: ["idMedicamento", "nombre", "descripcion"]
    let nombrePropiedades = objConfiguracionGlobal.propiedades;

    contenido = '<table class="table">';
    contenido += "<thead>"

    /* Primera fila de la tabla con los headers */

    contenido += "<tr>"

    for (var i = 0; i < cabeceras.length; i++) {
        contenido += "<td>" + cabeceras[i] + "</td>";
    }

    if (objConfiguracionGlobal.editar || objConfiguracionGlobal.eliminar) {
        contenido += "<td>Operaciones</td>";
    }

    contenido += "</tr>"

    contenido += "</thead>"

    // Cuerpo

    contenido += "<tbody>"

    let nroRegistros = res.length;
    let obj;
    let propiedadActual;

    for (let i = 0; i < nroRegistros; i++) {
        obj = res[i];
        contenido += "<tr>";

        for (var j = 0; j < nombrePropiedades.length; j++) {
            propiedadActual = nombrePropiedades[j];
            contenido += "<td>" + obj[propiedadActual] + "</td>";
        }

        if (objConfiguracionGlobal.editar || objConfiguracionGlobal.eliminar) {
            contenido += "<td>";

            const objId = obj[objConfiguracionGlobal.propiedadId];

            if (objConfiguracionGlobal.editar) {
                contenido += `
                <button class="btn btn-primary" onclick="Editar(${objId})">
                    <i class="icon">
                        <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-pencil-square" viewBox="0 0 16 16">
                      <path d="M15.502 1.94a.5.5 0 0 1 0 .706L14.459 3.69l-2-2L13.502.646a.5.5 0 0 1 .707 0l1.293 1.293zm-1.75 2.456-2-2L4.939 9.21a.5.5 0 0 0-.121.196l-.805 2.414a.25.25 0 0 0 .316.316l2.414-.805a.5.5 0 0 0 .196-.12l6.813-6.814z"/>
                      <path fill-rule="evenodd" d="M1 13.5A1.5 1.5 0 0 0 2.5 15h11a1.5 1.5 0 0 0 1.5-1.5v-6a.5.5 0 0 0-1 0v6a.5.5 0 0 1-.5.5h-11a.5.5 0 0 1-.5-.5v-11a.5.5 0 0 1 .5-.5H9a.5.5 0 0 0 0-1H2.5A1.5 1.5 0 0 0 1 2.5z"/>
                        </svg>
                    </i>
                </button>`;
            }

            if (objConfiguracionGlobal.eliminar) {
                contenido += `
                <button class="btn btn-danger" onclick="Eliminar(${objId})">
                    <i class="icon">
                        <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-trash-fill" viewBox="0 0 16 16">
                        <path d="M2.5 1a1 1 0 0 0-1 1v1a1 1 0 0 0 1 1H3v9a2 2 0 0 0 2 2h6a2 2 0 0 0 2-2V4h.5a1 1 0 0 0 1-1V2a1 1 0 0 0-1-1H10a1 1 0 0 0-1-1H7a1 1 0 0 0-1 1zm3 4a.5.5 0 0 1 .5.5v7a.5.5 0 0 1-1 0v-7a.5.5 0 0 1 .5-.5M8 5a.5.5 0 0 1 .5.5v7a.5.5 0 0 1-1 0v-7A.5.5 0 0 1 8 5m3 .5v7a.5.5 0 0 1-1 0v-7a.5.5 0 0 1 1 0"/>
                        </svg>
                    </i>
                </button>`;
            }

            contenido += "</td>";
        }

        /*
        contenido += "<td>" + obj.idMedicamento + "</td>";
        contenido += "<td>" + obj.nombre + "</td>";
        contenido += "<td>" + obj.descripcion + "</td>";
        */
        contenido += "</tr>";
    }


    contenido += "</tbody>"
    contenido += "</table>"

    return contenido;
}

function recuperarGenerico(url, idFormulario) {
    const form = document.getElementById(idFormulario);
    const elementosName = form.querySelectorAll("[name]");

    fetchGet(url, "json", (res) => {
        form.scrollIntoView();

        elementosName.forEach((elemento) => {
            setValue(elemento.id, res[elemento.name]);
        });
    });
}

async function Confirmacion() {
    const swalRes = await Swal.fire({
        title: "Confirmación",
        text: "¿Está seguro que desea relizar la acción?",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Continuar",
        cancelButtonText: "Cancelar",
    });
    return swalRes.isConfirmed;
}


function ExitoToast(msg = "Operación realizada con éxito") {
    toastr.options = {
        "closeButton": false,
        "debug": false,
        "newestOnTop": false,
        "progressBar": false,
        "positionClass": "toast-top-right",
        "preventDuplicates": false,
        "onclick": null,
        "showDuration": "300",
        "hideDuration": "1000",
        "timeOut": "5000",
        "extendedTimeOut": "1000",
        "showEasing": "swing",
        "hideEasing": "linear",
        "showMethod": "fadeIn",
        "hideMethod": "fadeOut"
    }
    toastr["success"](msg);
}

function ErrorToast(msg = "Ha ocurrido un error") {
    toastr.options = {
        "closeButton": false,
        "debug": false,
        "newestOnTop": false,
        "progressBar": false,
        "positionClass": "toast-top-right",
        "preventDuplicates": false,
        "onclick": null,
        "showDuration": "300",
        "hideDuration": "1000",
        "timeOut": "5000",
        "extendedTimeOut": "1000",
        "showEasing": "swing",
        "hideEasing": "linear",
        "showMethod": "fadeIn",
        "hideMethod": "fadeOut"
    }
    toastr["error"](msg);
}