using capaEntidad;
using capaNegocio;
using Microsoft.AspNetCore.Mvc;

namespace PrimeraAppNetCoreMVC.Controllers
{
    public class DocumentoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult PruebaUso()
        {
            return View();
        }

        public List<DocumentoCLS> listarDocumentos()
        {
            DocumentoBL obj = new DocumentoBL();
            return obj.listarDocumentos();
        }
    }
}