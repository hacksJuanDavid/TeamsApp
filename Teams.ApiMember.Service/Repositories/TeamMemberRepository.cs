using Microsoft.EntityFrameworkCore;
using Teams.ApiMember.Service.Context;
using Teams.ApiMember.Service.Exceptions;
using Teams.ApiMember.Service.Interfaces;
using Teams.ApiMember.Service.Models;

namespace Teams.ApiMember.Service.Repositories;

public class TeamMemberRepository : ITeamMemberRepository
{
    // Variables
    private readonly AppDbContext _appDbContext;

    // Constructor
    public TeamMemberRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    // Methods
    public async Task<List<TeamMember>> GetAllTeamMembersAsync()
    {
        return await _appDbContext.Set<TeamMember>().ToListAsync();
    }

    public async Task<TeamMember?> GetTeamMemberByIdAsync(int id)
    {
        return await _appDbContext.Set<TeamMember>().FindAsync(id);
    }

    public async Task<TeamMember> CreateTeamMemberAsync(TeamMember teamMember)
    {
        var createdTeamMember = await _appDbContext.Set<TeamMember>().AddAsync(teamMember);
        await _appDbContext.SaveChangesAsync();
        return createdTeamMember.Entity;
    }

    public async Task<TeamMember> UpdateTeamMemberAsync(TeamMember teamMember)
    {
        // Busca la entidad original por ID
        var original = await _appDbContext.TeamMembers.FindAsync(teamMember.Id);

        if (original != null)
        {
            // Desconecta la entidad original del contexto
            _appDbContext.Entry(original).State = EntityState.Detached;

            // Actualiza la entidad
            var updatedTeamMember = _appDbContext.Update(teamMember);
            await _appDbContext.SaveChangesAsync();

            return updatedTeamMember.Entity;
        }

        throw new NotFoundException($"Team member with Id={teamMember.Id} Not Found");
    }

    public async Task DeleteTeamMemberAsync(int id)
    {
        var teamMember = await _appDbContext.Set<TeamMember>().FindAsync(id);
        if (teamMember != null) _appDbContext.Set<TeamMember>().Remove(teamMember);
        await _appDbContext.SaveChangesAsync();
    }

    public async Task<TeamMember?> GetTeamMembersByMemberIdAsync(int memberId)
    {
        return await _appDbContext.Set<TeamMember>().Where(x => x.TeamId == memberId).FirstOrDefaultAsync();
    }
}