using Teams.ApiTeam.Service.Dtos;
using Teams.ApiTeam.Service.Models;

namespace Teams.ApiTeam.Service.Interfaces;

public interface ITeamRepository
{
    // GetAllTeams
    Task<List<Team>> GetAllTeamsAsync();
    
    // GetTeamById
    Task<Team?> GetTeamByIdAsync(int id);
    
    // CreateTeam
    Task<Team> CreateTeamAsync(Team team);
    
    // UpdateTeam
    Task<Team> UpdateTeamAsync(Team team);
    
    // DeleteTeam
    Task DeleteTeamAsync(int id);
}