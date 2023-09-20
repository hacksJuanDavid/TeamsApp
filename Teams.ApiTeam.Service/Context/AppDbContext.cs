using Microsoft.EntityFrameworkCore;
using Teams.ApiTeam.Service.Models;

namespace Teams.ApiTeam.Service.Context;

public class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Team> Teams { get; set; }
}