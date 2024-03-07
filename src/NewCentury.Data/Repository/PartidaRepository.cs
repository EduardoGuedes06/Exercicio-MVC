
using NewCentury.Data.Context;
using NewCentury.Domain.Intefaces;
using NewCentury.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace NewCentury.Data.Repository
{
    public class PartidaRepository : Repository<Partida>, IPartidaRepository
    {
        public PartidaRepository(MeuDbContext context) : base(context)
        {

        }



    }
}