namespace DisneyApi.Core.Models.EntitiesConfigurations
{
    using DisneyApi.Core.Models.Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class PersonajePeliculaSerieEntityConfiguration : IEntityTypeConfiguration<PersonajePeliculaSerie>
    {
        public void Configure(EntityTypeBuilder<PersonajePeliculaSerie> builder)
        {
            builder.HasKey(x => new { x.IdPersonaje, x.IdPeliculaSerie });

            /*builder.HasOne(x => x.Personaje)
                .WithMany(x => x.PeliculasSeries)
                .HasForeignKey(x => x.IdPersonaje);

            builder.HasOne(x => x.PeliculaSerie)
                .WithMany(x => x.Personajes)
                .HasForeignKey(x => x.IdPeliculaSerie);*/


        }
    }
}
