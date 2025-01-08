using AutoMapper;
using HelloApp.MeteoHandler.Entities.Messages.Responses;
using HelloApp.Services.Entities;
using HelloApp.Services.Entities.Messages;

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
