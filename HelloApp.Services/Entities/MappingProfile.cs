using AutoMapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<HelloApp.Services.User, HelloApp.DataAccess.DbUser>();
        CreateMap<HelloApp.DataAccess.DbUser, HelloApp.Services.User>();
        CreateMap<HelloApp.Services.Device, HelloApp.DataAccess.DbDevice>();
        CreateMap<HelloApp.DataAccess.DbDevice, HelloApp.Services.Device>();
    }
}
