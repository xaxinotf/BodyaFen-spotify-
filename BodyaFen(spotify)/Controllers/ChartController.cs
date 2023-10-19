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

        [HttpGet("JsonData")]
        public async Task<JsonResult> JsonData()
        {
            var genres = await _context.Genres.Include(g => g.Songs).ToListAsync();

            var data = new List<object>
            {
                new { GenreType = "Genre Type", NumberOfSongs = "Number of Songs" }
            };

            data.AddRange(genres.Select(g => new { GenreType = g.Name, NumberOfSongs = g.Songs.Count }));

            return new JsonResult(data);
        }
    }
}
