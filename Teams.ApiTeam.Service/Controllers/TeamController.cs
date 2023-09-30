using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Teams.ApiTeam.Service.Dtos;
using Teams.ApiTeam.Service.Interfaces;
using Teams.ApiTeam.Service.Models;

namespace Teams.ApiTeam.Service.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TeamController : ControllerBase
{
    // Variables
    private readonly ITeamService _teamService;
    private readonly IMapper _mapper;

    // Constructor
    public TeamController(ITeamService teamService, IMapper mapper)
    {
        _teamService = teamService;
        _mapper = mapper;
    }

    // GET: api/<TeamsController>
    [HttpGet]
    public async Task<IActionResult> GetAllTeamsAsync()
    {
        // Get all teams
        var teams = await _teamService.GetAllTeamsAsync();
        return Ok(_mapper.Map<List<Team>, List<TeamDto>>(teams));
    }

    // GET api/<TeamsController>/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetTeamByIdAsync(int id)
    {
        // Get team by id
        var team = await _teamService.GetTeamByIdAsync(id);
        return Ok(_mapper.Map<Team, TeamDto>(team));
    }

    // POST api/<TeamsController>
    [HttpPost]
    public async Task<IActionResult> CreateTeamAsync([FromBody] TeamDto team)
    { 
        // Create team
        var createdTeam = await _teamService.CreateTeamAsync(_mapper.Map<TeamDto, Team>(team));
        return Ok(_mapper.Map<Team, TeamDto>(createdTeam));
    }

    // PUT api/<TeamsController>/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTeamAsync(int id, [FromBody] TeamDto team)
    {
        // Update team
        var updatedTeam = await _teamService.UpdateTeamAsync(_mapper.Map<TeamDto, Team>(team));
        return Ok(_mapper.Map<Team, TeamDto>(updatedTeam));
    }

    // DELETE api/<TeamsController>/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTeamAsync(int id)
    {
        // Delete team
        await _teamService.DeleteTeamAsync(id);
        return Ok(new { Message = $"Team with id {id} has been deleted." });
    }

    // GET api/<TeamsController>/team/5/members
    [HttpGet("{teamId}/members")]
    public async Task<IActionResult> GetTeamMembersAsync(int teamId)
    {
        // Get team members by team id
        var teamMembers = await _teamService.GetTeamMembersByTeamIdAsync(teamId);
        return Ok(_mapper.Map<List<TeamMemberDto>, List<TeamMemberDto>>(teamMembers!));
    }
}