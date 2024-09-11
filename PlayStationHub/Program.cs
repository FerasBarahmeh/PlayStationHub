using FluentValidation.AspNetCore;
using PlayStationHub.API.Configuration;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllers()
    .AddFluentValidation(fv =>
    {
        fv.RegisterValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
        fv.DisableDataAnnotationsValidation = true;

        // Optional: Disable automatic child property validation (disabled by default)
        // fv.ImplicitlyValidateChildProperties = false;

        // Optional: Disable implicit root collection element validation (disabled by default)
        // fv.ImplicitlyValidateRootCollectionElements = false;
    });


// API Configurations
Configure.APIConfigure(ref builder);


DependencyInjection.AddFilters(ref builder);
DependencyInjection.AddServicesDependencies(ref builder);
DependencyInjection.AddRepositoriesDependencies(ref builder);
DependencyInjection.AddAutoMappersDependencies(ref builder);



var app = builder.Build();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
