using Microsoft.AspNetCore.Mvc;

namespace PlayStationHub.API.Configuration;

public static class Configure
{
    public static void APIConfigure(ref WebApplicationBuilder builder)
    {
        builder.Services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);
    }
}
