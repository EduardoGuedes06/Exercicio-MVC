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
            CreateMap<RodadaViewModel, Rodada>().ReverseMap();
            CreateMap<PartidaViewModel, Partida>().ReverseMap();
            CreateMap<Jogador, JogadorViewModel>().ReverseMap();
        }
    }
}