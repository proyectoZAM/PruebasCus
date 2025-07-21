using Microsoft.AspNetCore.Mvc;
using AppCliente.Models;
using Microsoft.Extensions.Configuration;

namespace ClientesApp.Controllers
{
    public class ClientesController : Controller
    {
        private readonly ClienteDataAccess _dataAccess;

        public ClientesController(IConfiguration config)
        {
            _dataAccess = new ClienteDataAccess(config);
        }

        public IActionResult Index()
        {
            var lista = _dataAccess.CargaLista();
            return View(lista);
        }

        [HttpGet]
        public IActionResult Agrega()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Agrega(Cliente cliente)
        {
            string resultado = _dataAccess.Agrega(cliente);
            ViewBag.Mensaje = resultado;
            return RedirectToAction("Index");
        }

        public IActionResult Elimina(int id)
        {
            ViewBag.Mensaje = _dataAccess.Elimina(id);
            return RedirectToAction("Index");
        }
    }
}