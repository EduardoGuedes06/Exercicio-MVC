namespace NewCentury.ViewModels.Dash
{
    public class RankingDashViewModel
    {
        public List<UserClassificateViewModel>? User { get; set; }
    }


    public class UserClassificateViewModel 
    {
        public string Name { get; set; }
        public IEnumerable<RankingUserViewModel> ScoreUser { get; set; }

    }


    public class RankingUserViewModel
    {
        public string Classification { get; set; }
        public string Dificuldade { get; set; }
        public string TaxaAcertos { get; set; }

    }

}
