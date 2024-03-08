using NewCentury.Domain.Models;

namespace NewCentury.Domain.Intefaces
{
    public interface IPartidaService : IDisposable
    {
        Task<Partida> Adicionar(Partida partida);
        Task Atualizar(Partida Partida);
        Task AtualizarVencedor(Guid id);
        Task<string> CalcularVencedor(Guid id);
        Task<Partida> ObterPorId(Guid id);
    }
}