using Microsoft.AspNetCore.Mvc;

namespace PrimeraAppNetCoreMVC.Controllers
{
    public class Medicamento : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public string saludo()
        {
            return "Hola amigos";
        }
        public int numeroEntero()
        {
            return 18;
        }
        public double numeroDecimal()
        {
            return 5.6;
        }
    }
}
