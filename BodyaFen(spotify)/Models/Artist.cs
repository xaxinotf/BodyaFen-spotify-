using Microsoft.AspNetCore.Identity;

namespace BodyaFen_spotify_.Models
{
    public class Artist : IdentityUser
    {

        public string? Name { get; set; }

        public Photo? Photo { get; set; }

        public List<Song>? Songs { get; set; }

    }
}
