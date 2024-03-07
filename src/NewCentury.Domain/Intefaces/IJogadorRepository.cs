using NewCentury.Domain.Models;
using System;
using System.Threading.Tasks;

namespace NewCentury.Domain.Intefaces
{
    public interface IJogadorRepository : IRepository<Jogador>
    {
        Task<Jogador> ObterJogadorPorNome(string nome);
    }
}