using CapaNegocios;
using Microsoft.AspNetCore.Mvc;

namespace App.Controllers
{
    public class EtiquetasController : Controller
    {
        private readonly EtiquetaServicio _servicio;

        public EtiquetasController(EtiquetaServicio servicio)
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

        public IActionResult Create() => View(new EtiquetaModelo());

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(EtiquetaModelo modelo)
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
        public IActionResult Edit(int id, EtiquetaModelo modelo)
        {
            if (id != modelo.IdEtiqueta)
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
