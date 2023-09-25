using AutoMapper;
using RestSharp;
using FluentValidation;
using Teams.ApiMember.Service.Dtos;
using Teams.ApiMember.Service.Interfaces;
using Teams.ApiMember.Service.Mapping;
using Teams.ApiMember.Service.Repositories;
using Teams.ApiMember.Service.Services;
using Teams.ApiMember.Service.Validations;


namespace Teams.ApiMember.Service.Extensions;

public static class ModulesExtension
{
    // Function AddServices
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        // Add RestSharp
        services.AddScoped<RestClient>();

        // Add TeamMemberService
        services.AddScoped<ITeamMemberService, TeamMemberService>();

        // Add TeamMemberRepository
        services.AddScoped<ITeamMemberRepository, TeamMemberRepository>();

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
        services.AddTransient<IValidator<TeamMemberDto>, TeamMemberValidator>();

        return services;
    }
}