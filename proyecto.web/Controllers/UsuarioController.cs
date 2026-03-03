using Microsoft.AspNetCore.Mvc;
using proyecto.Application.DTOs;
using proyecto.Application.Services.Interfaces;
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
        public async Task<IActionResult> Index(int? page)
        {
            int pageNumber = page ?? 1;   // Página actual, por defecto 1
            int pageSize = 5;             // Cantidad de registros por página

            var usuarios = await _serviceUsuario.ListAsync();
            var pagedUsuarios = usuarios.ToPagedList(pageNumber, pageSize);

            return View(pagedUsuarios);
        }

        // GET: UsuarioController/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _serviceUsuario.FindByIdAsync(id.Value);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // GET: UsuarioController/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UsuarioController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UsuarioDTO dto)
        {
            if (ModelState.IsValid)
            {
                await _serviceUsuario.AddAsync(dto);
                return RedirectToAction(nameof(Index));
            }
            return View(dto);
        }

        // GET: UsuarioController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var usuario = await _serviceUsuario.FindByIdAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return View(usuario);
        }

        // POST: UsuarioController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UsuarioDTO dto)
        {
            if (ModelState.IsValid)
            {
                await _serviceUsuario.UpdateAsync(id, dto);
                return RedirectToAction(nameof(Index));
            }
            return View(dto);
        }

        // GET: UsuarioController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var usuario = await _serviceUsuario.FindByIdAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return View(usuario);
        }

        // POST: UsuarioController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _serviceUsuario.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}