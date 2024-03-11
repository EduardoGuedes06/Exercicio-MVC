using System;
using System.Collections.Generic;

namespace NewCentury.Domain.Models
{
    public class Jogador : Entity
    {
        public string Nome { get; set; }

        public ICollection<Partida> Partidas { get; set; }

    }
}