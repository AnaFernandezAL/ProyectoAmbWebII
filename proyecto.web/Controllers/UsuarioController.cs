using Microsoft.AspNetCore.Mvc;
using proyecto.Application.DTOs;
using proyecto.Application.Services.Interfaces;
using proyecto.web.Util;
using X.PagedList;
using X.PagedList.Extensions;

namespace proyecto.Web.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IServiceUsuario _serviceUsuario;

        public UsuarioController(IServiceUsuario serviceUsuario)
        {
            _serviceUsuario = serviceUsuario;
        }

        // GET: UsuarioController
        [HttpGet]
        public async Task<ActionResult> Index(int? page)
        {
            var collection = await _serviceUsuario.ListAsync();

            int pageNumber = page ?? 1;
            int pageSize = 5;

            return View(collection.ToPagedList(pageNumber, pageSize));
        }

        // GET: UsuarioController/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    TempData["Notificacion"] = SweetAlertHelper.CrearNotificacion(
                       "Usuario no encontrado",
                       "No existe un Usuario sin ID",
                       SweetAlertMessageType.error
                   );
                    return RedirectToAction("Index");
                }

                var usuario = await _serviceUsuario.FindByIdAsync(id.Value);
                if (usuario == null)
                {
                    throw new Exception("Usuario no existente");
                }

                ViewBag.Notificacion = SweetAlertHelper.CrearNotificacion(
                   "Detalle del Usuario",
                   $"Mostrando información del Usuario: {usuario.NombreCompleto}",
                   SweetAlertMessageType.info
               );
                return View(usuario);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // GET: UsuarioController/Create
        public ActionResult Create()
        {
            return View(new UsuarioDTO());
        }

        // POST: UsuarioController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(UsuarioDTO dto)
        {
            if (!ModelState.IsValid)
            {
                var errores = string.Join("<br>",
                    ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)
                );

                ViewBag.Notificacion = SweetAlertHelper.CrearNotificacion(
                    "Errores de validación",
                    $"El formulario contiene errores:<br>{errores}",
                    SweetAlertMessageType.warning
                );
                return View(dto);
            }

            await _serviceUsuario.AddAsync(dto);

            TempData["Notificacion"] = SweetAlertHelper.CrearNotificacion(
               "Usuario creado correctamente",
               $"El usuario {dto.NombreCompleto} fue registrado exitosamente.",
               SweetAlertMessageType.success
           );
            return RedirectToAction(nameof(Index));
        }

        // GET: UsuarioController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var usuario = await _serviceUsuario.FindByIdAsync(id);
            if (usuario == null) return NotFound();

            return View(usuario);
        }

        // POST: UsuarioController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, UsuarioDTO dto)
        {
            if (!ModelState.IsValid)
            {
                var errores = string.Join("<br>",
                    ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)
                );

                ViewBag.Notificacion = SweetAlertHelper.CrearNotificacion(
                    "Errores de validación",
                    $"El formulario contiene errores:<br>{errores}",
                    SweetAlertMessageType.warning
                );
                return View(dto);
            }

            await _serviceUsuario.UpdatePerfilAsync(id, dto);

            TempData["Notificacion"] = SweetAlertHelper.CrearNotificacion(
               "Usuario actualizado",
               $"El usuario {dto.NombreCompleto} ha sido modificado exitosamente.",
               SweetAlertMessageType.success
           );
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeState(int id, int nuevoEstadoId)
        {
            await _serviceUsuario.CambiarEstadoAsync(id, nuevoEstadoId);

            string accion = nuevoEstadoId == 1 ? "activado" : "bloqueado";

            TempData["Notificacion"] = SweetAlertHelper.CrearNotificacion(
               $"Usuario {accion}",
               $"El usuario ha sido {accion} correctamente.",
               nuevoEstadoId == 1 ? SweetAlertMessageType.success : SweetAlertMessageType.warning
            );

            return RedirectToAction(nameof(Index));
        }

    }
}
