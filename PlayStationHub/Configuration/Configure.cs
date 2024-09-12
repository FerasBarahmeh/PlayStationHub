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
                    ValidIssuer = JWTOptions.Issuer,

                    ValidateAudience = true,
                    ValidAudience = JWTOptions.Audience,

                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = BaseAuthenticationConfig.GetSymmetricSecurityKey(JWTOptions.SigningKey)
                };
            });

        builder.Services.AddSingleton(JWTOptions);

    }
}
