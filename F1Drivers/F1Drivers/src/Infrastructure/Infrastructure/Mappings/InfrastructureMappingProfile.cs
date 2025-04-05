using AutoMapper;
using Hahn.Application.DTOs;
using Hahn.Domain.Entities;

namespace Hahn.Infrastructure.Mappings;

public class InfrastructureMappingProfile : Profile
{
    public InfrastructureMappingProfile()
    {
        CreateMap<DriverDto, Driver>();
    }
}
