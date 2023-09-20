using AutoMapper;
using Teams.ApiMember.Service.Dtos;
using Teams.ApiMember.Service.Models;

namespace Teams.ApiMember.Service.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<TeamMemberDto, TeamMember>();
        CreateMap<TeamMember, TeamMemberDto>();
    }
}