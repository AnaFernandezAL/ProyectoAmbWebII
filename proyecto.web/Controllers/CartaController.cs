using Microsoft.AspNetCore.Mvc;
using proyecto.Application.DTOs;
using proyecto.Application.Services.Interfaces;
using X.PagedList;
using X.PagedList.Extensions;
using proyecto.web.Util;


namespace proyecto.Web.Controllers
{
    public class CartaController : Controller
    {
        private readonly IServiceCarta _serviceCarta;
        private readonly IServiceCategoria _serviceCategoria;
        private readonly IServiceUsuario _serviceUsuario;

        public CartaController(IServiceCarta serviceCarta, IServiceCategoria serviceCategoria, IServiceUsuario serviceUsuario)
        {
            _serviceCarta = serviceCarta;
            _serviceCategoria = serviceCategoria;
            _serviceUsuario = serviceUsuario;
        }

        public async Task<IActionResult> Index(int? page)
        {
            int pageNumber = page ?? 1;
            int pageSize = 6;

            var cartas = await _serviceCarta.ListAsync();
            var pagedCartas = cartas.ToPagedList(pageNumber, pageSize);

            return View(pagedCartas);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                TempData["Notificacion"] = SweetAlertHelper.CrearNotificacion(
                    "Carta no encontrada",
                    "No existe una carta sin ID",
                    SweetAlertMessageType.error
                );
                return RedirectToAction(nameof(Index));
            }

            var carta = await _serviceCarta.FindByIdAsync(id.Value);
            if (carta == null)
            {
                TempData["Notificacion"] = SweetAlertHelper.CrearNotificacion(
                    "Carta no encontrada",
                    "La carta solicitada no existe",
                    SweetAlertMessageType.error
                );
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Notificacion = SweetAlertHelper.CrearNotificacion(
                "Detalle de carta",
                $"Mostrando información de la carta: {carta.NombreCarta}",
                SweetAlertMessageType.info
            );

            return View(carta);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Categorias = await _serviceCategoria.ListAsync();
            ViewBag.UsuarioActual = await _serviceUsuario.FindByIdAsync(1);
            return View(new CartaDTO());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CartaDTO dto, int[] categoriasSeleccionadas)
        {
            if (!ModelState.IsValid || categoriasSeleccionadas.Length == 0 || dto.ImagenesCarta == null || dto.ImagenesCarta.Count == 0)
            {
                var errores = string.Join("<br>",
                    ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)
                );

                ViewBag.Categorias = await _serviceCategoria.ListAsync();
                ViewBag.UsuarioActual = await _serviceUsuario.FindByIdAsync(1);

                ViewBag.Notificacion = SweetAlertHelper.CrearNotificacion(
                    "Errores de validación",
                    $"El formulario contiene errores:<br>{errores}<br>Debe seleccionar al menos una categoría y subir al menos una imagen.",
                    SweetAlertMessageType.warning
                );
                return View(dto);
            }

            dto.CartaCategoria = categoriasSeleccionadas
                .Select(c => new CartaCategoriaDTO { CategoriaId = c })
                .ToList();

            dto.EstadoCartaId = 1;
            dto.VendedorId = 1;

            dto.ImagenesCartaNavigation = new List<ImagenCartaDTO>();
            for (int i = 0; i < dto.ImagenesCarta.Count; i++)
            {
                var file = dto.ImagenesCarta[i];
                var fileName = Path.GetFileName(file.FileName);
                var imagesPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images");
                if (!Directory.Exists(imagesPath)) Directory.CreateDirectory(imagesPath);

                var filePath = Path.Combine(imagesPath, fileName);

                if (!System.IO.File.Exists(filePath))
                {
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                }

                dto.ImagenesCartaNavigation.Add(new ImagenCartaDTO
                {
                    UrlImagen = "/Images/" + fileName,
                    EsPrincipal = (i == dto.ImagenPrincipalIndex) 
                });
            }

