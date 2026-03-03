using Microsoft.AspNetCore.Mvc;
using proyecto.Application.DTOs;
using proyecto.Application.Services.Interfaces;
using X.PagedList;
using X.PagedList.Extensions;

namespace proyecto.Web.Controllers
{
    public class CartaController : Controller
    {
        private readonly IServiceCarta _serviceCarta;

        public CartaController(IServiceCarta serviceCarta)
        {
            _serviceCarta = serviceCarta;
        }

        // GET: CartaController
        public async Task<IActionResult> Index(int? page)
        {
            int pageNumber = page ?? 1;   // Página actual, por defecto 1
            int pageSize = 5;             // Cantidad de registros por página

            var cartas = await _serviceCarta.ListAsync();
            var pagedCartas = cartas.ToPagedList(pageNumber, pageSize);

            return View(pagedCartas);
        }

        // GET: CartaController/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carta = await _serviceCarta.FindByIdAsync(id.Value);
            if (carta == null)
            {
                return NotFound();
            }

            return View(carta);
        }

        // GET: CartaController/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CartaController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CartaDTO dto)
        {
            if (ModelState.IsValid)
            {
                await _serviceCarta.AddAsync(dto);
                return RedirectToAction(nameof(Index));
            }
            return View(dto);
        }

        // GET: CartaController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var carta = await _serviceCarta.FindByIdAsync(id);
            if (carta == null)
            {
                return NotFound();
            }
            return View(carta);
        }

        // POST: CartaController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CartaDTO dto)
        {
            if (ModelState.IsValid)
            {
                await _serviceCarta.UpdateAsync(id, dto);
                return RedirectToAction(nameof(Index));
            }
            return View(dto);
        }

        // GET: CartaController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var carta = await _serviceCarta.FindByIdAsync(id);
            if (carta == null)
            {
                return NotFound();
            }
            return View(carta);
        }

        // POST: CartaController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _serviceCarta.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}