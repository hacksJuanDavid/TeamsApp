using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using Teams.ApiManager.Interfaces;
using Teams.ApiManager.Settings;
using Teams.ApiManager.Dtos;
using Teams.ApiManager.Exceptions;

namespace Teams.ApiManager.ConsumingServices;

public class TeamConsumingService : ITeamConsumingService
{
    // Variables
    private readonly RestClient _client;

    // Constructor
    public TeamConsumingService(IOptions<AppSettings> apiSettings, RestClient client)
    {
        // Client 
        _client = client;

        // ApiTeamUrl
        var apiTeamUrl = apiSettings.Value.ApiTeamUrl;

        // Client Init with ApiTeamUrl
        if (apiTeamUrl != null) _client = new RestClient(apiTeamUrl);
    }

    // CreateTeam
    public async Task<TeamDto?> CreateTeamAsync(TeamDto? team)
    {
        // POST api/Team
        var request = new RestRequest("Team/");

        if (team == null)
        {
            return null;
        }

        // Serialize team
        var teamJson = JsonConvert.SerializeObject(team);

        // Add Json to request
        request.AddParameter("application/json", teamJson, ParameterType.RequestBody);

        // Execute request
        var response = await _client.PostAsync(request);

        // Check status code
        if (response.IsSuccessful)
        {
            return response.IsSuccessful ? team : null;
        }
        else
        {
            throw new BadRequestException($"Error create team: {response.StatusCode}");
        }
    }

    // GetAllTeams
    public async Task<object?> GetAllTeamsAsync()
    {
        // GET api/Team
        var request = new RestRequest("Team/");

        // Execute request
        var response = await _client.GetAsync(request);

        // Check status code
        if (response.IsSuccessful)
        {
            // Content
            var content = response.Content;

            if (!string.IsNullOrEmpty(content))
            {
                // Pretty content
                var prettyContent = JToken.Parse(content).ToString(Formatting.Indented);

                return prettyContent;
            }
        }

        throw new NotFoundException($"Error get all teams: {response.StatusCode}");
    }

    // GetTeamById
    public async Task<object?> GetTeamByIdAsync(int id)
    {
        // GET api/Team/{id}
        var request = new RestRequest("Team/{id}");

        // Add id to request
        request.AddUrlSegment("id", id);

        // Execute request
        var response = await _client.GetAsync(request);

        // Check status code
        if (response.IsSuccessful)
        {
            // Content
            var content = response.Content;

            // Check content
            if (content != null)
            {
                // Pretty content
                var prettyContent = JToken.Parse(content).ToString(Formatting.Indented);

                return prettyContent;
            }
        }

        throw new NotFoundException($"Team member with id {id} not found");
    }

    // UpdateTeam
    public async Task<TeamDto?> UpdateTeamAsync(int id, TeamDto? team)
    {
        // PUT api/Team/{id}
        var request = new RestRequest("Team/{id}");

        // Add id to request
        request.AddUrlSegment("id", id);

        // Check team is null
        if (team == null)
        {
            return null;
        }

        // Serialize team
        var teamJson = JsonConvert.SerializeObject(team);

        // Add Json to request
        request.AddParameter("application/json", teamJson, ParameterType.RequestBody);

        // Var response
        var response = await _client.PutAsync(request);

        // Check status code
        if (response.IsSuccessful)
        {
            return response.IsSuccessful ? team : null;
        }
        else
        {
            throw new BadRequestException($"Error update team: {response.StatusCode}");
        }
    }

    // DeleteTeam
    public async Task<TeamDto?> DeleteTeamAsync(int id)
    {
        // DELETE api/Team/{id}
        var request = new RestRequest("Team/{id}");

        // Add id to request
        request.AddUrlSegment("id", id);

        // Execute request
        var response = await _client.DeleteAsync(request);

        // Check status code
        if (!response.IsSuccessful)
        {
            throw new BadRequestException($"Error delete team: {response.StatusCode}");
        }

        // Delete correct
        return response.IsSuccessful ? new TeamDto() : null;
    }
}