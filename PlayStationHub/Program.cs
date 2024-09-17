using PlayStationHub.API.Configuration;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
Configure.APIConfigure(ref builder);
Configure.JwtAuthentication(ref builder);
Configure.AddCORS(ref builder);
Configure.AddControllers(ref builder);


DependencyInjection.AddFilters(ref builder);
DependencyInjection.AddServicesDependencies(ref builder);
DependencyInjection.AddRepositoriesDependencies(ref builder);
DependencyInjection.AddAutoMappersDependencies(ref builder);



var app = builder.Build();
app.UseCors("AllowSpecificOrigin");
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
