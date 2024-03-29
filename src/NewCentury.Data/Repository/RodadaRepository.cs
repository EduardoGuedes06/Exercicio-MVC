﻿
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

        //Obter dados Dash

        public async Task<List<Rodada>> ObterRodadasComPartidasEJogadoresPorPeriodo(DateTime dataInicial, DateTime dataFinal)
        {
            dataInicial = dataInicial.Date;
            dataFinal = dataFinal.Date.AddDays(1).AddTicks(-1);

            return await Db.Rodadas
                .Include(r => r.Partida)
                    .ThenInclude(p => p.Jogador)
                .Where(r => r.DataCadastro >= dataInicial && r.DataCadastro <= dataFinal)
                .Where(r => !string.IsNullOrEmpty(r.Partida.Vencedor))
                .ToListAsync();
        }
        public async Task<string> CalcularDiferencaDatasEmMinutosPorPartida(Guid id)
        {
            var rodadas = await Db.Rodadas
                .Where(r => r.PartidaId == id)
                .ToListAsync();

            if (rodadas == null || rodadas.Count == 0)
            {
                return "00:00:00";
            }
            var dataMinima = rodadas.Min(r => r.DataCadastro);
            var dataMaxima = rodadas.Max(r => r.DataCadastro);
            var diferenca = dataMaxima.Subtract(dataMinima);
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