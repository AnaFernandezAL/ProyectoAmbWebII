using Microsoft.AspNetCore.Mvc;
using proyecto.Application.Services.Interfaces;
using proyecto.Application.DTOs;
using X.PagedList;
using X.PagedList.Extensions;
using System.Threading.Tasks;

namespace proyecto.Web.Controllers
{
    public class SubastaController : Controller
    {
        private readonly IServiceSubasta _serviceSubasta;

        public SubastaController(IServiceSubasta serviceSubasta)
        {
            _serviceSubasta = serviceSubasta;
        }

        // GET: SubastasController/Index
        public async Task<IActionResult> Index(int? page)
        {
            int pageNumber = page ?? 1;
            int pageSize = 5;

            var subastas = await _serviceSubasta.ListAsync();
            var pagedSubastas = subastas.ToPagedList(pageNumber, pageSize);

            return View(pagedSubastas);
        }

        // GET: SubastasController/Activas
        public async Task<IActionResult> Activas(int? page)
        {
            int pageNumber = page ?? 1;
            int pageSize = 5;

            var subastasActivas = await _serviceSubasta.ListActivasAsync();
            var pagedActivas = subastasActivas.ToPagedList(pageNumber, pageSize);

            return View(pagedActivas);
        }

        // GET: SubastasController/Finalizadas
        public async Task<IActionResult> Finalizadas(int? page)
        {
            int pageNumber = page ?? 1;
            int pageSize = 5;

            var subastasFinalizadas = await _serviceSubasta.ListFinalizadasAsync();
            var pagedFinalizadas = subastasFinalizadas.ToPagedList(pageNumber, pageSize);

            return View(pagedFinalizadas);
        }

        // GET: SubastasController/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subasta = await _serviceSubasta.FindByIdAsync(id.Value);
            if (subasta == null)
            {
                return NotFound();
            }

            return View(subasta);
        }

        // CRUD opcional (si se requiere crear/editar subastas desde la web)
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SubastaDTO dto)
        {
            if (ModelState.IsValid)
            {
                await _serviceSubasta.AddAsync(dto);
                return RedirectToAction(nameof(Index));
            }
            return View(dto);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var subasta = await _serviceSubasta.FindByIdAsync(id);
            if (subasta == null)
            {
                return NotFound();
            }
            return View(subasta);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SubastaDTO dto)
        {
            if (ModelState.IsValid)
            {
                await _serviceSubasta.UpdateAsync(id, dto);
                return RedirectToAction(nameof(Index));
            }
            return View(dto);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var subasta = await _serviceSubasta.FindByIdAsync(id);
            if (subasta == null)
            {
                return NotFound();
            }
            return View(subasta);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _serviceSubasta.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}