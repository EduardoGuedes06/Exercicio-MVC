using NewCentury.Domain.Models.Enum;
using System;
using System.Collections.Generic;

namespace NewCentury.Domain.Models
{
    public class Rodada : Entity
    {
        public int EscolhaMaquina { get; set; }
        public int EscolhaJogador { get; set; }
        public string Player { get; set; }
        public Guid PartidaId { get; set; }
        public Partida Partida { get; set; }
        public Resultado Resultado { get; set; }
        public DateTime DataCadastro { get; set; }
    }
}