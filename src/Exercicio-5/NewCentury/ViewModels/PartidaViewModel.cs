using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewCentury.Domain.Models
{
    public class PartidaViewModel
    {
        [Key]
        public Guid Id { get; set; }
        public Guid IdJogador { get; set; }
        public string Dificuldade { get; set; }
        public string QuemComeca { get; set; }
        public string? Vencedor { get; set; }
        public int numeroRodadas { get; set; }
        public Jogador Jogador { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime DataFim { get; set; }
        public IEnumerable<RodadaViewModel>? Rodadas  { get; set; }
    }
}
