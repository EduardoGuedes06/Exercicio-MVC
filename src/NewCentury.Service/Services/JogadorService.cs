using NewCentury.Domain.Intefaces;
using NewCentury.Domain.Models;

namespace NewCentury.Service.Services
{
    public class JogadorService : BaseService, IJogadorService
    {
        private readonly IJogadorRepository _jogadorRepository;
        public JogadorService(IJogadorRepository jogadorRepository,
                                 INotificador notificador) : base(notificador)
        {
            _jogadorRepository = jogadorRepository;
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

        public async Task<string> ObterClassificacao(double taxaVitoria)
        {
            if (taxaVitoria >= 0.9)
            {
                return "S";
            }
            else if (taxaVitoria >= 0.8)
            {
                return "A";
            }
            else if (taxaVitoria >= 0.7)
            {
                return "B";
            }
            else if (taxaVitoria >= 0.6)
            {
                return "C";
            }
            else if (taxaVitoria >= 0.5)
            {
                return "D";
            }
            else
            {
                return "E";
            }
        }

        public void Dispose()
        {
            _jogadorRepository?.Dispose();
        }
    }
}