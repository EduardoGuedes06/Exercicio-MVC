using NewCentury.Domain.Models;
using System;
using System.Threading.Tasks;

namespace NewCentury.Domain.Intefaces
{
    public interface IRodadaRepository : IRepository<Rodada>
    {
        Task<string> CalcularDiferencaDatasEmMinutosPorPartida(Guid id);
        Task<IEnumerable<Rodada>> ObterRodadasPorIdDaPartida(Guid partidaId);
    }
}