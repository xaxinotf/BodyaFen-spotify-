using BodyaFen_spotify_.Contexts;
using BodyaFen_spotify_.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Claims;

namespace BodyaFen_spotify_.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly BodyaFenDbContext _context;

        public HomeController(ILogger<HomeController> logger, BodyaFenDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var artistId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var artist = await _context.Artists.Include(a => a.Photo).FirstOrDefaultAsync(a=>a.Id == artistId);
            return View(artist);
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
    }
}