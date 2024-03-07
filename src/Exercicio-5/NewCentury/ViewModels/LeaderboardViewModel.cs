namespace NewCentury.ViewModels
{
    public class LeaderboardViewModel
    {
        public IEnumerable<JogadorViewModel> Jogadores { get; set; }
        public JogadorViewModel CurrentPlayer { get; set; }
    }
}
