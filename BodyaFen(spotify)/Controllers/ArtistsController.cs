using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BodyaFen_spotify_.Contexts;
using BodyaFen_spotify_.Models;
using Azure.Storage.Blobs;
using BodyaFen_spotify_.Dopomoga;
using Microsoft.Extensions.Options;

namespace BodyaFen_spotify_.Controllers
{
    public class ArtistsController : Controller
    {
        private readonly BodyaFenDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly BlobServiceClient _client;
        private readonly BlobServiceClient _client;

        public ArtistsController(BodyaFenDbContext context, IOptions<AzureConfig> azureConfig, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
            _client = new BlobServiceClient(azureConfig.Value.AzureConnString);
        }

        public async Task<string> InsertImage(Stream stream, string containerName, string blobName)
        {
            var containerClient = _client.GetBlobContainerClient(containerName);
            await containerClient.CreateIfNotExistsAsync();

            var blobClient = containerClient.GetBlobClient(blobName);

            await blobClient.UploadAsync(stream, true);

            return blobClient.Uri.ToString();
        }

        // GET: Artists
        public async Task<IActionResult> Index()
        {
            return _context.Artists != null ?
                        View(await _context.Artists.ToListAsync()) :
                        Problem("Entity set 'BodyaFenDbContext.Artists'  is null.");
        }

        // GET: Artists/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Artists == null)
            {
                return NotFound();
            }

            var artist = await _context.Artists.Include(a => a.Photo)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (artist == null)
            {
                return NotFound();
            }

            return View(artist);
        }

        // GET: Artists/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Artists/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Create(IFormFile imageFile, Artist artist)
        {

            if (imageFile != null && imageFile.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await imageFile.CopyToAsync(memoryStream);
                    memoryStream.Seek(0, SeekOrigin.Begin);
                    var urlOfImage = await InsertImage(memoryStream, _configuration["NameContainer"], Guid.NewGuid().ToString());
                    artist.Photo = new Photo {Url = urlOfImage };
                }
            }
            _context.Artists.Add(artist);
            _context.SaveChanges();

            return RedirectToAction("Index"); // Redirect to a success page or display a message
        }

        // GET: Artists/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Artists == null)
            {
                return NotFound();
            }

            var artist = await _context.Artists.FindAsync(id);
            if (artist == null)
            {
                return NotFound();
            }
            return View(artist);
        }

        // POST: Artists/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name")] Artist artist)
        {
            if (id != artist.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(artist);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArtistExists(artist.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(artist);
        }

        // GET: Artists/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Artists == null)
            {
                return NotFound();
            }

            var artist = await _context.Artists
                .FirstOrDefaultAsync(m => m.Id == id);
            if (artist == null)
            {
                return NotFound();
            }

            return View(artist);
        }

        // POST: Artists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Artists == null)
            {
                return Problem("Entity set 'BodyaFenDbContext.Artists'  is null.");
            }
            var artist = await _context.Artists.FindAsync(id);
            if (artist != null)
            {
                _context.Artists.Remove(artist);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ArtistExists(Guid id)
        {
            return (_context.Artists?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
