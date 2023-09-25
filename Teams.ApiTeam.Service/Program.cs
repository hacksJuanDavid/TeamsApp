using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Teams.ApiTeam.Service.Context;
using Teams.ApiTeam.Service.Extensions;
using Teams.ApiTeam.Service.Middlewares;
using Teams.ApiTeam.Service.Settings;

//  Create the builder with the default configuration
var builder = WebApplication.CreateBuilder(args);

// Add AppSettings
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

// Add AppDbContext 
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySQL(builder.Configuration.GetConnectionString("CnnStr")!));

// Add services to the container.
builder.Services.AddServices();

// Add AutoMapper
builder.Services.AddMapping();

// Add FluentValidation
builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();

// Add Controllers
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// App configuration build
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Add exception handler
app.ConfigureExceptionHandler();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();