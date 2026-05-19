using System.Text;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Legacy.Security.Authorization;
using Legacy.Shared.Authentication;
using Legacy.Shared.Constants;
using Legacy.Shared.Constants.Configurations;
using Legacy.Shared.Cors;
using Legacy.WebHost;
using Legacy.WebHost.Extensions;
using Legacy.WebHost.Registrations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

// Load Framework Components and Libraries Configuration.
config.AddJsonFile(FrameworkConfiguration.Filename, optional: false, reloadOnChange: true);

// Load Module Configuration
config.AddJsonFile(ModuleConfigurationConstants.ConfigurationFile, optional: false, reloadOnChange: true);

var jwtOptions = config.GetSection(string.Concat("Authentication:", SharedConstant.JwtOptions)).Get<JwtOptions>();
var origins = config.GetSection("Authentication:AllowedOrigins").Get<string[]>();

// Adding Token Authentication
//builder.Services.AddAuthentication(options =>
//{
//    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
//}).AddJwtBearer(bearerOptions =>
//{
//   // Setting Bear Options
//   var key = Encoding.UTF8.GetBytes(jwtOptions!.SigningKey);

//   bearerOptions.TokenValidationParameters = new TokenValidationParameters
//   {
//       ValidateIssuer = true,
//       ValidIssuer = jwtOptions.Issuer,
//       ValidateAudience = true,
//       ValidAudience = jwtOptions.Audience,
//       ValidateIssuerSigningKey = true,
//       IssuerSigningKey = new SymmetricSecurityKey(key),
//       ValidateLifetime = true
//   };
//});

// Add services to the container.
builder.Services.AddControllers();

// Framework Libraries
builder.Services.AddSharedFrameworkLibraries();

// Add and Scan runtime controllers
builder.Services.AddRuntimeControllers();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
//builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(c =>
{
    c.AddDefaultPolicy(options =>
    {
        //options.WithOrigins(origins!);
        options.WithMethods(CorsConfiguration.AllowedMethods);
        options.AllowAnyHeader();
        options.AllowCredentials();
    });
});

//builder.Services.AddSystemAuthorization();

builder.Host
    .UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .ConfigureContainer<ContainerBuilder>(cb =>
    {
        cb.RegisterModule(new AssemblyScanningModule());
    });

builder.Services.AddSwaggerGen(options =>
{
    options.CustomSchemaIds(type => type.ToString());
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.Http, 
        Description = "Enter 'Bearer' followed by a space and the JWT token.",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Scheme = "Bearer",
        BearerFormat = "JWT",
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    //app.MapOpenApi();
}

app.UseHttpsRedirection();

//app.UseStaticFiles(new StaticFileOptions
//{
//    FileProvider = new PhysicalFileProvider(
//        Path.Combine(app.Environment.ContentRootPath, "Uploads")),
//    RequestPath = "/Uploads"
//});

app.UseCors();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
