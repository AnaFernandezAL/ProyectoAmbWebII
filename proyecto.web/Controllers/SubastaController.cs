using Microsoft.AspNetCore.Mvc;
using proyecto.Application.DTOs;
using proyecto.Application.Services.Implementations;
using proyecto.Application.Services.Interfaces;
using proyecto.Infraestructure.Models;
using proyecto.web.Util;
using proyecto.web.ViewModels;
using System.Threading.Tasks;
using X.PagedList;
using X.PagedList.Extensions;

namespace proyecto.Web.Controllers
{
    public class SubastaController : Controller
    {
        private readonly IServiceSubasta _serviceSubasta;
        private readonly IServiceUsuario _serviceUsuario;
        private readonly IServiceCarta _serviceCarta;

        private const string CartSessionKey = "CartShopping";

        public SubastaController(IServiceCarta serviceCarta, IServiceSubasta serviceSubasta)
        {
            _serviceCarta = serviceCarta;
            _serviceSubasta = serviceSubasta;
        }

        public async Task<IActionResult> Index(int? page)
        {
            int pageNumber = page ?? 1;
            int pageSize = 5;

            var subastas = await _serviceSubasta.ListAsync();
            var pagedSubastas = subastas.ToPagedList(pageNumber, pageSize);

            return View(pagedSubastas);
        }

        public async Task<IActionResult> Activas(int? page)
        {
            int pageNumber = page ?? 1;
            int pageSize = 5;

            var subastasActivas = await _serviceSubasta.ListActivasAsync();
            var pagedActivas = subastasActivas.ToPagedList(pageNumber, pageSize);

            return View(pagedActivas);
        }

        public async Task<IActionResult> Finalizadas(int? page)
        {
            int pageNumber = page ?? 1;
            int pageSize = 5;

            var subastasFinalizadas = await _serviceSubasta.ListFinalizadasAsync();
            var pagedFinalizadas = subastasFinalizadas.ToPagedList(pageNumber, pageSize);

            return View(pagedFinalizadas);
        }

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
        public async Task<IActionResult> Create()
        {
            var nextReceiptNumber = await _serviceSubasta.GetNextNumberOrden();

            var vm = new SubastaCreateViewModel
            {
                Subasta = new SubastaCreateDTO
                {
                }
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SubastaCreateViewModel vm)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (vm?.Subasta == null)
                return BadRequest("Datos no recibidos correctamente");

            var dto = vm.Subasta;

            if (dto.CartaId == 0)
                return BadRequest("Debe seleccionar una carta");

            if (dto.FechaInicio < DateTime.Now)
                return BadRequest("La fecha de inicio debe ser mayor o igual a la fecha actual");

            if (dto.FechaCierre <= dto.FechaInicio)
                return BadRequest("La fecha de cierre debe ser posterior a la fecha de inicio");

            var carta = await _serviceCarta.FindByIdAsync(dto.CartaId);
            if (carta is null)
                return BadRequest("La carta no existe");

            var subasta = new SubastaDTO
            {
                CartaId = dto.CartaId,
                FechaInicio = dto.FechaInicio,
                FechaCierre = dto.FechaCierre,
                PrecioBase = dto.PrecioBase,
                IncrementoMinimo = dto.IncrementoMinimo,
                EstadoSubastaId = 1,
                VendedorId = 1
            };

            var subastaCreada = await _serviceSubasta.AddAsync(subasta);

            return Json(new
            {
                success = true,
                redirectUrl = Url.Action(nameof(Index)),
                estado = subastaCreada?.EstadoSubasta?.NombreEstado 
            });
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
            if (!ModelState.IsValid)
                return View(dto);

            try
            {
                await _serviceSubasta.UpdateAsync(id, dto);

                TempData["Notificacion"] = SweetAlertHelper.CrearNotificacion(
                    "Subasta editada correctamente",
                    $"La subasta {id} fue actualizada exitosamente.",
                    SweetAlertMessageType.success
                );

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["Notificacion"] = SweetAlertHelper.CrearNotificacion(
                    "Error al editar",
                    ex.Message,
                    SweetAlertMessageType.error
                );
                return View(dto);
            }
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