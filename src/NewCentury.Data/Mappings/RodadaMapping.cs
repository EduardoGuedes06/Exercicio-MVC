using NewCentury.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace NewCentury.Data.Mappings
{
    public class RodadaMapping : IEntityTypeConfiguration<Rodada>
    {
        public void Configure(EntityTypeBuilder<Rodada> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(c => c.EscolhaMaquina)
                .IsRequired()
                .HasColumnType("varchar(50)");

            builder.Property(c => c.EscolhaJogador)
                .IsRequired()
                .HasColumnType("varchar(50)");

            builder.HasOne(r => r.Partida)
                .WithMany(p => p.Rodadas)
                .HasForeignKey(r => r.PartidaId);

            builder.ToTable("Rodada");
        }
    }
}