using NewCentury.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace NewCentury.Data.Mappings
{
    public class PartidaMapping : IEntityTypeConfiguration<Partida>
    {
        public void Configure(EntityTypeBuilder<Partida> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(c => c.Dificuldade)
                .IsRequired()
                .HasColumnType("varchar(50)");

            builder.Property(c => c.QuemComeca)
                .IsRequired()
                .HasColumnType("varchar(50)");

            builder.Property(c => c.Vencedor)
                .HasColumnType("varchar(50)");

            builder.ToTable("Partida");
        }
    }
}