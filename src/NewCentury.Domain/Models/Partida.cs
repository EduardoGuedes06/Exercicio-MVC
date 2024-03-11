using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewCentury.Domain.Models
{
    public class Partida : Entity
    {
        public Guid IdJogador { get; set; }
        public string Dificuldade { get; set; }
        public string QuemComeca { get; set; }
        public string ? Vencedor { get; set; }
        public int numeroRodadas { get; set; }
        public IEnumerable<Rodada>? Rodadas { get; set; }

        public Jogador Jogador { get; set; }
    }
}
