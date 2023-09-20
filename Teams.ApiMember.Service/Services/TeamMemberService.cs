using Microsoft.EntityFrameworkCore;
using Teams.ApiMember.Service.Context;
using Teams.ApiMember.Service.Exceptions;
using Teams.ApiMember.Service.Interfaces;
using Teams.ApiMember.Service.Models;

namespace Teams.ApiMember.Service.Services;

public class TeamMemberService : ITeamMemberService
{   
    // Variables
    private readonly AppDbContext _appDbContext;
    
    // Constructor
    public TeamMemberService(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }
    
    public async Task<List<TeamMember>> GetAllTeamMembersAsync()
    {
        return await _appDbContext.Set<TeamMember>().ToListAsync();
    }

    public async Task<TeamMember> GetTeamMemberByIdAsync(int id)
    {
        var teamMember = await _appDbContext.Set<TeamMember>().FindAsync(id);
        if (teamMember == null)
        {
            throw new NotFoundException($"Team member with id {id} not found");
        }

        return teamMember;
    }

    public async Task<TeamMember> CreateTeamMemberAsync(TeamMember teamMember)
    {
        var createdTeamMember = await _appDbContext.Set<TeamMember>().AddAsync(teamMember);
        await _appDbContext.SaveChangesAsync();
        return createdTeamMember.Entity;
    }

    public async Task<TeamMember> UpdateTeamMemberAsync(TeamMember teamMember)
    {
        var original = await _appDbContext.Set<TeamMember>().FindAsync(teamMember.Id);

        // Check if team member exists
        if (original is null)
        {
            throw new NotFoundException($"TeamMember with Id={teamMember.Id} Not Found");
        }

        // Check if team member has a team
        _appDbContext.Entry(original).CurrentValues.SetValues(teamMember);

        // Save changes
        await _appDbContext.SaveChangesAsync();

        return teamMember;
    }

    public async Task DeleteTeamMemberAsync(int id)
    {
        var original = await _appDbContext.Set<TeamMember>().FindAsync(id);

        // Check if team member exists
        if (original is null)
        {
            throw new NotFoundException($"TeamMember with Id={id} Not Found");
        }

        // Delete team member
        _appDbContext.Set<TeamMember>().Remove(original);

        // Save changes
        await _appDbContext.SaveChangesAsync();
    }
}