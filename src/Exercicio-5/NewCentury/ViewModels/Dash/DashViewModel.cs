namespace NewCentury.ViewModels.Dash
{
    public class DashViewModel
    {
        public DateTime DataInicial { get; set; }
        public DateTime DataFinal { get; set; }
        public IEnumerable<HistoricoTentativasViewModel>? Tentativas { get; set; }

    }
}
