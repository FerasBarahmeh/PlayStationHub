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
        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddScoped<IAuthService, AuthService>();
    }
    public static void AddRepositoriesDependencies(ref WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IUserRepository, UserRepository>();
    }

    public static void AddFilters(ref WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<ValidationFilterAttribute>();
    }
}
