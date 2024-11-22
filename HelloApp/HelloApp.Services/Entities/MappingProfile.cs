using AutoMapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<HelloApp.Services.User, HelloApp.DataAccess.User>();
        CreateMap<HelloApp.DataAccess.User, HelloApp.Services.User>();
        CreateMap<HelloApp.Services.Device, HelloApp.DataAccess.Device>();
        CreateMap<HelloApp.DataAccess.Device, HelloApp.Services.Device>();
    }
}
