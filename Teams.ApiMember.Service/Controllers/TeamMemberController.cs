using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Teams.ApiMember.Service.Dtos;
using Teams.ApiMember.Service.Exceptions;
using Teams.ApiMember.Service.Interfaces;
using Teams.ApiMember.Service.Models;

namespace Teams.ApiMember.Service.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TeamMemberController : ControllerBase
{
    // Variables
    private readonly ITeamMemberRepository _teamMemberRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<TeamMemberDto> _teamMemberValidator;

    // Constructor
    public TeamMemberController(ITeamMemberRepository teamMemberRepository, IMapper mapper,
        IValidator<TeamMemberDto> teamMemberValidator)
    {
        _teamMemberRepository = teamMemberRepository;
        _mapper = mapper;
        _teamMemberValidator = teamMemberValidator;
    }

    // GET: api/<TeamsMemberController>
    [HttpGet]
    public async Task<IActionResult> GetAllTeamMembersAsync()
    {
        var teamMembers = await _teamMemberRepository.GetAllTeamMembersAsync();
        return Ok(_mapper.Map<List<TeamMember>, List<TeamMemberDto>>(teamMembers));
    }

    // GET api/<TeamsMemberController>/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetTeamMemberByIdAsync(int id)
    {
        var teamMember = await _teamMemberRepository.GetTeamMemberByIdAsync(id);

        // Check if team member exists
        if (teamMember == null)
        {
            throw new NotFoundException($"Team with id {id} does not exist");
        }

        return Ok(_mapper.Map<TeamMember, TeamMemberDto>(teamMember));
    }

    // POST api/<TeamsMemberController>
    [HttpPost]
    public async Task<IActionResult> CreateTeamMemberAsync([FromBody] TeamMemberDto teamMember)
    {
        try
        {
            // Validate TeamMemberDto
            var validationResult = await _teamMemberValidator.ValidateAsync(teamMember);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return BadRequest(new { Erros = errors });
            }

            return Ok(await _teamMemberRepository.CreateTeamMemberAsync(
                _mapper.Map<TeamMemberDto, TeamMember>(teamMember)));
        }
        catch (Exception ex)
        {
            // Handle FluentValidation ValidationException
            return BadRequest(new { Errors = new List<string> { ex.Message } });
        }
    }

    // PUT api/<TeamsMemberController>/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTeamMemberAsync(int id, [FromBody] TeamMemberDto teamMember)
    {
        try
        {
            // Validate TeamMemberDto
            var validationResult = await _teamMemberValidator.ValidateAsync(teamMember);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return BadRequest(new { Errors = errors });
            }

            // Check if team member exists
            var existingTeamMember = await _teamMemberRepository.GetTeamMemberByIdAsync(id);
            if (existingTeamMember == null)
            {
                throw new NotFoundException($"Team member with id {id} does not exist");
            }

            return Ok(await _teamMemberRepository.UpdateTeamMemberAsync(
                _mapper.Map<TeamMemberDto, TeamMember>(teamMember)));
        }
        catch (Exception ex)
        {
            // Handle FluentValidation ValidationException
            return BadRequest(new { Errors = new List<string> { ex.Message } });
        }
    }

    // DELETE api/<TeamsMemberController>/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTeamMemberAsync(int id)
    {
        await _teamMemberRepository.DeleteTeamMemberAsync(id);
        return Ok(new { Message = $"Team member with id {id} has been deleted." });
    }

    // Get /members/{id}/teams
    [HttpGet("{memberId}/teams")]
    public async Task<IActionResult> GetTeamsByMemberIdAsync(int memberId)
    {
        var teams = await _teamMemberRepository.GetTeamsByMemberIdAsync(memberId);
        return Ok(_mapper.Map<List<TeamDto>, List<TeamDto>>(teams));
    }
}