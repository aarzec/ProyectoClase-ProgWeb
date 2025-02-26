window.onload = function () {
    listarTipoMedicamento();
}

const idTipoMedicamentoInput = document.getElementById("idTipoMedicamento");
const guardarBtn = document.getElementById("buttonGuardar");

let objTipoMedicamento = {
    url: "",
    cabeceras: ["Id Medicamento", "Nombre", "Descripcion"],
    propiedades: ["idTipoMedicamento", "nombre", "descripcion"],
    eliminar: false,
    editar: false,
    propiedadId: "idTipoMedicamento",
};

async function listarTipoMedicamento() {
    objTipoMedicamento.url = "TipoMedicamento/listarTipoMedicamento";
    objTipoMedicamento.editar = true;
    objTipoMedicamento.eliminar = true;
    pintar(objTipoMedicamento);
}

async function filtrarMedicamento(desc) {

    objTipoMedicamento.url = "TipoMedicamento/filtrarMed/?desc=" + desc;
    pintar(objTipoMedicamento, "divtablaBusqueda")
};

async function buscarMed() {
    const input = get("txtNombreBusqueda");
    await filtrarMedicamento(input.value);
}

async function limpiarControl() {
    setValue("txtNombreBusqueda", "");
    await filtrarMedicamento("");
}

function guardarTipoMedicamento() {
    const frmGuardar = new FormData(document.getElementById("formGuardarTipoMedicamento"));
    const callback = (res) => {
        const resInt = parseInt(res);
        if (resInt == 1) {
            listarTipoMedicamento();
            limpiarTipoMedicamento();
        }
    }

    if (idTipoMedicamentoInput.value != "") {
        fetchPut("TipoMedicamento/GuardarTipoMedicamento", "text", frmGuardar, callback);
    } else {
        fetchPost("TipoMedicamento/GuardarTipoMedicamento", "text", frmGuardar, callback);
    }
}

function limpiarTipoMedicamento() {
    limpiarFormulario("formGuardarTipoMedicamento");
    guardarBtn.innerText = "Guardar";
}

function Editar(id) {
    guardarBtn.innerText = "Actualizar";
    recuperarGenerico("TipoMedicamento/RecuperarTipoMedicamento/?idTipoMedicamento=" + id, "formGuardarTipoMedicamento");
}

function Eliminar(id) {
    const deleteAns = confirm("¿De verdad quiere eliminar este registro?");
    if (!deleteAns) return;

    fetchDelete("TipoMedicamento/EliminarTipoMedicamento/?idTipoMedicamento=" + id, "text", (res) => {
        if (parseInt(res) == 1) {
            listarTipoMedicamento();
        }
    });
}