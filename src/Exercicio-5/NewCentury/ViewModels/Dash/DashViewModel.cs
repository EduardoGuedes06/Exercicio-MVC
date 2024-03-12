using NewCentury.Domain.Models;

namespace NewCentury.ViewModels.Dash
{
    public class DashViewModel
    {
        public DateTime DataInicial { get; set; }
        public DateTime DataFinal { get; set; }
        public IEnumerable<RodadaViewModel>? Tentativas { get; set; }
        public IEnumerable<PartidaViewModel>? Partidas { get; set; }

    }
}
