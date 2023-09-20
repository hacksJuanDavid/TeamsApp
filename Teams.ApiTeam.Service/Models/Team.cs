using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Teams.ApiTeam.Service.Models;

public sealed class Team
{
    [Key] public int Id { get; set; }

    [Required] public string Name { get; set; } = null!;

    public string Coach { get; set; } = null!;

    public string Conference { get; set; } = null!;
    
    [NotMapped]
    public List<int> TeamMember { get; set; } = new List<int>();
}