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

        public async Task<IActionResult> Index(int? page)
        {
            int pageNumber = page ?? 1;  
            int pageSize = 5;            

            var cartas = await _serviceCarta.ListAsync();
            var pagedCartas = cartas.ToPagedList(pageNumber, pageSize);

            return View(pagedCartas);
        }

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

        public IActionResult Create()
        {
            return View();
        }

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

        public async Task<IActionResult> Edit(int id)
        {
            var carta = await _serviceCarta.FindByIdAsync(id);
            if (carta == null)
            {
                return NotFound();
            }
            return View(carta);
        }

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

        public async Task<IActionResult> Delete(int id)
        {
            var carta = await _serviceCarta.FindByIdAsync(id);
            if (carta == null)
            {
                return NotFound();
            }
            return View(carta);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _serviceCarta.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}