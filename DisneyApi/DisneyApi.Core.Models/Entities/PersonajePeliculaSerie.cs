namespace DisneyApi.Core.Models.Entities
{
    public class PersonajePeliculaSerie
    {
        public int IdPersonaje { get; set; }
        public Personaje Personaje { get; set; }

        public int IdPeliculaSerie { get; set; }
        public PeliculaSerie PeliculaSerie { get; set; }
    }
}
