using NewCentury.Domain.Intefaces;
using NewCentury.Domain.Models;

namespace NewCentury.Service.Services
{
    public class PartidaService : BaseService, IPartidaService
    {
        private readonly IPartidaRepository _PartidaRepository;
        private readonly IHistoricoTentativaRepository _historicoTentativaoRepository;
        public PartidaService(IPartidaRepository PartidaRepository,
                                 IHistoricoTentativaRepository historicoTentativaoRepository,
                                 INotificador notificador) : base(notificador)
        {
            _PartidaRepository = PartidaRepository;
            _historicoTentativaoRepository = historicoTentativaoRepository;
        }



        public async Task<Partida> Adicionar(Partida partida)
        {
            await _PartidaRepository.Adicionar(partida);
            return partida;
        }

        public async Task Atualizar(Partida Partida)
        {

            await _PartidaRepository.Atualizar(Partida);
        }


        public async Task Remover(Guid id)
        {

            await _PartidaRepository.Remover(id);
        }

        public void Dispose()
        {
            _PartidaRepository?.Dispose();
        }
    }
}