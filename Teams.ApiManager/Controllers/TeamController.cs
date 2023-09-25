using Microsoft.AspNetCore.Mvc;
using Teams.ApiManager.Dtos;
using Teams.ApiManager.Exceptions;
using Teams.ApiManager.Interfaces;

namespace Teams.ApiManager.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TeamController : ControllerBase
{   
    // Variables
    private readonly ITeamConsumingService _teamConsumingService;
    
    // Constructor
    public TeamController(ITeamConsumingService teamConsumingService)
    {
        _teamConsumingService = teamConsumingService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateTeamAsync(TeamDto? team)
    {
        // if not data return bad request
        if (team == null)
        {
            throw new BadRequestException("Team is null");
        }

        // if team not created return bad request
        if (await _teamConsumingService.CreateTeamAsync(team) == null)
        {
            throw new BadRequestException("Team not created");
        }

        // Create team
        var teamResponse = await _teamConsumingService.CreateTeamAsync(team);
        return Ok(teamResponse);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllTeamsAsync()
    {
        // if not data return bad request
        if (await _teamConsumingService.GetAllTeamsAsync() == null)
        {
            throw new BadRequestException("Teams not found");
        }

        // Get all teams
        var teamsResponse = await _teamConsumingService.GetAllTeamsAsync();
        return Ok(teamsResponse);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetTeamByIdAsync(int id)
    {
        // if not data return bad request
        if (await _teamConsumingService.GetTeamByIdAsync(id) == null)
        {
            throw new BadRequestException("Team not found");
        }

        // Get team by id
        var teamResponse = await _teamConsumingService.GetTeamByIdAsync(id);
        return Ok(teamResponse);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTeamAsync(int id, TeamDto? team)
    {
        // if not data return bad request
        if (team == null)
        {
            throw new BadRequestException("Team is null");
        }

        // if team not updated return bad request
        if (await _teamConsumingService.UpdateTeamAsync(id, team)! == null)
        {
            throw new BadRequestException("Team not updated");
        }

        // Update team
        var teamResponse = await _teamConsumingService.UpdateTeamAsync(id, team)!;
        return Ok(teamResponse);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTeamAsync(int id)
    {
        // if team not deleted return bad request
        if (await _teamConsumingService.DeleteTeamAsync(id) == null)
        {
            throw new BadRequestException("Team not deleted");
        }

        // Delete team
        await _teamConsumingService.DeleteTeamAsync(id);
        return Ok();
    }
    
    [HttpGet("{id}/members")]
    public async Task<IActionResult> GetTeamMembersByTeamIdAsync(int id)
    {
        // if not data return bad request
        if (await _teamConsumingService.GetTeamMembersByTeamIdAsync(id) == null)
        {
            throw new BadRequestException("Team members not found");
        }

        // Get team members by team id
        var teamMembersResponse = await _teamConsumingService.GetTeamMembersByTeamIdAsync(id);
        return Ok(teamMembersResponse);
    }
}