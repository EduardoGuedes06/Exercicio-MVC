
using NewCentury.Data.Context;
using NewCentury.Domain.Intefaces;
using NewCentury.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace NewCentury.Data.Repository
{
    public class JogadorRepository : Repository<Jogador>, IJogadorRepository
    {
        public JogadorRepository(MeuDbContext context) : base(context)
        {

        }

        public async Task<Jogador> ObterJogadorPorNome(string nome)
        {
            return await Db.Jogador
                .FirstOrDefaultAsync(c => c.Nome == nome);
        }


    }
}