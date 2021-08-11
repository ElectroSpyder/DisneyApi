namespace DisneyApi.Core.Models.Context
{
    using DisneyApi.Core.Models.Entities;
    using DisneyApi.Core.Models.EntitiesConfigurations;
    using Microsoft.EntityFrameworkCore;

    public class DisneyDBContext : DbContext
    {
        public DisneyDBContext(DbContextOptions<DisneyDBContext> options) : base(options)
        {

        }
        public DbSet<Personaje> Personaje { get; set; }
        public DbSet<PeliculaSerie> PeliculaSerie { get; set; }
        public DbSet<Genero> Genero { get; set; }
        //public DbSet<PersonajePeliculaSerie> PersonajePeliculaSeries { get; set; }

        protected override  void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
           
        }
    }
}
