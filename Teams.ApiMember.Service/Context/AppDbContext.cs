using Microsoft.EntityFrameworkCore;
using Teams.ApiMember.Service.Models;

namespace Teams.ApiMember.Service.Context;

public class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<TeamMember> TeamMembers { get; set; }
}