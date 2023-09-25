using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using Teams.ApiManager.Interfaces;
using Teams.ApiManager.Settings;
using Teams.ApiManager.Dtos;
using Teams.ApiManager.Exceptions;

namespace Teams.ApiManager.ConsumingServices;

public class TeamMemberConsumingService : ITeamMemberConsumingService
{
    // Variables
    private readonly RestClient _client;

    // Constructor
    public TeamMemberConsumingService(IOptions<AppSettings> apiSettings, RestClient client)
    {
        // Client 
        _client = client;

        // ApiTeamMemberUrl
        var apiMemberUrl = apiSettings.Value.ApiMemberUrl;

        // Client Init with ApiTeamMemberUrl
        if (apiMemberUrl != null) _client = new RestClient(apiMemberUrl);
    }

    public async Task<TeamMemberDto?> CreateTeamMemberAsync(TeamMemberDto? teamMember)
    {
        // POST api/TeamMember
        var request = new RestRequest("TeamMember/");

        if (teamMember == null)
        {
            return null;
        }

        // Serialize teamMember
        var teamMemberJson = JsonConvert.SerializeObject(teamMember);

        // Add Json to request
        request.AddParameter("application/json", teamMemberJson, ParameterType.RequestBody);

        // Execute request
        var response = await _client.PostAsync(request);

        // Check status code
        if (response.IsSuccessful)
        {
            return response.IsSuccessful ? teamMember : null;
        }
        else
        {
            throw new BadRequestException($"Error create teamMember: {response.StatusCode}");
        }
    }

    public async Task<object?> GetAllTeamMembersAsync()
    {
        // GET api/TeamMember
        var request = new RestRequest("TeamMember/");

        // Execute request
        var response = await _client.GetAsync(request);

        // Check status code
        if (response.IsSuccessful)
        {
            // Deserialize response
            var teamMemberJson = JsonConvert.DeserializeObject<JArray>(response.Content!);

            // Return response
            return teamMemberJson;
        }
        else
        {
            throw new BadRequestException($"Error get all teamMembers: {response.StatusCode}");
        }
    }

    public async Task<object?> GetTeamMemberByIdAsync(int id)
    {
        // GET api/TeamMember/{id}
        var request = new RestRequest("TeamMember/{id}");

        // Add id to request
        request.AddUrlSegment("id", id);

        // Execute request
        var response = await _client.GetAsync(request);

        // Check status code
        if (response.IsSuccessful)
        {
            // Deserialize response
            var teamMemberJson = JsonConvert.DeserializeObject<JObject>(response.Content!);

            // Return response
            return teamMemberJson;
        }
        else
        {
            throw new BadRequestException($"Error get teamMember by id: {response.StatusCode}");
        }
    }

    public async Task<TeamMemberDto?>? UpdateTeamMemberAsync(int id, TeamMemberDto? teamMember)
    {
        // PUT api/TeamMember/{id}
        var request = new RestRequest("TeamMember/{id}");

        // Add id to request
        request.AddUrlSegment("id", id);

        if (teamMember == null)
        {
            return null;
        }

        // Serialize teamMember
        var teamMemberJson = JsonConvert.SerializeObject(teamMember);

        // Add Json to request
        request.AddParameter("application/json", teamMemberJson, ParameterType.RequestBody);

        // Execute request
        var response = await _client.PutAsync(request);

        // Check status code
        if (response.IsSuccessful)
        {
            return response.IsSuccessful ? teamMember : null;
        }
        else
        {
            throw new BadRequestException($"Error update teamMember: {response.StatusCode}");
        }
    }

    public async Task<TeamMemberDto?> DeleteTeamMemberAsync(int id)
    {
        // DELETE api/TeamMember/{id}
        var request = new RestRequest("TeamMember/{id}");

        // Add id to request
        request.AddUrlSegment("id", id);

        // Execute request
        var response = await _client.DeleteAsync(request);

        // Check status code
        if (response.IsSuccessful)
        {
            return null;
        }
        else
        {
            throw new BadRequestException($"Error delete teamMember: {response.StatusCode}");
        }
    }

    public async Task<List<TeamDto>?> GetTeamsByMemberIdAsync(int memberId)
    {
        // GET api/TeamMember/{memberId}/teams
        var request = new RestRequest("TeamMember/{memberId}/teams");

        // Add id to request
        request.AddUrlSegment("memberId", memberId);

        // Execute request
        var response = await _client.GetAsync(request);

        // Check status code
        if (response.IsSuccessful)
        {
            // Deserialize response as a list of TeamDto
            var teams = JsonConvert.DeserializeObject<List<TeamDto>>(response.Content!);

            return teams;
        }
        else
        {
            throw new BadRequestException($"Error get teams by member id: {response.StatusCode}");
        }
    }
}