using Microsoft.AspNetCore.Mvc;
using Teams.ApiManager.Dtos;
using Teams.ApiManager.Interfaces;

namespace Teams.ApiManager.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TeamController : ControllerBase
{
    private readonly ITeamConsumingService _teamConsumingService;

    public TeamController(ITeamConsumingService teamConsumingService)
    {
        _teamConsumingService = teamConsumingService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateTeamAsync(TeamDto? team)
    {
        var teamResponse = await _teamConsumingService.CreateTeamAsync(team);
        return Ok(teamResponse);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllTeamsAsync()
    {
        var teamsResponse = await _teamConsumingService.GetAllTeamsAsync();
        return Ok(teamsResponse);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetTeamByIdAsync(int id)
    {
        var teamResponse = await _teamConsumingService.GetTeamByIdAsync(id) ?? new { Message = "Not found team" };
        return Ok(teamResponse);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTeamAsync(int id, TeamDto? team)
    {
        var teamResponse = await _teamConsumingService.UpdateTeamAsync(id, team)!;
        return Ok(teamResponse);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTeamAsync(int id)
    {
        var teamResponse = await _teamConsumingService.DeleteTeamAsync(id);
        return Ok(teamResponse);
    }
}