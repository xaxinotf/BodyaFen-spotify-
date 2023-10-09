using BodyaFen_spotify_.Contexts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BodyaFen_spotify_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChartController : ControllerBase
    {
        private BodyaFenDbContext _context;
        public ChartController(BodyaFenDbContext context)
        {
            _context = context;
        }

        [HttpGet("JsonData")]
        public JsonResult JsonData()
        {
            var genres = _context.Genres.Include(g => g.Songs).ToList();
            List<object> data = new List<object>();

            data.Add(new[] { "Genre Type", "Number of Songs" });

            foreach (var genre in genres)
            {
                data.Add(new object[] { genre.Name, genre.Songs.Count });
            }

            return new JsonResult(data);
        }

    }
}
