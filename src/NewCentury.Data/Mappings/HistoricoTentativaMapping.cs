
using NewCentury.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace NewCentury.Data.Mappings
{
    public class HistoricoTentativaMapping : IEntityTypeConfiguration<HistoricoTentativas>
    {
        public void Configure(EntityTypeBuilder<HistoricoTentativas> builder)
        {
            builder.HasKey(p => p.Id);


            builder.Property(ht => ht.Resultado)
                .IsRequired()
                .HasMaxLength(20);

           

            builder.ToTable("HistoricoTentativas");
        }
    }
}