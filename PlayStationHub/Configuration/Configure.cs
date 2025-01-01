using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PlayStationHub.API.Authentication;
using PlayStationHub.Business.Authentication;
using PlayStationHub.Business.Enums;
using System.Net;
using System.Text.Json;
using Utilities.Response;

namespace PlayStationHub.API.Configuration;

public static class Configure
{
    public static void APIConfigure(ref WebApplicationBuilder builder)
    {
        builder.Services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);
    }
    public static void JwtAuthentication(ref WebApplicationBuilder builder)
    {
        var JWTOptions = builder.Configuration.GetSection("Jwt").Get<JwtOptions>();
        builder.Services.AddAuthentication()
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = JWTOptions.Issuer,
                    ValidAudience = JWTOptions.Audience,
                    IssuerSigningKey = AuthenticationHelper.GetSymmetricSecurityKey(JWTOptions.SigningKey)
                };
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = ctx =>
                    {
                        ctx.Request.Cookies.TryGetValue("jwtToken", out var jwtToken);
                        if (!string.IsNullOrEmpty(jwtToken))
                        {
                            ctx.Token = jwtToken;
                        }

                        return Task.CompletedTask;
                    },
                    OnForbidden = context =>
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                        context.Response.ContentType = "application/json";
                        var result = JsonSerializer.Serialize(new NullableResponseData(HttpStatusCode.Forbidden, "You are not authorized to access this resource."));
                        return context.Response.WriteAsync(result);
                    },

                };
            });

        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy(nameof(Policies.AdminOnly), policy => policy.RequireRole(nameof(Privileges.Admin)));
            options.AddPolicy(nameof(Policies.OwnerOnly), policy => policy.RequireRole(nameof(Privileges.Owner)));
            options.AddPolicy(nameof(Policies.UserOnly), policy => policy.RequireRole(nameof(Privileges.User)));
        });
        builder.Services.AddSingleton(JWTOptions);
        builder.Services.AddHttpContextAccessor();
        builder.Services.AddSingleton<ClaimsHelper>();
    }
    public static void AddCORS(ref WebApplicationBuilder builder)
    {
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowSpecificOrigin",
                builder =>
                {
                    builder.WithOrigins("https://play-station-hub-frontend.vercel.app", "http://localhost:4200")
                           .AllowAnyMethod()
                           .AllowAnyHeader()
                           .AllowCredentials();
                });
        });
    }
    public static void AddControllers(ref WebApplicationBuilder builder)
    {
        builder.Services.AddControllers();

        builder.Services.AddValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
        builder.Services.AddFluentValidationAutoValidation(options =>
        {
            options.DisableDataAnnotationsValidation = true;
        });
    }
}
