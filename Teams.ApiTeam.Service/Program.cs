using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Teams.ApiTeam.Service.Context;
using Teams.ApiTeam.Service.Extensions;

//  Create the builder with the default configuration
var builder = WebApplication.CreateBuilder(args);

// Add AppDbContext 
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySQL(builder.Configuration.GetConnectionString("CnnStr")!));

// Add services to the container.
builder.Services.AddServices();
builder.Services.AddMapping();
builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();