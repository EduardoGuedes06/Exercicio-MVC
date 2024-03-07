using NewCentury.Domain.Models;

namespace NewCentury.Domain.Intefaces
{
    public interface IPartidaService : IDisposable
    {
        Task<Partida> Adicionar(Partida partida);
        Task Atualizar(Partida Partida);
    }
}