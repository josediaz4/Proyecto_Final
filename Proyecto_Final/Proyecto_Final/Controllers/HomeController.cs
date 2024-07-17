using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto_Final.Data;
using Proyecto_Final.Data.Entidades;
using Proyecto_Final.Helpers;
using Proyecto_Final.Models;
using System.Diagnostics;

namespace Proyecto_Final.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBlobHelper _helpersBlob;
        private readonly IUserHelper _userHelper;
        private readonly DataContext _context;

        public HomeController(ILogger<HomeController> logger, DataContext context, IBlobHelper helpersBlob, IUserHelper userHelper)
        {
            _logger = logger;
            _helpersBlob = helpersBlob;
            _userHelper = userHelper;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            List<Service> serviceModel = await GetServices();
            return View(serviceModel);
        }

        public async Task<List<Service>> GetServices()
        {
            return await _context.Servicios.ToListAsync();
        }

        public async Task<IActionResult> DetailService(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var service = await _context.Servicios
                .FirstOrDefaultAsync(m => m.Id == id);
            if (service == null)
            {
                return NotFound();
            }

            return View(service);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

		[Route("error/404")]
		public IActionResult Error404()
		{
			return View();
		}
  

    }
}
