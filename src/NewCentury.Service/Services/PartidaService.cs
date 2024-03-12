using NewCentury.Domain.Intefaces;
using NewCentury.Domain.Models;

namespace NewCentury.Service.Services
{
    public class PartidaService : BaseService, IPartidaService
    {
        private readonly IPartidaRepository _PartidaRepository;
        private readonly IRodadaRepository _rodadaRepository;
        public PartidaService(IPartidaRepository PartidaRepository, IRodadaRepository rodadaRepository,
                                 INotificador notificador) : base(notificador)
        {
            _rodadaRepository = rodadaRepository;
            _PartidaRepository = PartidaRepository;
        }



        public async Task<Partida> Adicionar(Partida partida)
        {
            await _PartidaRepository.Adicionar(partida);
            return partida;
        }



        public async Task<Partida> ObterPorId(Guid id)
        {
            var partida = await _PartidaRepository.ObterPorId(id);
            return partida;
        }

        public async Task Atualizar(Partida Partida)
        {

            await _PartidaRepository.Atualizar(Partida);
        }

        public async Task AtualizarVencedor(Guid id)
        {

            Partida partida = await ObterPorId(id);


            var rodadas = await _rodadaRepository.ObterRodadasPorIdDaPartida(id);
            var vencedor = "";

            var countJ = 0;
            var countM = 0;

            foreach (var rodada in rodadas)
            {
                var resultado = rodada.Resultado;
                if(resultado == Domain.Models.Enum.Resultado.SUCCESS) { countJ++; }
                else { countM++; }
            }
            if(countJ > countM) { vencedor = "jogador"; }
            if(countJ < countM) { vencedor = "maquina"; }
            else if(countJ == countM) { vencedor = "empate"; }

            partida.Vencedor = vencedor;
            partida.DataFim = DateTime.Now;
            await _PartidaRepository.Atualizar(partida);
        }

        public async Task<string> CalcularVencedor(Guid id)
        {
            var rodadas = await _rodadaRepository.ObterRodadasPorIdDaPartida(id);
            var countJ = 0;
            var countM = 0;

            foreach (var rodada in rodadas)
            {
                var resultado = rodada.Resultado;
                if (resultado == Domain.Models.Enum.Resultado.SUCCESS)
                {
                    countJ++;
                }
                else
                {
                    countM++;
                }
            }

            if (countJ > countM)
            {
                return "jogador";
            }
            else if (countJ < countM)
            {
                return "maquina";
            }
            else
            {
                return "empate";
            }
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