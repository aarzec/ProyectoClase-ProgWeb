using capaEntidad;
using capaNegocio;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace PrimeraAppNetCoreMVC.Controllers
{
    public class SucursalController : Controller
    {
        public IActionResult Index()
        {
            SucursalBL obj = new SucursalBL();
            List<SucursalCLS> lista = obj.listarSucursal();

            Console.WriteLine($"🚀 Se enviarán {lista.Count} sucursales a la vista.");

            return View(lista);
        }

        public List<SucursalCLS> filtrarSucursal(SucursalCLS objSucursal)
        {
            SucursalBL obj = new SucursalBL();
            return obj.filtrarSucursal(objSucursal);
        }

        public int GuardarSucursal(SucursalCLS objSucursalCLS)
        {
            SucursalBL obj = new SucursalBL();
            return obj.GuardarSucursal(objSucursalCLS);
        }
    }
}
