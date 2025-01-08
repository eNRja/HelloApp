using AutoMapper;
using HelloApp.Services.Models;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, HelloApp.DataAccess.DbUser>();
        CreateMap<HelloApp.DataAccess.DbUser, User>();
        CreateMap<Device, HelloApp.DataAccess.DbDevice>();
        CreateMap<HelloApp.DataAccess.DbDevice, Device>();
    }
}
