using Microsoft.AspNetCore.Mvc;
using proyecto.Application.DTOs;
using proyecto.Application.Services.Implementations;
using proyecto.Application.Services.Interfaces;
using proyecto.Infraestructure.Models;
using X.PagedList;
using X.PagedList.Extensions;

namespace proyecto.web.Controllers
{
    public class PujasController : Controller
    {
        private readonly IServicePujas _servicePujas;
        public PujasController(IServicePujas servicePujas)
        {
            _servicePujas = servicePujas;
        }

        public async Task<IActionResult> Index(int? subastaId, int? page)
        {
            int pageNumber = page ?? 1; int pageSize = 10; ICollection<PujasDTO> pujas; 
            if (subastaId.HasValue)
            {
             pujas = await _servicePujas.FindByIDSubastaAsync(subastaId.Value);
            } else { 
                pujas = await _servicePujas.ListAsync(); 
            }
                var pagedPujas = pujas.ToPagedList(pageNumber, pageSize); 
                return View(pagedPujas);
            }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pujas = await _servicePujas.FindByIDSubastaAsync(id.Value);

            if (pujas == null)
            {
                return NotFound();
            }

            return View(pujas);
        }
    }
}
