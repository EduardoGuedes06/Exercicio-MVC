namespace NewCentury.ViewModels.Dash
{
    public class DashViewModel
    {
        public DateTime DataInicial { get; set; }
        public DateTime DataFinal { get; set; }
        public string FiltroHistoricoNomeJogador { get; set; }
        public string FiltroRankingNomeJogador { get; set; }

        public string DificuldadePartida { get; set; }

        public Guid IdJogador { get; set; }
        public Guid IdPartida { get; set; }


    }
}
