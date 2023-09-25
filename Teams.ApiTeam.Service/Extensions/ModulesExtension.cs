using AutoMapper;
using RestSharp;
using FluentValidation;
using Teams.ApiTeam.Service.Dtos;
using Teams.ApiTeam.Service.Interfaces;
using Teams.ApiTeam.Service.Mapping;
using Teams.ApiTeam.Service.Repositories;
using Teams.ApiTeam.Service.Services;
using Teams.ApiTeam.Service.Validations;

namespace Teams.ApiTeam.Service.Extensions;

public static class ModulesExtension
{
    // Function AddServices
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        // Add RestSharp
        services.AddScoped<RestClient>();
        
        // Add TeamMemberService
        services.AddScoped<ITeamService, TeamService>();

        // Add TeamMemberRepository
        services.AddScoped<ITeamRepository, TeamRepository>();

        return services;
    }
    
    // Function AddAutoMapper
    public static IServiceCollection AddMapping(this IServiceCollection services)
    {
        // Auto Mapper Configurations
        var mapperConfig = new MapperConfiguration(mc => { mc.AddProfile(new MappingProfile()); });

        IMapper mapper = mapperConfig.CreateMapper();
        services.AddSingleton(mapper);

        return services;
    }
    
    // Function AddFluentValidationAutoValidation
    public static IServiceCollection AddFluentValidationAutoValidation(this IServiceCollection services)
    {
        // Add TeamMemberValidator
        services.AddTransient<IValidator<TeamDto>, TeamValidator>();

        return services;
    }
}