using PlayStationHub.API.Filters;
using PlayStationHub.Business.Interfaces.Services;
using PlayStationHub.Business.Services;
using PlayStationHub.DataAccess.Interfaces.Repositories;
using PlayStationHub.DataAccess.Repositories;

namespace PlayStationHub.API.Configuration;

public static class DependencyInjection
{
    public static void AddServicesDependencies(ref WebApplicationBuilder builder)
    {
        builder.Services.AddHttpClient();
        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddSingleton<IAuthService, AuthService>();
        builder.Services.AddScoped<IClubService, ClubService>();
        builder.Services.AddScoped<IClubFeedbackService, ClubFeedbackService>();
        builder.Services.AddScoped<IGeminiService, GeminiService>();
        builder.Services.AddScoped<IOwnerService, OwnerService>();
    }
    public static void AddRepositoriesDependencies(ref WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<IClubRepository, ClubRepository>();
        builder.Services.AddScoped<IClubFeedbackRepository, ClubFeedbackRepository>();
        builder.Services.AddScoped<IOwnerRepository, OwnerRepository>();
    }

    public static void AddFilters(ref WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<ValidationFilterAttribute>();
    }
}
