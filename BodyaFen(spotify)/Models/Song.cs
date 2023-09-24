namespace BodyaFen_spotify_.Models
{
    public class Song
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public Artist Artist { get; set; }

        public Genre Genre { get; set; }
    }
}
