using AutoMapper;
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
        public async Task<IActionResult> RodadaJogoMaquina(SessaoAtualViewModel sessao)
        {
            sessao.EscolhaMaquina = await _gameService.GerarNumeroSecreto(sessao.Dificuldade);
            sessao.RodadaAtual = sessao.RodadaAtual + 1;
            if (sessao.EscolhaMaquina != sessao.EscolhaJogador)
                {
                    sessao.Situacao = Domain.Models.Enum.Resultado.SUCCESS;
                    await AuditoriaRodadaAsync(sessao);
                }
            else
                {
                    sessao.Situacao = Domain.Models.Enum.Resultado.WRONG;
                    await AuditoriaRodadaAsync(sessao);
                }
            if (sessao.RodadaAtual == sessao.Rodadas + 1){

                if (sessao.PlayerInicial == "jogador")
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
            if (sessao.EscolhaMaquina == sessao.EscolhaJogador)
                {
                    sessao.Situacao = Domain.Models.Enum.Resultado.SUCCESS;
                    await AuditoriaRodadaAsync(sessao);
                }
            else
                {
                    sessao.Situacao = Domain.Models.Enum.Resultado.WRONG;
                    await AuditoriaRodadaAsync(sessao);
                }
            
            if (sessao.RodadaAtual == sessao.Rodadas + 1)
            {
                if (sessao.PlayerInicial == "maquina")
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

                }


                sessao.EscolhaJogador = 0;
                sessao.EscolhaMaquina = 0;
                sessao.Player = "maquina";
                sessao.RodadaAtual = 1;
            }
            return RedirectToAction("Jogo", sessao);
        }


        public IActionResult Fim(FimDeJogoViewModel fim)
        {
            return View(fim);
        }

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
