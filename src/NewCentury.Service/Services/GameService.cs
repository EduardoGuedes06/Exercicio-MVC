using NewCentury.Domain.Intefaces;
using NewCentury.Domain.Models;

namespace NewCentury.Service.Services
{
    public class GameService : BaseService, IGameService
    {
        private readonly IJogadorRepository _jogadorRepository;
        private readonly IJogadorService _jogadorService;
        private readonly IHistoricoTentativaRepository _historicoTentativaoRepository;
        private readonly IHistoricoTentativaService _historicoTentativaService;
        public GameService(IJogadorRepository jogadorRepository,
                                 IJogadorService jogadorService,
                                 IHistoricoTentativaRepository historicoTentativaoRepository,
                                 IHistoricoTentativaService historicoTentativaService,
                                 INotificador notificador) : base(notificador)
        {
            _jogadorService = jogadorService;
            _jogadorRepository = jogadorRepository;
            _historicoTentativaoRepository = historicoTentativaoRepository;
            _historicoTentativaService = historicoTentativaService;
        }

        public void Dispose()
        {
            _jogadorRepository?.Dispose();
        }

        public async Task<int> GerarNumeroSecreto(string dificuldade)
        {
            Random random = new Random();
            switch (dificuldade)
            {
                case "facil":
                    return random.Next(1, 3);
                case "medio":
                    return random.Next(1, 6);
                case "dificil":
                    return random.Next(1, 11);
                default:
                    throw new ArgumentException("Dificuldade inválida");
            }
        }



    }
}