using AutoMapper;
using Teams.ApiTeam.Service.Dtos;
using Teams.ApiTeam.Service.Models;

namespace Teams.ApiTeam.Service.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<TeamDto, Team>();
        CreateMap<Team, TeamDto>();
    }
}