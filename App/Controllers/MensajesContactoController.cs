using CapaNegocios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace App.Controllers;

[Authorize]
public class MensajesContactoController : Controller
{
    private readonly MensajeContactoServicio _servicio;

    public MensajesContactoController(MensajeContactoServicio servicio)
    {
        _servicio = servicio;
    }

    [AllowAnonymous]
    [HttpGet]
    public IActionResult Create() => View(new MensajeContactoModelo());

    [AllowAnonymous]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(MensajeContactoModelo modelo)
    {
        if (!ModelState.IsValid)
            return View(modelo);

        _servicio.Crear(modelo);
        TempData["MensajeContactoOk"] = "Tu mensaje fue enviado correctamente.";
        return RedirectToAction(nameof(Create));
    }

    public IActionResult Index() => View(_servicio.Listar());

    public IActionResult Details(int? id)
    {
        if (id is null)
            return NotFound();
        var modelo = _servicio.ObtenerPorId(id.Value);
        return modelo is null ? NotFound() : View(modelo);
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
    public IActionResult Edit(int id, MensajeContactoModelo modelo)
    {
        if (id != modelo.IdMensaje)
            return NotFound();
        if (!ModelState.IsValid)
            return View(modelo);

        var usuarioIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (int.TryParse(usuarioIdClaim, out var idUsuario))
            modelo.IdUsuario = idUsuario;

        if (!_servicio.Actualizar(modelo))
            return NotFound();

        return RedirectToAction(nameof(Index));
    }
}
