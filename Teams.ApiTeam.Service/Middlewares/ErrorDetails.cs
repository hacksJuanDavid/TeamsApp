using Newtonsoft.Json;

namespace Teams.ApiTeam.Service.Middlewares;

public class ErrorDetails
{
    public string? ErrorType { get; set; }

    public List<string>? Errors { get; set; }

    public override string ToString()
    {
        return JsonConvert.SerializeObject(this);
    }
}