using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NewCentury.Data.Repository;
using NewCentury.Domain.Intefaces;
using NewCentury.Domain.Models;
using NewCentury.Domain.Models.Enum;
using NewCentury.Service.Services;
using NewCentury.ViewModels;
using NewCentury.ViewModels.Dash;
using NewCentury.ViewModels.Temp;
using System.Collections.Generic;

namespace NewCentury.Controllers
{
    public class DashController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IGameService _gameService;
        private readonly IJogadorRepository _jogadorRepository;
        private readonly IJogadorService _jogadorService;
        private readonly IPartidaService _partidaService;
        private readonly IPartidaRepository _partidaRepository;
        private readonly IRodadaRepository _rodadaRepository;
        private readonly IRodadaService _rodadaService;

        public DashController(IGameService gameService,
                                 IPartidaRepository partidaRepository,
                                 IRodadaRepository rodadaRepository,
                                 IMapper mapper, IJogadorRepository jogadorRepository,
                                 IPartidaService partidaService,
                                 IRodadaService rodadaService,
                                 IJogadorService jogadorService,
                                 INotificador notificador)
        {
            _rodadaRepository = rodadaRepository;
            _mapper = mapper;
            _partidaService = partidaService;
            _partidaRepository = partidaRepository;
            _rodadaService = rodadaService;
            _gameService = gameService;
            _jogadorService = jogadorService;
            _jogadorRepository = jogadorRepository;

        }

        public async Task<IActionResult> HistoricoTentativas(DashViewModel? filtro)
        {
            if (filtro.DataInicial == DateTime.MinValue)
            {
                filtro.DataInicial = DateTime.Today.AddDays(-2);
            }

            if (filtro.DataFinal == DateTime.MinValue)
            {
                filtro.DataFinal = DateTime.Today;
            }

            var tentativas = _mapper.Map<IEnumerable<RodadaViewModel>>(await _rodadaRepository.ObterRodadasComPartidasEJogadoresPorPeriodo(filtro.DataInicial, filtro.DataFinal));


            filtro.Tentativas = tentativas.ToList();

            return View(filtro);
        }

        public async Task<IActionResult> HistoricoPartidas(DashViewModel? filtro)
        {
            if (filtro.DataInicial == DateTime.MinValue)
            {
                filtro.DataInicial = DateTime.Today.AddDays(-2);
            }

            if (filtro.DataFinal == DateTime.MinValue)
            {
                filtro.DataFinal = DateTime.Today;
            }

            var partidas = _mapper.Map<IEnumerable<PartidaViewModel>>(await _partidaRepository.ObterPartidasComRodadasEJogadoresPorPeriodo(filtro.DataInicial, filtro.DataFinal));


            filtro.Partidas = partidas.ToList();

            return View(filtro);
        }


        public async Task<IActionResult> Ranking(DashViewModel? filtro)
        {
            if (filtro == null)
            {
                filtro = new DashViewModel(); // Certifica-se de que filtro não é nulo
            }

            if (filtro.DataInicial == DateTime.MinValue)
            {
                filtro.DataInicial = DateTime.Today.AddDays(-2);
            }

            if (filtro.DataFinal == DateTime.MinValue)
            {
                filtro.DataFinal = DateTime.Today;
            }


            var partidas = _mapper.Map<IEnumerable<PartidaViewModel>>(await _partidaRepository.ObterPartidasComRodadasEJogadoresPorPeriodo(filtro.DataInicial, filtro.DataFinal));
            var rodadasPorJogador = partidas.GroupBy(tentativa => tentativa.Jogador.Nome);


            var classifiedUsers = new List<UserClassificateViewModel>();
            foreach (var jogadorGroup in rodadasPorJogador)
            {
                var playerName = jogadorGroup.Key;
                var playerScores = new List<RankingUserViewModel>();
                var rodadasPorDificuldade = jogadorGroup.SelectMany(partida => partida.Rodadas)
                                                        .GroupBy(rodada => rodada.Partida.Dificuldade);
                foreach (var dificuldadeGroup in rodadasPorDificuldade)
                {
                    var dificuldade = dificuldadeGroup.Key;
                    var totalRodadas = dificuldadeGroup.Count();
                    var vitorias = dificuldadeGroup.Count(rodada => rodada.Resultado == Resultado.SUCCESS);
                    var taxaVitoria = totalRodadas > 0 ? (double)vitorias / totalRodadas : 0.0;
                    var classificacao = await _jogadorService.ObterClassificacao(taxaVitoria);
                    playerScores.Add(new RankingUserViewModel
                    {
                        Classification = classificacao,
                        Dificuldade = dificuldade,
                        TaxaAcertos = taxaVitoria.ToString("P0")
                    });
                }
                classifiedUsers.Add(new UserClassificateViewModel
                {
                    Name = playerName,
                    ScoreUser = playerScores
                });
            }

            var rankingViewModel = new RankingDashViewModel
            {
                User = classifiedUsers
            };
            filtro.Ranking = rankingViewModel;

            return View(filtro);
        }

    }
}
