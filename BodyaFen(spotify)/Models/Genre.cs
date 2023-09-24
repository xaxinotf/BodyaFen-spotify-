namespace BodyaFen_spotify_.Models
{
    public class Genre
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<Song>? Songs { get; set; }
    }
}
