using NewCentury.Domain.Models.Enum;
using System;

namespace NewCentury.Domain.Models
{
    public class HistoricoTentativas : Entity
    {
        public Guid JogadorId { get; set; }
        public Jogador Jogador { get; set; }
        public int NumeroTentativa { get; set; }
        public Resultado Resultado { get; set; }
    }
}