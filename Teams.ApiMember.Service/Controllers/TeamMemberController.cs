using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Teams.ApiMember.Service.Dtos;
using Teams.ApiMember.Service.Interfaces;
using Teams.ApiMember.Service.Models;

namespace Teams.ApiMember.Service.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TeamMemberController : ControllerBase
{
    // Variables
    private readonly ITeamMemberService _teamMemberService;
    private readonly IMapper _mapper;

    // Constructor
    public TeamMemberController(ITeamMemberService teamMemberService, IMapper mapper)
    {
        _teamMemberService = teamMemberService;
        _mapper = mapper;
    }

    // GET: api/<TeamsMemberController>
    [HttpGet]
    public async Task<IActionResult> GetAllTeamMembersAsync()
    {
        // Get all team members
        var teamMembers = await _teamMemberService.GetAllTeamMembersAsync();
        return Ok(_mapper.Map<List<TeamMember>, List<TeamMemberDto>>(teamMembers));
    }

    // GET api/<TeamsMemberController>/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetTeamMemberByIdAsync(int id)
    {
        // Get team member by id
        var teamMember = await _teamMemberService.GetTeamMemberByIdAsync(id);
        return Ok(_mapper.Map<TeamMember, TeamMemberDto>(teamMember));
    }

    // POST api/<TeamsMemberController>
    [HttpPost]
    public async Task<IActionResult> CreateTeamMemberAsync([FromBody] TeamMemberDto teamMember)
    {
        // Create team member
        var createdTeamMember = await _teamMemberService.CreateTeamMemberAsync(_mapper.Map<TeamMemberDto, TeamMember>(teamMember));
        return Ok(_mapper.Map<TeamMember, TeamMemberDto>(createdTeamMember));
    }

    // PUT api/<TeamsMemberController>/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTeamMemberAsync(int id, [FromBody] TeamMemberDto teamMember)
    {   
        // Update team member
        var updatedTeamMember = await _teamMemberService.UpdateTeamMemberAsync(_mapper.Map<TeamMemberDto, TeamMember>(teamMember));
        return Ok(_mapper.Map<TeamMember, TeamMemberDto>(updatedTeamMember));
    }

    // DELETE api/<TeamsMemberController>/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTeamMemberAsync(int id)
    {   
        // Delete team member
        await _teamMemberService.DeleteTeamMemberAsync(id);
        return Ok(new { Message = $"Team member with id {id} has been deleted." });
    }

    // Get /members/{id}/teams
    [HttpGet("{memberId}/teams")]
    public async Task<IActionResult> GetTeamsByMemberIdAsync(int memberId)
    {
        // Get teams by member id
        var teams = await _teamMemberService.GetTeamsByMemberIdAsync(memberId);
        return Ok(_mapper.Map<List<TeamDto>, List<TeamDto>>(teams));
    }
}