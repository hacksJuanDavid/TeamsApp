using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Teams.ApiTeam.Service.Exceptions;
using Teams.ApiTeam.Service.Dtos;
using Teams.ApiTeam.Service.Interfaces;
using Teams.ApiTeam.Service.Models;

namespace Teams.ApiTeam.Service.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TeamController : ControllerBase
{
    // Variables
    private readonly ITeamRepository _teamRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<TeamDto> _teamValidator;

    // Constructor
    public TeamController(ITeamRepository teamRepository, IMapper mapper, IValidator<TeamDto> teamValidator)
    {
        _teamRepository = teamRepository;
        _mapper = mapper;
        _teamValidator = teamValidator;
    }

    // GET: api/<TeamsController>
    [HttpGet]
    public async Task<IActionResult> GetAllTeamsAsync()
    {
        var teams = await _teamRepository.GetAllTeamsAsync();
        return Ok(_mapper.Map<List<Team>, List<TeamDto>>(teams));
    }

    // GET api/<TeamsController>/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetTeamByIdAsync(int id)
    {
        var team = await _teamRepository.GetTeamByIdAsync(id);

        // Check if team exists
        if (team == null)
        {
            throw new NotFoundException($"Team with id {id} does not exist");
        }

        return Ok(_mapper.Map<Team, TeamDto>(team));
    }

    // POST api/<TeamsController>
    [HttpPost]
    public async Task<IActionResult> CreateTeamAsync([FromBody] TeamDto team)
    {
        try
        {
            // Validate TeamDto
            var validationResult = await _teamValidator.ValidateAsync(team);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(error => error.ErrorMessage).ToList();
                return BadRequest(new { Errors = errors });
            }

            return Ok(await _teamRepository.CreateTeamAsync(_mapper.Map<TeamDto, Team>(team)));
        }
        catch (ValidationException ex)
        {
            // Handle FluentValidation ValidationException
            return BadRequest(new { Errors = new List<string> { ex.Message } });
        }
    }

    // PUT api/<TeamsController>/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTeamAsync(int id, [FromBody] TeamDto team)
    {
        try
        {
            // Validate TeamDto
            var validationResult = await _teamValidator.ValidateAsync(team);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(error => error.ErrorMessage).ToList();
                return BadRequest(new { Errors = errors });
            }

            // Check if team exists
            var teamToUpdate = await _teamRepository.GetTeamByIdAsync(id);
            if (teamToUpdate == null)
            {
                throw new NotFoundException($"Team with id {id} does not exist");
            }

            return Ok(await _teamRepository.UpdateTeamAsync(_mapper.Map<TeamDto, Team>(team)));
        }
        catch (ValidationException ex)
        {
            // Handle FluentValidation ValidationException
            return BadRequest(new { Errors = new List<string> { ex.Message } });
        }
    }

    // DELETE api/<TeamsController>/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTeamAsync(int id)
    {
        // Check if team exists
        var teamToDelete = await _teamRepository.GetTeamByIdAsync(id);
        if (teamToDelete == null)
        {
            throw new NotFoundException($"Team with id {id} does not exist");
        }

        // Delete team
        await _teamRepository.DeleteTeamAsync(id);

        return Ok(new { Message = $"Team with id {id} has been deleted." });
    }

    // GET api/<TeamsController>/team/5/members
}