using BodyaFen_spotify_.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace BodyaFen_spotify_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChartController : ControllerBase
    {
        private readonly BodyaFenDbContext _context;
        public ChartController(BodyaFenDbContext context)
        {
            _context = context;
        }

        [HttpGet("RationGenresToSongs")]
        public async Task<JsonResult> RationGenresToSongs()
        {
            var genres = await _context.Genres.Include(g => g.Songs).ToListAsync();

            var data = genres.Select(g => new { GenreType = g.Name, NumberOfSongs = g.Songs.Count })
                .ToList();

            return new JsonResult(data);
        }
    }
}
