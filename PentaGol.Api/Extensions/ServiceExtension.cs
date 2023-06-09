﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using PentaGol.Data.IRepositories;
using PentaGol.Data.Repositories;
using PentaGol.Domain.Entities;
using PentaGol.Service.Interfaces;
using PentaGol.Service.Services;

namespace PentaGol.Api.Extensions;

public static class ServiceExtension
{
    /// <summary>
    /// Add services
    /// </summary>
    /// <param name="services"></param>
    public static void AddCustomServices(this IServiceCollection services)
    {
        services.AddScoped<IRepository<Game>, Repository<Game>>();
        services.AddScoped<IRepository<Liga>, Repository<Liga>>();
        services.AddScoped<IRepository<News>, Repository<News>>();
        services.AddScoped<IRepository<Team>, Repository<Team>>();

        services.AddScoped<IGameService, GameService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<ILigaService, LigaService>();
        services.AddScoped<INewsService, NewsService>();
        services.AddScoped<ITeamService, TeamService>();
    }

    public static void ConfigureJwt(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtSettings = configuration.GetSection("Jwt");

        string key = jwtSettings.GetSection("Key").Value;

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;

            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings.GetSection("Issuer").Value,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))

            };
        });
    }
}
