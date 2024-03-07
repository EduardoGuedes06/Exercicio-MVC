using NewCentury.Domain.Models;

namespace NewCentury.Domain.Intefaces
{
    public interface IRodadaService : IDisposable
    {
        Task Adicionar(Rodada Rodada);
        Task Atualizar(Rodada Rodada);
        Task Remover(Guid id);
    }
}