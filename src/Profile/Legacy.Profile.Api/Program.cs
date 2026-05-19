using Legacy.Framework.FileManager.Configuration;
using Legacy.Framework.FileManager.Extensions;
using Legacy.Framework.Utility.Configuration;
using Legacy.Framework.Utility.Extensions;
using Legacy.Profile.Application;
using Legacy.Profile.Application.Configuration;
using Legacy.Shared.Constants.Configurations;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

config.AddJsonFile(FrameworkConfiguration.Filename, optional: false, reloadOnChange: true);

config.AddJsonFile(ModuleConfigurationConstants.ConfigurationFile, optional: false, reloadOnChange: true);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddLogging();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

builder.Services
    .AddProfileModule<ModuleConfigurationOptions>()
    .AddProfileDatabaseModule();

builder.Services.AddFileManager<FileManagerConfigurationOption>();
builder.Services.AddUtilities<UtilityConfigurationOptions>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
