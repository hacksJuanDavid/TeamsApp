using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Teams.ApiMember.Service.Models;

public class TeamMember
{
    [Key] public int Id { get; set; }

    [Required] public string FirstName { get; set; } = null!;

    [Required] public string LastName { get; set; } = null!;

    [Required] [DataType(DataType.Date)] public DateTime BirthDate { get; set; }

    public string Position { get; set; } = null!;

    public string Phone { get; set; } = null!;

    [Required] public int TeamId { get; set; }
}