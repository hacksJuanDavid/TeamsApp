using System.ComponentModel.DataAnnotations;
using Teams.ApiMember.Service.Models;

namespace Teams.ApiTeam.Service.Models;

public class Team
{
    [Key] public int Id { get; set; }

    [Required] public string Name { get; set; } = null!;

    public string Coach { get; set; } = null!;

    public string Conference { get; set; } = null!;

    public virtual List<TeamMember> Members { get; set; } = new List<TeamMember>();
}