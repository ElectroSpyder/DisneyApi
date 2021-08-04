using DisneyApi.Core.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DisneyApi.Core.Models.EntitiesConfigurations
{
    public class PeliculaSerieEntityConfiguration: IEntityTypeConfiguration<PeliculaSerie>
    {
        public void Configure(EntityTypeBuilder<PeliculaSerie> builder)
        {
            builder.HasOne(x => x.Genero)
                .WithMany(x => x.PeliculaSeries)
                .HasForeignKey(x => x.IdGenero);
        }
    }
}
