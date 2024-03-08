
using NewCentury.Data.Context;
using NewCentury.Domain.Intefaces;
using NewCentury.Domain.Models;
using Microsoft.EntityFrameworkCore;
using NewCentury.Domain.Models.Enum;

namespace NewCentury.Data.Repository
{
    public class PartidaRepository : Repository<Partida>, IPartidaRepository
    {
        public PartidaRepository(MeuDbContext context) : base(context)
        {

        }

        public async Task<int> ContarResultadosPorVencedor(Guid id, string vencedor)
        {
            if (vencedor != "maquina" && vencedor != "usuario")
            {
                throw new ArgumentException("O vencedor deve ser 'maquina' ou 'usuario'");
            }

            if (vencedor == "maquina")
            {
                return await Db.Rodadas
                    .Where(r => r.PartidaId == id && r.Resultado == Resultado.WRONG)
                    .CountAsync();
            }
            else
            {
                return await Db.Rodadas
                    .Where(r => r.PartidaId == id && r.Resultado == Resultado.SUCCESS)
                    .CountAsync();
            }
        }

        public async Task<int> ContarResultadosPorVencedorMaquina(Guid id)
        {
            return await ContarResultadosPorVencedor(id, "maquina");
        }

        public async Task<int> ContarResultadosPorVencedorUsuario(Guid id)
        {
            return await ContarResultadosPorVencedor(id, "usuario");
        }
    }
}