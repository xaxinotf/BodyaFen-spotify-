using BodyaFen_spotify_.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace BodyaFen_spotify_.Contexts
{
    public class BodyaFenDbContext : DbContext
    {
        public BodyaFenDbContext(DbContextOptions<BodyaFenDbContext> options)
        : base(options)
        {


        }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Song> Songs { get; set; }

    }

}