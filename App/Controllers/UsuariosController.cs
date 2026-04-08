using CapaNegocios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace App.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly UsuarioServicio _servicio;
        private readonly RolServicio _rolServicio;

        public UsuariosController(UsuarioServicio servicio, RolServicio rolServicio)
        {
            _servicio = servicio;
            _rolServicio = rolServicio;
        }

        private void CargarRolesViewBag()
        {
            ViewBag.Roles = new SelectList(_rolServicio.Listar().Where(r => r.Activo), "IdRol", "Nombre");
        }

        public IActionResult Index() => View(_servicio.Listar());

        public IActionResult Details(int? id)
        {
            if (id is null) return NotFound();
            var modelo = _servicio.ObtenerPorId(id.Value);
            return modelo is null ? NotFound() : View(modelo);
        }

        public IActionResult Create()
        {
            CargarRolesViewBag();
            return View(new UsuarioModelo());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(UsuarioModelo modelo)
        {
            if (!ModelState.IsValid)
            {
                CargarRolesViewBag();
                return View(modelo);
            }
            _servicio.Crear(modelo);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int? id)
        {
            if (id is null) return NotFound();
            var modelo = _servicio.ObtenerPorId(id.Value);
            if (modelo is null) return NotFound();
            CargarRolesViewBag();
            return View(modelo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, UsuarioModelo modelo)
        {
            if (id != modelo.IdUsuario) return NotFound();
            if (!ModelState.IsValid)
            {
                CargarRolesViewBag();
                return View(modelo);
            }
            _servicio.Actualizar(modelo);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int? id)
        {
            if (id is null) return NotFound();
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
