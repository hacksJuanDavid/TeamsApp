using Teams.ApiMember.Service.Interfaces;
using Teams.ApiMember.Service.Models;

namespace Teams.ApiMember.Service.Repositories;

public class TeamMemberRepository : ITeamMemberRepository
{
    // Variables
    private readonly ITeamMemberService _teamMemberService;

    // Constructor
    public TeamMemberRepository(ITeamMemberService teamMemberService)
    {
        _teamMemberService = teamMemberService;
    }

    // Methods
    public async Task<List<TeamMember>> GetAllTeamMembersAsync()
    {
        return await _teamMemberService.GetAllTeamMembersAsync();
    }

    public async Task<TeamMember> GetTeamMemberByIdAsync(int id)
    {
        return await _teamMemberService.GetTeamMemberByIdAsync(id);
    }

    public async Task<TeamMember> CreateTeamMemberAsync(TeamMember teamMember)
    {
        return await _teamMemberService.CreateTeamMemberAsync(teamMember);
    }

    public async Task<TeamMember> UpdateTeamMemberAsync(TeamMember teamMember)
    {
        return await _teamMemberService.UpdateTeamMemberAsync(teamMember);
    }

    public async Task DeleteTeamMemberAsync(int id)
    {
        await _teamMemberService.DeleteTeamMemberAsync(id);
    }
}