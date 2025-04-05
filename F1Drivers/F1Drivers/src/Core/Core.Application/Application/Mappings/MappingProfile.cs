using AutoMapper;
using Hahn.Application.DTOs;
using Hahn.Domain.Entities;

namespace Hahn.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Driver, DriverDto>().ReverseMap();
    }
}