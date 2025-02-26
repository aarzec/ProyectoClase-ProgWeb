window.onload = function () {
    listarTipoMedicamento();
}

const modalTipoMedicamento = new bootstrap.Modal(get("modalTipoMedicamento"), {})
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

async function guardarTipoMedicamento() {
    const frmGuardar = new FormData(document.getElementById("formGuardarTipoMedicamento"));
    const callback = (res) => {
        const resInt = parseInt(res);
        if (resInt == 1) {
            listarTipoMedicamento();
            limpiarTipoMedicamento();
            ExitoToast("Registro guardado con éxito");
        } else {
            ErrorToast();
        }
    }

    if (idTipoMedicamentoInput.value != "") {
        if (!await Confirmacion()) return;
        modalTipoMedicamento.hide();
        fetchPut("TipoMedicamento/GuardarTipoMedicamento", "text", frmGuardar, callback);
    } else {
        fetchPost("TipoMedicamento/GuardarTipoMedicamento", "text", frmGuardar, callback);
    }
}

function limpiarTipoMedicamento() {
    limpiarFormulario("formGuardarTipoMedicamento");
    guardarBtn.innerText = "Guardar";
}

function nuevoTipoMedicamento() {
    limpiarTipoMedicamento();
    modalTipoMedicamento.show();
}

async function Editar(id) {
    guardarBtn.innerText = "Actualizar";
    recuperarGenerico("TipoMedicamento/RecuperarTipoMedicamento/?idTipoMedicamento=" + id, "formGuardarTipoMedicamento");
    modalTipoMedicamento.show();
}

async function Eliminar(id) {
    if (!await Confirmacion()) return;
    modalTipoMedicamento.hide();

    fetchDelete("TipoMedicamento/EliminarTipoMedicamento/?idTipoMedicamento=" + id, "text", (res) => {
        if (parseInt(res) == 1) {
            listarTipoMedicamento();
            ExitoToast("Registro eliminado con éxito");
        } else {
            ErrorToast();
        }
    });
}