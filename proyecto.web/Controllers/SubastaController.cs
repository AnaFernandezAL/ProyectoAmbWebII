using Microsoft.AspNetCore.Mvc;
using proyecto.Application.DTOs;
using proyecto.Application.Services.Implementations;
using proyecto.Application.Services.Interfaces;
using proyecto.Infraestructure.Models;
using proyecto.web.Models;
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

        // Clave única para almacenar el carrito en Session
        private const string CartSessionKey = "CartShopping";

        public SubastaController(IServiceSubasta serviceSubasta)
        {
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
                subasta = new SubastaDTO
                {
                    SubastaId = nextReceiptNumber
                },
                NombreCliente = "-"
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SubastaCreateViewModel vm)
        {
            try
            {
                // Validar modelo básico
                if (!ModelState.IsValid)
                {
                    return BadRequest("Los datos de la orden no son válidos. Verifique el cliente y la información general.");
                }



                // Validar que el cliente exista
                var vendedor = await _serviceUsuario.FindByIdAsync(vm.subasta.Vendedor.UsuarioId);
                if (vendedor is null)
                {
                    return BadRequest("El vendedor indicado no existe.");
                }

                var carta = await _serviceCarta.FindByIdAsync(vm.subasta.Carta.CartaId);
                if (carta is null)
                {
                    return BadRequest("La carta indicada no existe.");
                }

                // Completar y validar algunos datos de la subasta 
                var subasta = vm.subasta;

                if (subasta.Carta is null || subasta.EstadoSubasta is null || subasta.Vendedor is null)
                {
                    return BadRequest("Faltan datos obligatorios.");
                }

                subasta.SubastaId = 0;
                subasta.FechaInicio = subasta.FechaInicio;
                subasta.FechaCierre = subasta.FechaCierre;
                subasta.PrecioBase = subasta.PrecioBase;
                subasta.IncrementoMinimo = subasta.IncrementoMinimo;
                subasta.EstadoSubastaId = 1;
                subasta.CartaId = subasta.Carta.CartaId;
                subasta.VendedorId = 1;

                await _serviceSubasta.AddAsync(subasta);

                // Respuesta JSON para que el JS de la vista redireccione amigablemente
                return Json(new
                {
                    success = true,
                    redirectUrl = Url.Action(nameof(Index))
                });
            }
            catch (Exception ex)
            {
                // registrar el error (logging).
                return BadRequest($"Error al guardar la orden: {ex.Message}");
            }
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