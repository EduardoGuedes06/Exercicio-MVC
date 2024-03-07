using AutoMapper;
using NewCentury.Domain.Models;
using NewCentury.ViewModels;


namespace NewCentury.MVC.Configurations
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<PartidaViewModel, Partida>().ReverseMap();
            CreateMap<RodadaViewModel, Rodada>().ReverseMap();
            CreateMap<Jogador, JogadorViewModel>().ReverseMap();
            CreateMap<HistoricoTentativas, HistoricoTentativasViewModel>().ReverseMap();
        }
    }
}