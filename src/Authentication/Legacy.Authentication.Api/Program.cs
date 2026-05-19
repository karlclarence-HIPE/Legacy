using Legacy.Authentication.Application;
using Legacy.Authentication.Application.Configuration;
using Legacy.Shared.Constants;
using Legacy.Shared.Constants.Configurations;
using Legacy.Shared.Cors;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

config.AddJsonFile(ModuleConfigurationConstants.ConfigurationFile, optional: false, reloadOnChange: true);

var origins = config.GetSection(SharedConstant.Origins).Get<string[]>();
// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddSwaggerGen();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

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
