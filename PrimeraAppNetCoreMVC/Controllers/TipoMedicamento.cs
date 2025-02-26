using capaEntidad;
using capaNegocio;
using Microsoft.AspNetCore.Mvc;

namespace PrimeraAppNetCoreMVC.Controllers
{
    public class TipoMedicamento : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Inicio()
        {
            return View();
        }
        public IActionResult sinMenu()
        {
            return View();
        }
        public List<TipoMedicamentoCLS> listarTipoMedicamento()
        {
            TipoMedicamentoBL obj = new TipoMedicamentoBL();
            return obj.listarMedicamentos();
        }
        
        public List<TipoMedicamentoCLS> filtrarMed(string desc)
        {
            if (desc == null)
            {
                desc = "";
            }
            TipoMedicamentoBL obj = new TipoMedicamentoBL();
            return obj.filtrarMed(desc);
        }

        public int GuardarTipoMedicamento(TipoMedicamentoCLS objTipoMedicamentoCLS)
        {
            TipoMedicamentoBL obj = new TipoMedicamentoBL();
            return obj.GuardarTipoMedicamento(objTipoMedicamentoCLS);
        }

        public TipoMedicamentoCLS RecuperarTipoMedicamento(int idTipoMedicamento)
        {
            TipoMedicamentoBL obj = new TipoMedicamentoBL();
            return obj.RecuperarTipoMedicamento(idTipoMedicamento);
        }

        public int EliminarTipoMedicamento(int idTipoMedicamento)
        {
            TipoMedicamentoBL obj = new TipoMedicamentoBL();
            return obj.EliminarTipoMedicamento(idTipoMedicamento);
        }
    }
}
