using Microsoft.EntityFrameworkCore;
using QuizUp.DAL.Data;
using QuizUp.IdentityServer;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

Log.Information("Starting up");

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Host.UseSerilog((ctx, lc) => lc
        .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}")
        .Enrich.FromLogContext()
        .ReadFrom.Configuration(ctx.Configuration));

    var app = builder
        .ConfigureServices()
        .ConfigurePipeline();

    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        var applicationDbContext = services.GetRequiredService<ApplicationDbContext>();
        applicationDbContext.Database.Migrate();

        var dataInitializer = services.GetRequiredService<DataInitializer>();
        await dataInitializer.Seed();
    }

    app.Run();
}
catch (Exception ex) when (ex is not HostAbortedException)
{
    Log.Fatal(ex, "Unhandled exception");
}
finally
{
    Log.Information("Shut down complete");
    Log.CloseAndFlush();
}
