using CapaNegocios;
using Microsoft.AspNetCore.Mvc;

namespace App.Controllers
{
    public class RolesController : Controller
    {
        private readonly RolServicio _servicio;

        public RolesController(RolServicio servicio)
        {
            _servicio = servicio;
        }

        public IActionResult Index() => View(_servicio.Listar());

        public IActionResult Details(int? id)
        {
            if (id is null)
                return NotFound();
            var modelo = _servicio.ObtenerPorId(id.Value);
            return modelo is null ? NotFound() : View(modelo);
        }

        public IActionResult Create() => View(new RolModelo());

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(RolModelo modelo)
        {
            if (!ModelState.IsValid)
                return View(modelo);
            _servicio.Crear(modelo);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int? id)
        {
            if (id is null)
                return NotFound();
            var modelo = _servicio.ObtenerPorId(id.Value);
            return modelo is null ? NotFound() : View(modelo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, RolModelo modelo)
        {
            if (id != modelo.IdRol)
                return NotFound();
            if (!ModelState.IsValid)
                return View(modelo);
            if (!_servicio.Actualizar(modelo))
                return NotFound();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int? id)
        {
            if (id is null)
                return NotFound();
            var modelo = _servicio.ObtenerPorId(id.Value);
            return modelo is null ? NotFound() : View(modelo);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _servicio.Desactivar(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
