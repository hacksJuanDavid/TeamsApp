using RestSharp;
using Teams.ApiManager.ConsumingServices;
using Teams.ApiManager.Interfaces;

namespace Teams.ApiManager.Extensions;

public static class ServiceExtension
{
    // Function AddServices
    public static IServiceCollection AddServices(this IServiceCollection services)
    {   
        // Add RestSharp
        services.AddScoped<RestClient>();
        
        // Add Services
        services.AddScoped<ITeamConsumingService,TeamConsumingService>();
        services.AddScoped<ITeamMemberConsumingService,TeamMemberConsumingService>();
        
        return services;
    }
    
    
    
    
    
}