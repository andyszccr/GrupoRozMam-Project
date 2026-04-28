using CapaNegocios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace App.Controllers
{
    [Authorize]
    public class CategoriasController : Controller
    {
        private readonly CategoriaServicio _servicio;

        public CategoriasController(CategoriaServicio servicio)
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

        public IActionResult Create() => View(new CategoriaModelo());

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CategoriaModelo modelo)
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
        public IActionResult Edit(int id, CategoriaModelo modelo)
        {
            if (id != modelo.IdCategoria)
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
