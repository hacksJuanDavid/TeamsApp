using Microsoft.EntityFrameworkCore;
using Teams.ApiTeam.Service.Context;
using Teams.ApiTeam.Service.Interfaces;
using Teams.ApiTeam.Service.Models;

namespace Teams.ApiTeam.Service.Repositories;

public class TeamRepository : ITeamRepository
{
    
    // Variables
    private readonly AppDbContext _appDbContext;
    
    // Constructor
    public TeamRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }
    
    // GetAllTeams
    public async Task<List<Team>> GetAllTeamsAsync()
    {
        return await _appDbContext.Set<Team>().ToListAsync();
    }

    // GetTeamById
    public async Task<Team?> GetTeamByIdAsync(int id)
    {
        return await _appDbContext.Set<Team>().FindAsync(id);
    }
    
    // CreateTeam
    public async Task<Team> CreateTeamAsync(Team team)
    {
        var createdTeam = await _appDbContext.Set<Team>().AddAsync(team);
        await _appDbContext.SaveChangesAsync();
        return createdTeam.Entity;
    }

    // UpdateTeam
    public async Task<Team> UpdateTeamAsync(Team team)
    {
        var updatedTeam = _appDbContext.Set<Team>().Update(team);
        await _appDbContext.SaveChangesAsync();
        return updatedTeam.Entity;
    }

    // DeleteTeam
    public async Task DeleteTeamAsync(int id)
    {
        var team = await _appDbContext.Set<Team>().FindAsync(id);
        if (team != null) _appDbContext.Set<Team>().Remove(team);
        await _appDbContext.SaveChangesAsync();
    }
    
}