namespace NewCentury.ViewModels
{
    public class GameHistoricoViewModel
    {
        public int JogadorId { get; set; }
        public string Nome { get; set; }
        public IEnumerable<HistoricoTentativasViewModel> Historico { get; set; }
    }
}
