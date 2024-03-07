using NewCentury.Domain.Intefaces;
using NewCentury.Domain.Models;

namespace NewCentury.Service.Services
{
    public class JogadorService : BaseService, IJogadorService
    {
        private readonly IJogadorRepository _jogadorRepository;
        private readonly IHistoricoTentativaRepository _historicoTentativaoRepository;
        public JogadorService(IJogadorRepository jogadorRepository,
                                 IHistoricoTentativaRepository historicoTentativaoRepository,
                                 INotificador notificador) : base(notificador)
        {
            _jogadorRepository = jogadorRepository;
            _historicoTentativaoRepository = historicoTentativaoRepository;
        }

  

        public async Task Adicionar(Jogador jogador)
        {
            if (_jogadorRepository.Buscar(f => f.Nome == jogador.Nome).Result.Any())
            {
                Notificar("Já existe um jogador com este nome infomado.");
                return;
            }

            await _jogadorRepository.Adicionar(jogador);
        }

        public async Task Atualizar(Jogador jogador)
        {
            if (_jogadorRepository.Buscar(f => f.Nome == jogador.Nome).Result.Any())
            {
                Notificar("Já existe um jogador com este nome infomado.");
                return;
            }

            await _jogadorRepository.Atualizar(jogador);
        }


        public async Task Remover(Guid id)
        {

            await _jogadorRepository.Remover(id);
        }

        public void Dispose()
        {
            _jogadorRepository?.Dispose();
        }
    }
}