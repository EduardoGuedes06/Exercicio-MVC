using NewCentury.Domain.Intefaces;
using NewCentury.Domain.Models;

namespace NewCentury.Service.Services
{
    public class HistoricoTentativaService : BaseService, IHistoricoTentativaService
    {
        private readonly IJogadorRepository _jogadorRepository;
        private readonly IHistoricoTentativaRepository _historicoTentativaoRepository;
        public HistoricoTentativaService(IJogadorRepository jogadorRepository,
                                 IHistoricoTentativaRepository historicoTentativaoRepository,
                                 INotificador notificador) : base(notificador)
        {
            _jogadorRepository = jogadorRepository;
            _historicoTentativaoRepository = historicoTentativaoRepository;
        }

        public void Dispose()
        {
            _historicoTentativaoRepository?.Dispose();
        }
    }
}