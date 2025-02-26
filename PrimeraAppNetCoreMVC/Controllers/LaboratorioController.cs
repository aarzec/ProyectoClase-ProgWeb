using capaEntidad;
using capaNegocio;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace PrimeraAppNetCoreMVC.Controllers
{
    public class LaboratorioController : Controller
    {

        public List<LaboratorioCLS> ListarLaboratorio()
        {
            LaboratorioBL obj = new LaboratorioBL();
            return obj.listarLaboratorio();
        }
        public IActionResult Index()
        {
            List<LaboratorioCLS> lista = ListarLaboratorio();

            Console.WriteLine($"🚀 Se enviarán {lista.Count} Laboratorioes a la vista.");

            return View(lista);
        }

        public List<LaboratorioCLS> filtrarLaboratorio(LaboratorioCLS objLaboratorio)
        {
            //if (nombre == null)
            //{
            //    nombre = "";
            //}
            Console.WriteLine($"🔍 Se buscará Laboratorio con nombre: {objLaboratorio.Nombre}");
            LaboratorioBL obj = new LaboratorioBL();
            return obj.filtrarLaboratorio(objLaboratorio);
        }
    }
}
