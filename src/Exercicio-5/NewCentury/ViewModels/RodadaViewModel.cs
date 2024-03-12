using NewCentury.Domain.Models.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NewCentury.Domain.Models
{
    public class RodadaViewModel
    {
        [Key]
        public Guid Id { get; set; }
        public Guid PartidaId { get; set; }
        public string Jogador { get; set; }
        public int EscolhaMaquina { get; set; }
        public int EscolhaJogador { get; set; }
        public Resultado Resultado { get; set; }
        public DateTime DataCadastro { get; set; }
        public string Player { get; set; }
        public PartidaViewModel Partida { get; set; }
    }
}