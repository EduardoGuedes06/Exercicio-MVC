﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NewCentury.Data.Repository;
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
        private readonly IPartidaService _partidaService;
        private readonly IPartidaRepository _partidaRepository;
        private readonly IRodadaRepository _rodadaRepository;
        private readonly IRodadaService _rodadaService;

        public GameController(IGameService gameService,
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

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(string nome)
        {
            var jogador = await _jogadorRepository.ObterJogadorPorNome(nome.ToUpper());
            if (jogador == null)
            {
                var novoJogador = new Jogador
                {
                    Nome = nome.ToUpper(),
                };
                await _jogadorService.Adicionar(novoJogador);
                jogador = novoJogador;
            }

            return RedirectToAction("Jogar", new { jogadorId = jogador.Id });
        }

        //Verifica Jogador
        public async Task<IActionResult> Jogar(Guid jogadorId)
        {
            var partidaViewModel = new PartidaAtualViewModel
            {
                JogadorId = jogadorId
            };


            return View(partidaViewModel);
        }

        //Formulario Começo de Jogo
        [HttpPost]
        public async Task<IActionResult> JogarAsync(PartidaAtualViewModel partidaViewModel)
        {
            if (ModelState.IsValid)
            {
                var jogador = await _jogadorRepository.ObterPorId(partidaViewModel.JogadorId);

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
                        NomeJogador = jogador.Nome,
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
        public async Task<IActionResult> RodadaJogoJogador(SessaoAtualViewModel sessao)
        {
            sessao.EscolhaMaquina = await _gameService.GerarNumeroSecreto(sessao.Dificuldade);
            sessao.RodadaAtual = sessao.RodadaAtual + 1;
            sessao.RodadaAntiga = sessao.RodadaAtual - 1;


            if (sessao.RodadaAtual == 1) { sessao.MostrarModal = false; } else { sessao.MostrarModal = true; }


            if (sessao.EscolhaMaquina == sessao.EscolhaJogador)
            {
                if (sessao.PlayerInicial == "jogador") { sessao.Situacao = Domain.Models.Enum.Resultado.SUCCESS; sessao.QtdeJogador = sessao.QtdeJogador + 1; sessao.VencedorRodadaAtual = "jogador"; } else { sessao.Situacao = Domain.Models.Enum.Resultado.WRONG; sessao.QtdeMaquina = sessao.QtdeMaquina + 1; sessao.VencedorRodadaAtual = "maquina"; }                   
            }
            else
            {
                if (sessao.PlayerInicial == "jogador") { sessao.Situacao = Domain.Models.Enum.Resultado.WRONG; sessao.QtdeMaquina = sessao.QtdeMaquina + 1; sessao.VencedorRodadaAtual = "maquina"; } else { sessao.Situacao = Domain.Models.Enum.Resultado.SUCCESS; sessao.QtdeJogador = sessao.QtdeJogador + 1; sessao.VencedorRodadaAtual = "jogador"; }
            }

            await AuditoriaRodadaAsync(sessao);


            if (sessao.RodadaAtual == sessao.Rodadas + 1)
            { 
                sessao.loop = sessao.loop + 1;
                if(sessao.loop == 2)
                {
                    await _partidaService.AtualizarVencedor(sessao.partidaId);

                    var dadosPartida = new FimDeJogoViewModel
                    {
                        NomeJogador = sessao.NomeJogador,
                        Vencedor = await _partidaService.CalcularVencedor(sessao.partidaId),
                        QtdeJogador = await _partidaRepository.ContarResultadosPorVencedorUsuario(sessao.partidaId),
                        QtdeMaquina = await _partidaRepository.ContarResultadosPorVencedorMaquina(sessao.partidaId),
                        DuracaoPartida = await _rodadaRepository.CalcularDiferencaDatasEmMinutosPorPartida(sessao.partidaId)
                    };


                    return RedirectToAction("Fim", dadosPartida);
                };
                if(sessao.PlayerInicial == "maquina") { sessao.Player = "jogador"; }
                if(sessao.PlayerInicial == "jogador") { sessao.Player = "maquina"; }
                sessao.EscolhaJogador = 0;
                sessao.EscolhaMaquina = 0;
                sessao.RodadaAtual = 1;
            }



            return RedirectToAction("Jogo", sessao);
        }

        //[HttpPost]
        //public async Task<IActionResult> RodadaJogoJogador(SessaoAtualViewModel sessao)
        //{
        //    sessao.EscolhaMaquina = await _gameService.GerarNumeroSecreto(sessao.Dificuldade);
        //    sessao.RodadaAtual = sessao.RodadaAtual + 1;
        //    if (sessao.EscolhaMaquina == sessao.EscolhaJogador)
        //        {
        //            sessao.Situacao = Domain.Models.Enum.Resultado.SUCCESS;
        //            await AuditoriaRodadaAsync(sessao);
        //        }
        //    else
        //        {
        //            sessao.Situacao = Domain.Models.Enum.Resultado.WRONG;
        //            await AuditoriaRodadaAsync(sessao);
        //        }

        //    if (sessao.RodadaAtual == sessao.Rodadas + 1)
        //    {
        //        if (sessao.PlayerInicial == "maquina")
        //        {
        //            await _partidaService.AtualizarVencedor(sessao.partidaId);

        //            var dadosPartida = new FimDeJogoViewModel
        //            {
        //                NomeJogador = sessao.NomeJogador,
        //                Vencedor = await _partidaService.CalcularVencedor(sessao.partidaId),
        //                QtdeJogador = await _partidaRepository.ContarResultadosPorVencedorUsuario(sessao.partidaId),
        //                QtdeMaquina = await _partidaRepository.ContarResultadosPorVencedorMaquina(sessao.partidaId),
        //                DuracaoPartida = await _rodadaRepository.CalcularDiferencaDatasEmMinutosPorPartida(sessao.partidaId)
        //            };


        //            return RedirectToAction("Fim", dadosPartida);

        //        }


        //        sessao.EscolhaJogador = 0;
        //        sessao.EscolhaMaquina = 0;
        //        sessao.Player = "maquina";
        //        sessao.RodadaAtual = 1;
        //    }
        //    return RedirectToAction("Jogo", sessao);
        //}

        //Placar da Partida
        public IActionResult Fim(FimDeJogoViewModel fim)
        {
            return View(fim);
        }

        //Auditoria dos dados da partida
        public async Task AuditoriaRodadaAsync(SessaoAtualViewModel sessao)
        {
            var rodadaAtual = new RodadaViewModel
            {
                PartidaId = sessao.partidaId,
                EscolhaJogador = sessao.EscolhaJogador,
                EscolhaMaquina = sessao.EscolhaMaquina,
                Resultado = sessao.Situacao,
                Player = sessao.Player
            };
            await _rodadaService.Adicionar(_mapper.Map<Rodada>(rodadaAtual));

        }


    }
}
