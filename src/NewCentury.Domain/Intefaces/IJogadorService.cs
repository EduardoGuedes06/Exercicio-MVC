using NewCentury.Domain.Models;

namespace NewCentury.Domain.Intefaces
{
    public interface IJogadorService : IDisposable
    {
        Task Adicionar(Jogador jogador);
        Task Atualizar(Jogador jogador);
        Task<string> ObterClassificacao(double taxaVitoria);
        Task Remover(Guid id);
    }
}