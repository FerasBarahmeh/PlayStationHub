using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PlayStationHub.API.Authentication;

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
                    IssuerSigningKey = BaseAuthenticationConfig.GetSymmetricSecurityKey(JWTOptions.SigningKey)
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
                };
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
                    builder.WithOrigins("http://localhost:4200")
                           .AllowAnyMethod()
                           .AllowAnyHeader()
                           .AllowCredentials();
                });
        });
    }
    public static void AddControllers(ref WebApplicationBuilder builder)
    {
        builder.Services.AddControllers()
            .AddFluentValidation(fv =>
            {
                fv.RegisterValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
                fv.DisableDataAnnotationsValidation = true;
            });

    }
}
