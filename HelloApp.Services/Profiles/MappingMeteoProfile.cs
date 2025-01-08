using AutoMapper;
using HelloApp.MeteoHandler.Messages.Responses;
using HelloApp.Services.Models;

public class MappingMeteoProfile : Profile
{
    public MappingMeteoProfile()
    {
        // Маппинг ответа API в бизнес-модель
        CreateMap<WeatherResponse, MeteoModel>()
            .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City))
            .ForMember(dest => dest.Temperature, opt => opt.MapFrom(src => src.Temperature));
    }
}
