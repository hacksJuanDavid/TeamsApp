using Teams.ApiManager.Dtos;

namespace Teams.ApiManager.Interfaces;

public interface ITeamMemberConsumingService
{
    // CreateTeamMember
    Task<TeamMemberDto?> CreateTeamMemberAsync(TeamMemberDto? teamMember);
    
    // GetAllTeamMembers
    Task<object?> GetAllTeamMembersAsync();
    
    // GetTeamMemberById
    Task<object?> GetTeamMemberByIdAsync(int id);
    
    // UpdateTeamMember
    Task<TeamMemberDto?>? UpdateTeamMemberAsync(int id, TeamMemberDto? teamMember);
    
    // DeleteTeamMember
    Task<TeamMemberDto?> DeleteTeamMemberAsync(int id);
}