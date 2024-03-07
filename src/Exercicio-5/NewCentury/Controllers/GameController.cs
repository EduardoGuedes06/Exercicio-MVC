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
        private readonly IGameService _gameService;
        private readonly IJogadorRepository _jogadorRepository;
        private readonly IJogadorService _jogadorService;
        private readonly IHistoricoTentativaRepository _historicoTentativaoRepository;
        private readonly IHistoricoTentativaService _historicoTentativaService;

        public GameController(IGameService gameService, IJogadorRepository jogadorRepository,
                                 IJogadorService jogadorService,
                                 IHistoricoTentativaRepository historicoTentativaoRepository,
                                 IHistoricoTentativaService historicoTentativaService,
                                 INotificador notificador)
        {
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

            return RedirectToAction("Jogar", jogador.Id);
        }


        public IActionResult Jogar(Guid jogadorId)
        {
            var partidaViewModel = new PartidaViewModel
            {
                JogadorId = jogadorId
            };


            return View(partidaViewModel);
        }

        [HttpPost]
        public IActionResult Jogar(PartidaViewModel partidaViewModel)
        {
            if (ModelState.IsValid)
            {
                // Aqui você pode realizar qualquer validação adicional dos dados recebidos
                // Se tudo estiver correto, você pode redirecionar para a próxima etapa

                if (partidaViewModel.QuemComeca == "jogador")
                {
                    return RedirectToAction("JogadorJoga", partidaViewModel);
                }
                else if (partidaViewModel.QuemComeca == "maquina")
                {
                    return RedirectToAction("VezMaquina", partidaViewModel);
                }
                else
                {
                    // Lógica para tratamento de erro ou escolha inválida
                    return View("Error");
                }
            }
            else
            {
                // Se o modelo não for válido, retorne a mesma view para exibir os erros
                return View(partidaViewModel);
            }
        }

        public IActionResult JogadorJoga(PartidaViewModel partidaViewModel)
        {
            return View(partidaViewModel);
        }

        public async Task<IActionResult> VezMaquina(PartidaViewModel partidaViewModel)
        {  return View(partidaViewModel);}


        //public async Task<IActionResult> VezMaquina(PartidaViewModel viewModel)
        //{

        //    var escolha = await _gameService.GerarNumeroSecreto(viewModel.Dificuldade);
        //    viewModel. = machineGuess;
        //    viewModel.GuessResult = "Sucesso! A máquina adivinhou o número.";
        //    return View(viewModel);
        //}

        //public async Task<IActionResult> MaquinaJoga(PartidaViewModel partidaViewModel)
        //{
        //    int numeroSecreto = await _gameService.GerarNumeroSecreto(partidaViewModel.Dificuldade);
        //    bool resultado = false; // Inicializar o resultado como falso

        //    // Simular a tentativa da máquina de adivinhar o número
        //    for (int tentativa = 1; tentativa <= partidaViewModel.NumeroRodadas; tentativa++)
        //    {
        //        if (numeroSecreto == tentativa) // Se a máquina acertar
        //        {
        //            resultado = true;
        //            break; // Encerrar o loop
        //        }
        //    }

        //    // Preparar mensagem de resultado para exibição na view
        //    string mensagem;
        //    if (resultado)
        //    {
        //        mensagem = "A máquina acertou!";
        //    }
        //    else
        //    {
        //        mensagem = "A máquina errou!";
        //    }

        //    // Aguardar um tempo antes de redirecionar para a tela de resultado
        //    Thread.Sleep(3000); // Espera 3 segundos (tempo para o usuário visualizar a mensagem)

        //    // Redirecionar para a tela de resultado com a mensagem
        //    return RedirectToAction("Resultado", new { mensagem = mensagem, jogadorId = partidaViewModel.JogadorId });
        //}








    }
}
