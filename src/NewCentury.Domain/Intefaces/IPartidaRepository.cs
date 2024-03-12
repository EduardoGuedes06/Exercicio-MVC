using NewCentury.Domain.Models;
using System;
using System.Threading.Tasks;

namespace NewCentury.Domain.Intefaces
{
    public interface IPartidaRepository : IRepository<Partida>
    {
        Task<int> ContarResultadosPorVencedor(Guid id, string vencedor);
        Task<int> ContarResultadosPorVencedorMaquina(Guid id);
        Task<int> ContarResultadosPorVencedorUsuario(Guid id);
        Task<List<Partida>> ObterPartidasComRodadasEJogadoresPorPeriodo(DateTime dataInicial, DateTime dataFinal);
    }
}