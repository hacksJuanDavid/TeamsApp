using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RestSharp;
using Teams.ApiMember.Service.Context;
using Teams.ApiMember.Service.Dtos;
using Teams.ApiMember.Service.Exceptions;
using Teams.ApiMember.Service.Interfaces;
using Teams.ApiMember.Service.Models;
using Teams.ApiMember.Service.Settings;

namespace Teams.ApiMember.Service.Services;

public class TeamMemberService : ITeamMemberService
{
    // Variables
    private readonly AppDbContext _appDbContext;
    private readonly RestClient _client;

    // Constructor
    public TeamMemberService(AppDbContext appDbContext, IOptions<AppSettings> apiSetting, RestClient client)
    {
        _appDbContext = appDbContext;
        _client = client;

        // ApiTeamUrl
        var apiTeamUrl = apiSetting.Value.ApiTeamUrl;

        // Client Init with ApiTeamUrl
        if (apiTeamUrl != null) _client = new RestClient(apiTeamUrl);
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

    public async Task<List<TeamDto>> GetTeamsByMemberIdAsync(int memberId)
    {
        // Get team members
        var teamMembers = await _appDbContext.Set<TeamMember>().Where(tm => tm.Id == memberId).ToListAsync();
        
        // Check if team members exists
        if (teamMembers.Count == 0)
        {
            throw new NotFoundException($"TeamMember with Id={memberId} Not Found");
        }
        
        // Get teams
        var teams = new List<TeamDto>();
        
        foreach (var teamMember in teamMembers)
        {
            var request = new RestRequest($"Team/{teamMember.TeamId}");
            var response = await _client.ExecuteAsync(request);

            if (response.IsSuccessful)
            {
                var team = JsonConvert.DeserializeObject<TeamDto>(response.Content!);
                if (team != null) teams.Add(team);
            }
            else
            {
                throw new BadRequestException($"Error get team: {response.StatusCode}");
            }
        }
        
        return teams;
    }
}