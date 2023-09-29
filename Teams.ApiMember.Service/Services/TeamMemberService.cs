using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RestSharp;
using Teams.ApiMember.Service.Dtos;
using Teams.ApiMember.Service.Exceptions;
using Teams.ApiMember.Service.Interfaces;
using Teams.ApiMember.Service.Models;
using Teams.ApiMember.Service.Settings;

namespace Teams.ApiMember.Service.Services;

public class TeamMemberService : ITeamMemberService
{
    // Variables
    private readonly ITeamMemberRepository _teamMemberRepository;
    private readonly RestClient _client;

    // Constructor
    public TeamMemberService(IOptions<AppSettings> apiSetting, RestClient client,
        ITeamMemberRepository teamMemberRepository)
    {
        _teamMemberRepository = teamMemberRepository;
        _client = client;

        // ApiTeamUrl
        var apiTeamUrl = apiSetting.Value.ApiTeamUrl;

        // Client Init with ApiTeamUrl
        if (apiTeamUrl != null) _client = new RestClient(apiTeamUrl);
    }

    public async Task<List<TeamMember>> GetAllTeamMembersAsync()
    {
        return await _teamMemberRepository.GetAllTeamMembersAsync();
    }

    public async Task<TeamMember> GetTeamMemberByIdAsync(int id)
    {
        // Get team member by id
        var teamMember = await _teamMemberRepository.GetTeamMemberByIdAsync(id);

        // Check if team member exists
        if (teamMember == null)
        {
            throw new NotFoundException($"Team member with id {id} not found");
        }

        return teamMember;
    }

    public async Task<TeamMember> CreateTeamMemberAsync(TeamMember teamMember)
    {
        // Get team member by id
        var original = await _teamMemberRepository.GetTeamMemberByIdAsync(teamMember.Id);

        // Check if team member exists
        if (original != null)
        {
            throw new BadRequestException($"Team member with id {teamMember.Id} already exists");
        }

        // Create team member
        var createdTeamMember = await _teamMemberRepository.CreateTeamMemberAsync(teamMember);

        return createdTeamMember;
    }

    public async Task<TeamMember> UpdateTeamMemberAsync(TeamMember teamMember)
    {
        // Get team member by id
        var original = await _teamMemberRepository.GetTeamMemberByIdAsync(teamMember.Id);

        // Check if team member exists
        if (original is null)
        {
            throw new NotFoundException($"TeamMember with Id={teamMember.Id} Not Found");
        }

        // Update team member
        var updatedTeamMember = await _teamMemberRepository.UpdateTeamMemberAsync(teamMember);

        return updatedTeamMember;
    }

    public async Task DeleteTeamMemberAsync(int id)
    {
        // Get team member by id
        var original = await _teamMemberRepository.GetTeamMemberByIdAsync(id);

        // Check if team member exists
        if (original is null)
        {
            throw new NotFoundException($"TeamMember with Id={id} Not Found");
        }

        // Delete team member
        await _teamMemberRepository.DeleteTeamMemberAsync(id);
    }

    public async Task<List<TeamDto>> GetTeamsByMemberIdAsync(int memberId)
    {
        // Get team member
        var teamMember = await _teamMemberRepository.GetTeamMembersByMemberIdAsync(memberId);

        // Check if team member exists
        if (teamMember == null)
        {
            throw new NotFoundException($"TeamMember with Id={memberId} Not Found");
        }

        // Get teams
        var teams = new List<TeamDto>();

        // Get team
        var request = new RestRequest($"Team/{teamMember.TeamId}");

        // Get response
        var response = await _client.ExecuteAsync(request);

        if (response.IsSuccessful)
        {
            // Deserialize response
            var team = JsonConvert.DeserializeObject<TeamDto>(response.Content!);
            // Check if team exists and add to list
            if (team != null)
            {
                teams.Add(team);
            }
        }
        else
        {
            throw new BadRequestException($"Error getting team: {response.StatusCode}");
        }

        return teams;
    }
}