using Microsoft.AspNetCore.Mvc;
using proyecto.Application.Services.Interfaces;

namespace proyecto.web.Controllers
{
    public class SubastaController : Controller
    {
        private readonly IServiceSubasta _serviceSubasta;

        public SubastaController(IServiceSubasta serviceSubasta)
        {
            _serviceSubasta = serviceSubasta;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var collection = await _serviceSubasta.ListAsync();
            return View(collection);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var subasta = await _serviceSubasta.FindByIdAsync(id);
            if (subasta == null)
            {
                return NotFound();
            }
            return View(subasta);
        }
    }
}
