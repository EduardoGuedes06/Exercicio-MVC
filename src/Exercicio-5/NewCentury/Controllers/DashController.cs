using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NewCentury.Data.Repository;
using NewCentury.Domain.Intefaces;
using NewCentury.Domain.Models;
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
        private readonly IHistoricoTentativaRepository _historicoTentativaoRepository;
        private readonly IHistoricoTentativaService _historicoTentativaService;
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
                                 IHistoricoTentativaRepository historicoTentativaoRepository,
                                 IHistoricoTentativaService historicoTentativaService,
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
            _historicoTentativaoRepository = historicoTentativaoRepository;
            _historicoTentativaService = historicoTentativaService;
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

        public async Task<IActionResult> HistoricoPartidasAsync(DashViewModel? filtro)
        {
            if (filtro.DataInicial == DateTime.MinValue)
            {
                filtro.DataInicial = DateTime.Today.AddDays(-2);
            }

            if (filtro.DataFinal == DateTime.MinValue)
            {
                filtro.DataFinal = DateTime.Today.AddDays(-1);
            }

            var partidas = _mapper.Map<IEnumerable<PartidaViewModel>>(await _partidaRepository.ObterPartidasComRodadasEJogadoresPorPeriodo(filtro.DataInicial, filtro.DataFinal));


            filtro.Partidas = partidas.ToList();

            return View(filtro);
        }


        public IActionResult Ranking(DashViewModel? filtro)
        {
            if (filtro.DataInicial == DateTime.MinValue)
            {
                filtro.DataInicial = DateTime.Today.AddDays(-2);
            }

            if (filtro.DataFinal == DateTime.MinValue)
            {
                filtro.DataFinal = DateTime.Today.AddDays(-1);
            }

            return View(filtro);
        }

        public IActionResult FiltrarRanking(DashViewModel filtro)
        {
            ViewBag.Filtro = filtro;
            return View("Ranking", filtro);
        }


    }
}