            await _serviceCarta.AddAsync(dto);

            TempData["Notificacion"] = SweetAlertHelper.CrearNotificacion(
                "Carta creada correctamente",
                $"La carta {dto.NombreCarta} fue registrada exitosamente.",
                SweetAlertMessageType.success
            );

            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Edit(int id)
        {
            var carta = await _serviceCarta.FindByIdAsync(id);
            if (carta == null) return NotFound();

            if (carta.Subastas.Any(s => s.EstadoSubastaId == 1))
            {
                TempData["Notificacion"] = SweetAlertHelper.CrearNotificacion(
                    "Edición no permitida",
                    "La carta está en una subasta activa y no puede editarse.",
                    SweetAlertMessageType.error
                );
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Categorias = await _serviceCategoria.ListAsync();
            ViewBag.UsuarioActual = await _serviceUsuario.FindByIdAsync(1);

            return View(carta);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CartaDTO dto, int[] categoriasSeleccionadas)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categorias = await _serviceCategoria.ListAsync();
                return View(dto);
            }

            dto.CartaCategoria = categoriasSeleccionadas
                .Select(c => new CartaCategoriaDTO { CategoriaId = c })
                .ToList();

            if (dto.ImagenesCarta != null && dto.ImagenesCarta.Count > 0)
            {
                dto.ImagenesCartaNavigation = new List<ImagenCartaDTO>();
                for (int i = 0; i < dto.ImagenesCarta.Count; i++)
                {
                    var file = dto.ImagenesCarta[i];
                    var fileName = Path.GetFileName(file.FileName);
                    var imagesPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images");
                    if (!Directory.Exists(imagesPath)) Directory.CreateDirectory(imagesPath);

                    var filePath = Path.Combine(imagesPath, fileName);

                    if (!System.IO.File.Exists(filePath))
                    {
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }
                    }

                    dto.ImagenesCartaNavigation.Add(new ImagenCartaDTO
                    {
                        UrlImagen = "/Images/" + fileName,
                        EsPrincipal = (i == dto.ImagenPrincipalIndex)
                    });
                }
            }
            else
            {
                if (dto.ImagenesCartaNavigation != null && dto.ImagenesCartaNavigation.Any())
                {
                    int index = 0;
                    foreach (var img in dto.ImagenesCartaNavigation)
                    {
                        img.EsPrincipal = (index == dto.ImagenPrincipalIndex);
                        index++;
                    }
                }
            }

            await _serviceCarta.UpdateAsync(id, dto);

            TempData["Notificacion"] = SweetAlertHelper.CrearNotificacion(
                "Carta actualizada",
                $"La carta {dto.NombreCarta} ha sido modificada exitosamente.",
                SweetAlertMessageType.success
            );

            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeState(int id, int nuevoEstadoId)
        {
            var carta = await _serviceCarta.FindByIdAsync(id);
            if (carta == null) return NotFound();

            carta.EstadoCartaId = nuevoEstadoId;
            await _serviceCarta.UpdateAsync(id, carta);

            string accion = nuevoEstadoId == 1 ? "activada" : "desactivada";

            TempData["Notificacion"] = SweetAlertHelper.CrearNotificacion(
                $"Carta {accion}",
                $"La carta ha sido {accion} correctamente.",
                nuevoEstadoId == 1 ? SweetAlertMessageType.success : SweetAlertMessageType.warning
            );

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> GetCartaByName(string filtro)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(filtro))
                {
                    return Json(new List<object>());
                }

                var cartas = await _serviceCarta.FindByNameAsync(filtro);

                var result = cartas.Select(c => new
                {
                    cartaId = c.CartaId,
                    nombre = c.NombreCarta,
                    descripcion = c.Descripcion,
                    estado = c.EstadoCarta!.NombreEstado,
                    imagen = c.ImagenesCartaNavigation
                                .FirstOrDefault(i => i.EsPrincipal)?.UrlImagen
                });

                return Json(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}