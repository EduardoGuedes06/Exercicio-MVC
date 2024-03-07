using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NewCentury.Domain.Models
{
    public class RodadaViewModel
    {
        [Key]
        public Guid Id { get; set; }
        public string EscolhaMaquina { get; set; }
        public string EscolhaJogador { get; set; }

        public Guid PartidaId { get; set; }
        public PartidaViewModel Partida { get; set; }
    }
}