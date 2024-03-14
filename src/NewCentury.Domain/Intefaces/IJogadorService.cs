using NewCentury.Domain.Models;

namespace NewCentury.Domain.Intefaces
{
    public interface IJogadorService : IDisposable
    {
        Task<Guid> AdicionarJogador(string nome);
        Task<string> ObterClassificacao(double taxaVitoria);
    }
}