namespace BodyaFen_spotify_.Models
{
    public class Artist
    {

        public Guid Id { get; set; }

        public string Name { get; set; }

        public Photo? Photo { get; set; }

        public List<Song>? Songs { get; set; }   

    }
}
