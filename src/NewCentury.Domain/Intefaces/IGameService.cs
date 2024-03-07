using NewCentury.Domain.Models;

namespace NewCentury.Domain.Intefaces
{
    public interface IGameService : IDisposable
    {
        Task<int> GerarNumeroSecreto(string dificuldade);
    }
}