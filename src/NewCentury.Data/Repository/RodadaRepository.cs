
using NewCentury.Data.Context;
using NewCentury.Domain.Intefaces;
using NewCentury.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace NewCentury.Data.Repository
{
    public class RodadaRepository : Repository<Rodada>, IRodadaRepository
    {
        public RodadaRepository(MeuDbContext context) : base(context)
        {

        }
        public async Task<IEnumerable<Rodada>> ObterRodadasPorIdDaPartida(Guid partidaId)
        {
            return await Db.Rodadas
                .Where(r => r.PartidaId == partidaId)
                .ToListAsync();
        }


    }
}