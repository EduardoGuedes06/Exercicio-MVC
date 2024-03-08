using System.ComponentModel.DataAnnotations;

namespace NewCentury.ViewModels
{
    public class JogadorViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public int Vitorias { get; set; }
        public int Derrotas { get; set; }
        public string Classificacao { get; set; }

        public double TaxaVitoria { get; set; }
    }
}
