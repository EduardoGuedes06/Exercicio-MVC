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

        public async Task<Guid> AdicionarJogador(string nome)
        {
            var jogador = await _jogadorRepository.ObterJogadorPorNome(nome.ToUpper());
            if (jogador == null)
            {
                var novoJogador = new Jogador
                {
                    Nome = nome.ToUpper(),
                };
                await _jogadorRepository.Adicionar(novoJogador);
                return novoJogador.Id;
            }
            return jogador.Id;
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