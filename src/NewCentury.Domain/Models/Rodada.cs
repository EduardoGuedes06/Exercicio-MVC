using System;
using System.Collections.Generic;

namespace NewCentury.Domain.Models
{
    public class Rodada : Entity
    {
        public string EscolhaMaquina { get; set; }
        public string EscolhaJogador { get; set; }

        public Guid PartidaId { get; set; }
        public Partida Partida { get; set; }
    }
}