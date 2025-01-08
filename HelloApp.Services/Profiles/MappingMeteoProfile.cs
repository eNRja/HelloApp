using AutoMapper;
using HelloApp.MeteoHandler.Entities.Messages.Requests;
using HelloApp.MeteoHandler.Entities.Messages.Responses;
using HelloApp.Services.Entities.Messages.Requests;
using HelloApp.Services.Entities.Messages.Responses;

public class MappingMeteoProfile : Profile
{
    public MappingMeteoProfile()
    {
        // Маппинг для MeteoLocationRequest в LocationRequest
        CreateMap<MeteoLocationRequest, LocationRequest>()
            .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.Location));

        // Маппинг для WeatherResponse в MeteoModel
        CreateMap<WeatherResponse, MeteoModel>()
            .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City))
            .ForMember(dest => dest.Temperature, opt => opt.MapFrom(src => src.Temperature));
    }
}
