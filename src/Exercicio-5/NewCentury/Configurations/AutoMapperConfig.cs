using AutoMapper;
using NewCentury.Domain.Models;
using NewCentury.ViewModels;
using NewCentury.ViewModels.Dash;


namespace NewCentury.MVC.Configurations
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            //Rodada
            CreateMap<HistoricoTentativasViewModel, Rodada>().ReverseMap();          
            CreateMap<RodadaViewModel, Rodada>().ReverseMap();

            //Partida
            CreateMap<PartidaViewModel, Partida>().ReverseMap();
            CreateMap<Jogador, JogadorViewModel>().ReverseMap();
        }
    }
}