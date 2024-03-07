
using NewCentury.Data.Context;
using NewCentury.Domain.Intefaces;
using NewCentury.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace NewCentury.Data.Repository
{
    public class HistoricoTentativaRepository : Repository<HistoricoTentativas>, IHistoricoTentativaRepository
    {
        public HistoricoTentativaRepository(MeuDbContext context) : base(context)
        {
        }

    }
}