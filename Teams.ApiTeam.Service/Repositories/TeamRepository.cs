using Teams.ApiTeam.Service.Dtos;
using Teams.ApiTeam.Service.Interfaces;
using Teams.ApiTeam.Service.Models;

namespace Teams.ApiTeam.Service.Repositories;

public class TeamRepository : ITeamRepository
{
    // Variables
    private readonly ITeamService _teamService;

    // Constructor
    public TeamRepository(ITeamService teamService)
    {
        _teamService = teamService;
    }

    // Methods
    public async Task<List<Team>> GetAllTeamsAsync()
    {
        return await _teamService.GetAllTeamsAsync();
    }

    public async Task<Team> GetTeamByIdAsync(int id)
    {
        return await _teamService.GetTeamByIdAsync(id);
    }

    public async Task<Team> CreateTeamAsync(Team team)
    {
        return await _teamService.CreateTeamAsync(team);
    }

    public async Task<Team> UpdateTeamAsync(Team team)
    {
        return await _teamService.UpdateTeamAsync(team);
    }

    public async Task DeleteTeamAsync(int id)
    {
        await _teamService.DeleteTeamAsync(id);
    }
    
    public async Task<List<TeamMemberDto>> GetTeamMembersByTeamIdAsync(int teamId)
    {
        return await _teamService.GetTeamMembersByTeamIdAsync(teamId);
    }
}