using Teams.ApiMember.Service.Dtos;
using Teams.ApiMember.Service.Models;

namespace Teams.ApiMember.Service.Interfaces;

public interface ITeamMemberService
{
    // GetAllTeamMembers
    Task<List<TeamMember>> GetAllTeamMembersAsync();

    // GetTeamMemberById
    Task<TeamMember> GetTeamMemberByIdAsync(int id);

    // CreateTeamMember
    Task<TeamMember> CreateTeamMemberAsync(TeamMember teamMember);

    // UpdateTeamMember
    Task<TeamMember> UpdateTeamMemberAsync(TeamMember teamMember);

    // DeleteTeamMember
    Task DeleteTeamMemberAsync(int id);

    // GetTeamsByMemberId
    Task<List<TeamDto>> GetTeamsByMemberIdAsync(int memberId);
}