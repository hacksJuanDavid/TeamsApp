using Teams.ApiManager.Dtos;

namespace Teams.ApiManager.Interfaces;

public interface ITeamConsumingService
{
    // CreateTeam
    Task<TeamDto?> CreateTeamAsync(TeamDto? team);
    
    // GetAllTeams
    Task<object?> GetAllTeamsAsync();
    
    // GetTeamById
    Task<object?> GetTeamByIdAsync(int id);
    
    // UpdateTeam
    Task<TeamDto?>? UpdateTeamAsync(int id, TeamDto? team);
    
    // DeleteTeam
    Task<TeamDto?> DeleteTeamAsync(int id);
    
    // GetTeamMembersByTeamId
    Task<List<TeamMemberDto>?> GetTeamMembersByTeamIdAsync(int teamId);
}