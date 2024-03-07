using NewCentury.Domain.Intefaces;
using NewCentury.Domain.Models;

namespace NewCentury.Service.Services
{
    public class RodadaService : BaseService, IRodadaService
    {
        private readonly IRodadaRepository _RodadaRepository;
        private readonly IHistoricoTentativaRepository _historicoTentativaoRepository;
        public RodadaService(IRodadaRepository RodadaRepository,
                                 IHistoricoTentativaRepository historicoTentativaoRepository,
                                 INotificador notificador) : base(notificador)
        {
            _RodadaRepository = RodadaRepository;
            _historicoTentativaoRepository = historicoTentativaoRepository;
        }

  

        public async Task Adicionar(Rodada Rodada)
        {

            await _RodadaRepository.Adicionar(Rodada);
        }

        public async Task Atualizar(Rodada Rodada)
        {

            await _RodadaRepository.Atualizar(Rodada);
        }


        public async Task Remover(Guid id)
        {

            await _RodadaRepository.Remover(id);
        }

        public void Dispose()
        {
            _RodadaRepository?.Dispose();
        }
    }
}