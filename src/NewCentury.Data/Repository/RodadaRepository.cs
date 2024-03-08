
using NewCentury.Data.Context;
using NewCentury.Domain.Intefaces;
using NewCentury.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace NewCentury.Data.Repository
{
    public class RodadaRepository : Repository<Rodada>, IRodadaRepository
    {
        public RodadaRepository(MeuDbContext context) : base(context)
        {

        }
        public async Task<string> CalcularDiferencaDatasEmMinutosPorPartida(Guid id)
        {
            var rodadas = await Db.Rodadas
                .Where(r => r.PartidaId == id)
                .ToListAsync();

            if (rodadas == null || rodadas.Count == 0)
            {
                return "00:00:00"; // Se não houver rodadas, retorna 00:00:00
            }

            var dataMinima = rodadas.Min(r => r.DataCadastro);
            var dataMaxima = rodadas.Max(r => r.DataCadastro);

            var diferenca = dataMaxima.Subtract(dataMinima); // Calcula a diferença entre as datas

            // Formata a diferença para retornar apenas horas, minutos e segundos
            var diferencaFormatada = $"{diferenca.Hours:00}:{diferenca.Minutes:00}:{diferenca.Seconds:00}";

            return diferencaFormatada;
        }

        public async Task<IEnumerable<Rodada>> ObterRodadasPorIdDaPartida(Guid partidaId)
        {
            return await Db.Rodadas
                .Where(r => r.PartidaId == partidaId)
                .ToListAsync();
        }


    }
}