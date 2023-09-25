using Microsoft.AspNetCore.Mvc;
using Teams.ApiManager.Dtos;
using Teams.ApiManager.Exceptions;
using Teams.ApiManager.Interfaces;

namespace Teams.ApiManager.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TeamMemberController : ControllerBase
{
    // Variables
    private readonly ITeamMemberConsumingService _teamMemberConsumingService;

    // Constructor
    public TeamMemberController(ITeamMemberConsumingService teamMemberConsumingService)
    {
        _teamMemberConsumingService = teamMemberConsumingService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateTeamMemberAsync(TeamMemberDto? teamMember)
    {
        // if not data return bad request
        if (teamMember == null)
        {
            throw new BadRequestException("TeamMember is null");
        }

        // if teamMember not created return bad request
        if (await _teamMemberConsumingService.CreateTeamMemberAsync(teamMember) == null)
        {
            throw new BadRequestException("TeamMember not created");
        }

        // Create teamMember
        var teamMemberResponse = await _teamMemberConsumingService.CreateTeamMemberAsync(teamMember);
        return Ok(teamMemberResponse);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllTeamMembersAsync()
    {
        // if not data return bad request
        if (await _teamMemberConsumingService.GetAllTeamMembersAsync() == null)
        {
            throw new BadRequestException("TeamMembers not found");
        }

        // Get all teamMembers
        var teamMembersResponse = await _teamMemberConsumingService.GetAllTeamMembersAsync();
        return Ok(teamMembersResponse);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetTeamMemberByIdAsync(int id)
    {
        // if not data return bad request
        if (await _teamMemberConsumingService.GetTeamMemberByIdAsync(id) == null)
        {
            throw new BadRequestException("TeamMember not found");
        }

        // Get teamMember by id
        var teamMemberResponse = await _teamMemberConsumingService.GetTeamMemberByIdAsync(id);
        return Ok(teamMemberResponse);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTeamMemberAsync(int id, TeamMemberDto? teamMember)
    {
        // if not data return bad request
        if (teamMember == null)
        {
            throw new BadRequestException("TeamMember is null");
        }

        // if teamMember not updated return bad request
        if (await _teamMemberConsumingService.UpdateTeamMemberAsync(id, teamMember)! == null)
        {
            throw new BadRequestException("TeamMember not updated");
        }

        // Update teamMember
        var teamMemberResponse = await _teamMemberConsumingService.UpdateTeamMemberAsync(id, teamMember)!;
        return Ok(teamMemberResponse);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTeamMemberAsync(int id)
    {
        // if not data return bad request
        if (await _teamMemberConsumingService.DeleteTeamMemberAsync(id) == null)
        {
            throw new BadRequestException("TeamMember not deleted");
        }

        // Delete teamMember
        var teamMemberResponse = await _teamMemberConsumingService.DeleteTeamMemberAsync(id);
        return Ok(teamMemberResponse);
    }

    [HttpGet("{id}/team")]
    public async Task<IActionResult> GetTeamsByMemberIdAsync(int id)
    {
        // if not data return bad request
        if (await _teamMemberConsumingService.GetTeamsByMemberIdAsync(id) == null)
        {
            throw new BadRequestException("Team not found");
        }

        // Get team by teamMember id
        var teamResponse = await _teamMemberConsumingService.GetTeamsByMemberIdAsync(id);
        return Ok(teamResponse);
    }
}