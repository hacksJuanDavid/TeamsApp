using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RestSharp;
using Teams.ApiTeam.Service.Dtos;
using Teams.ApiTeam.Service.Exceptions;
using Teams.ApiTeam.Service.Interfaces;
using Teams.ApiTeam.Service.Models;
using Teams.ApiTeam.Service.Settings;

namespace Teams.ApiTeam.Service.Services;

public class TeamService : ITeamService
{
    // Variables
    private readonly ITeamRepository _teamRepository;
    private readonly RestClient _client;

    // Constructor
    public TeamService(IOptions<AppSettings> apiSettings, RestClient client, ITeamRepository teamRepository)
    {
        _teamRepository = teamRepository;
        _client = client;

        // ApiTeamMemberUrl
        var apiTeamMemberUrl = apiSettings.Value.ApiMemberUrl;

        // Client Init with ApiTeamMemberUrl
        if (apiTeamMemberUrl != null) _client = new RestClient(apiTeamMemberUrl);
    }

    public async Task<List<Team>> GetAllTeamsAsync()
    {
        // Return all teams
        var teams = await _teamRepository.GetAllTeamsAsync();

        return teams;
    }

    public async Task<Team> GetTeamByIdAsync(int id)
    {
        // Get team
        var team = await _teamRepository.GetTeamByIdAsync(id);

        // Check if team exists
        if (team == null)
        {
            throw new NotFoundException($"Team with id {id} not found");
        }

        return team;
    }

    public async Task<Team> CreateTeamAsync(Team team)
    {
        // Get team by id
        var original = await _teamRepository.GetTeamByIdAsync(team.Id);

        // Check if team exists
        if (original != null)
        {
            throw new BadRequestException($"Team with Id={team.Id} already exists");
        }

        // Create team
        var createdTeam = await _teamRepository.CreateTeamAsync(team);

        return createdTeam;
    }

    public async Task<Team> UpdateTeamAsync(Team team)
    {
        // Get team by id
        var original = await _teamRepository.GetTeamByIdAsync(team.Id);

        // Check if team exists
        if (original is null)
        {
            throw new NotFoundException($"Team with Id={team.Id} Not Found");
        }

        // Update team
        var updatedTeam = await _teamRepository.UpdateTeamAsync(team);

        return updatedTeam;
    }

    public async Task DeleteTeamAsync(int id)
    {
        var original = await _teamRepository.GetTeamByIdAsync(id);

        // Check if team exists
        if (original is null)
        {
            throw new NotFoundException($"Team with Id={id} Not Found");
        }

        // Delete team members
        var teamMembers = await GetTeamMembersByTeamIdAsync(id);

        if (teamMembers != null)
        {
            foreach (var teamMember in teamMembers)
            {
                var response = await _client.DeleteAsync(new RestRequest($"TeamMember/{teamMember.Id}"));
                if (!response.IsSuccessful)
                {
                    // Log or handle the error, but do not throw an exception to prevent stopping the team deletion process.
                    throw new BadRequestException($"Error deleting teamMember: {response.StatusCode}");
                }
            }
        }

        // Delete team
        await _teamRepository.DeleteTeamAsync(id);
    }

    // Get /teams/{teamId}/members
    public async Task<List<TeamMemberDto>?> GetTeamMembersByTeamIdAsync(int teamId)
    {
        // Get team
        var team = await _teamRepository.GetTeamByIdAsync(teamId);

        // Check if team exists
        if (team == null)
        {
            throw new NotFoundException($"Team with id {teamId} not found");
        }

        // Get team members
        var request = new RestRequest("TeamMember/");

        // Execute request
        var response = await _client.GetAsync(request);

        // Check status code
        if (!response.IsSuccessful)
        {
            throw new BadRequestException($"Error get all teamMembers: {response.StatusCode}");
        }

        // Deserialize response
        var teamMembers = JsonConvert.DeserializeObject<List<TeamMemberDto>>(response.Content!);

        // Filter team members by team id
        teamMembers = teamMembers?.Where(teamMember => teamMember.TeamId == teamId).ToList();

        return teamMembers;
    }
}