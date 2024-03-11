using NewCentury.Domain.Models;
using NewCentury.Domain.Models.Enum;
using System.ComponentModel.DataAnnotations;

namespace NewCentury.ViewModels.Dash
{
    public class HistoricoTentativasViewModel
    {
        [Key]
        public Guid id { get; set; }
        public Guid PartidaId { get; set; }
        public string Jogador { get; set; }
        public int EscolhaMaquina { get; set; }
        public int EscolhaJogador { get; set; }
        public DateTime DataCadastro { get; set; }
        public string Player { get; set; }

        public Partida Partida { get; set; }
        public Resultado Resultado { get; set; }
    }
}
