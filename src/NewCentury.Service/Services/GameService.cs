using NewCentury.Domain.Intefaces;
using NewCentury.Domain.Models;

namespace NewCentury.Service.Services
{
    public class GameService : BaseService, IGameService
    {
        private readonly IJogadorRepository _jogadorRepository;
        private readonly IJogadorService _jogadorService;
        public GameService(IJogadorRepository jogadorRepository,
                                 IJogadorService jogadorService,
                                 INotificador notificador) : base(notificador)
        {
            _jogadorService = jogadorService;
            _jogadorRepository = jogadorRepository;
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
                case "FACIL":
                    return random.Next(1, 3);
                case "MEDIO":
                    return random.Next(1, 6);
                case "DIFICIL":
                    return random.Next(1, 11);
                default:
                    throw new ArgumentException("Dificuldade inválida");
            }
        }



    }
}