using AutoMapper;
using NewCentury.Domain.Models;
using NewCentury.ViewModels;


namespace NewCentury.MVC.Configurations
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Jogador, JogadorViewModel>().ReverseMap();
            CreateMap<HistoricoTentativas, HistoricoTentativasViewModel>().ReverseMap();
        }
    }
}