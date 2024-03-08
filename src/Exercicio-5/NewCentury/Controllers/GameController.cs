﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NewCentury.Domain.Intefaces;
using NewCentury.Domain.Models;
using NewCentury.Service.Services;
using NewCentury.ViewModels;
using NewCentury.ViewModels.Temp;

namespace NewCentury.Controllers
{
    public class GameController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IGameService _gameService;
        private readonly IJogadorRepository _jogadorRepository;
        private readonly IJogadorService _jogadorService;
        private readonly IHistoricoTentativaRepository _historicoTentativaoRepository;
        private readonly IHistoricoTentativaService _historicoTentativaService;
        private readonly IPartidaService _partidaService;
        private readonly IRodadaService _rodadaService;

        public GameController(IGameService gameService, IMapper mapper, IJogadorRepository jogadorRepository,
                                 IPartidaService partidaService,
                                 IRodadaService rodadaService,
                                 IJogadorService jogadorService,
                                 IHistoricoTentativaRepository historicoTentativaoRepository,
                                 IHistoricoTentativaService historicoTentativaService,
                                 INotificador notificador)
        {
            _mapper = mapper;
            _partidaService = partidaService;
            _rodadaService = rodadaService;
            _gameService = gameService;
            _jogadorService = jogadorService;
            _jogadorRepository = jogadorRepository;
            _historicoTentativaoRepository = historicoTentativaoRepository;
            _historicoTentativaService = historicoTentativaService;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(string nome)
        {
            var jogador = await _jogadorRepository.ObterJogadorPorNome(nome);
            if (jogador == null)
            {
                var novoJogador = new Jogador
                {
                    Nome = nome,
                };
                await _jogadorService.Adicionar(novoJogador);
                jogador = novoJogador;
            }

            return RedirectToAction("Jogar", new { jogadorId = jogador.Id });
        }


        public async Task<IActionResult> Jogar(Guid jogadorId)
        {
            var partidaViewModel = new PartidaAtualViewModel
            {
                JogadorId = jogadorId
            };


            return View(partidaViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> JogarAsync(PartidaAtualViewModel partidaViewModel)
        {
            if (ModelState.IsValid)
            {
                var partida = new PartidaViewModel
                {
                    IdJogador = partidaViewModel.JogadorId,
                    Dificuldade = partidaViewModel.Dificuldade,
                    QuemComeca = partidaViewModel.QuemComeca,
                    numeroRodadas = partidaViewModel.NumeroRodadas,
                    Rodadas = null,
                    Vencedor = null
                };

                if (partidaViewModel.QuemComeca == "jogador" || partidaViewModel.QuemComeca == "maquina")
                {
                    var partidaDb = await _partidaService.Adicionar(_mapper.Map<Partida>(partida));
                    var rodada = new SessaoAtualViewModel
                    {
                        partidaId = partidaDb.Id,
                        Player = partidaViewModel.QuemComeca,
                        Situacao = Domain.Models.Enum.Resultado.AFK,
                        Dificuldade = partidaViewModel.Dificuldade,
                        Rodadas = partidaViewModel.NumeroRodadas,
                        PlayerInicial = partidaViewModel.QuemComeca,
                        RodadaAtual = 1
                    };
                    return RedirectToAction("Jogo", rodada);
                }
                else
                {
                    return View("Error");
                }
            }
            else
            {
                return View(partidaViewModel);
            }
        }

        public async Task<IActionResult> Jogo(SessaoAtualViewModel sessao)
        {

            return View(sessao);
        }

        [HttpPost]
        public async Task<IActionResult> RodadaJogoMaquina(SessaoAtualViewModel sessao)
        {
            sessao.EscolhaMaquina = await _gameService.GerarNumeroSecreto(sessao.Dificuldade);
            sessao.RodadaAtual = sessao.RodadaAtual + 1;


            if (sessao.RodadaAtual == sessao.Rodadas + 1){

                if(sessao.PlayerInicial == "jogador")
                {
                    var x = "x";

                    //Final
                }
                sessao.EscolhaJogador = 0;
                sessao.EscolhaMaquina = 0;
                sessao.Player = "jogador";
                sessao.RodadaAtual = 1;
            }
            else
            {

            }
            

            return RedirectToAction("Jogo", sessao);
        }






        [HttpPost]
        public async Task<IActionResult> RodadaJogoJogador(SessaoAtualViewModel sessao)
        {
            sessao.EscolhaMaquina = await _gameService.GerarNumeroSecreto(sessao.Dificuldade);
            sessao.RodadaAtual = sessao.RodadaAtual + 1;


            if (sessao.RodadaAtual == sessao.Rodadas + 1)
            {
                if (sessao.PlayerInicial == "maquinha")
                {
                    var x = "x";

                    //Final
                }
                sessao.EscolhaJogador = 0;
                sessao.EscolhaMaquina = 0;
                sessao.Player = "maquina";
                sessao.RodadaAtual = 1;
            }


            return RedirectToAction("Jogo", sessao);
        }









    }
}
