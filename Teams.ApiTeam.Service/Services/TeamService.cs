using Microsoft.EntityFrameworkCore;
using Teams.ApiTeam.Service.Context;
using Teams.ApiTeam.Service.Exceptions;
using Teams.ApiTeam.Service.Interfaces;
using Teams.ApiTeam.Service.Models;

namespace Teams.ApiTeam.Service.Services;

public class TeamService : ITeamService
{   
    // Variables
    private readonly AppDbContext _appDbContext;
    
    // Constructor
    public TeamService(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }
    
    public async Task<List<Team>> GetAllTeamsAsync()
    {
        return await _appDbContext.Set<Team>().ToListAsync();
    }

    public async Task<Team> GetTeamByIdAsync(int id)
    {
        var team = await _appDbContext.Set<Team>().FindAsync(id);
        if (team == null)
        {
            throw new NotFoundException($"Team with id {id} not found");
        }
        
        return team;
    }

    public async Task<Team> CreateTeamAsync(Team team)
    {
        var createdTeam = await _appDbContext.Set<Team>().AddAsync(team);
        await _appDbContext.SaveChangesAsync();
        return createdTeam.Entity;
    }

    public async Task<Team> UpdateTeamAsync(Team team)
    {
        var original = await _appDbContext.Set<Team>().FindAsync(team.Id);
        
        // Check if team exists
        if (original is null)
        {
            throw new NotFoundException($"Team with Id={team.Id} Not Found");
        }
        
        // Check if team has a team
        _appDbContext.Entry(original).CurrentValues.SetValues(team);
        
        // Save changes
        await _appDbContext.SaveChangesAsync();
        
        return team;
    }

    public async Task DeleteTeamAsync(int id)
    {
        var original = await _appDbContext.Set<Team>().FindAsync(id);
        
        // Check if team exists
        if (original is null)
        {
            throw new NotFoundException($"Team with Id={id} Not Found");
        }
        
        // Delete team
        _appDbContext.Set<Team>().Remove(original);
        
        // Save changes
        await _appDbContext.SaveChangesAsync();
    }
    
    // Get /teams/{teamId}/members
}