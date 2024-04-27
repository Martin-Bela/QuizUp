using Autofac;
using Autofac.Extensions.DependencyInjection;
using QuizUp.Server;
using QuizUp.Server.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddSignalR().AddJsonProtocol();

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    DependencyInjection.RegisterServices(containerBuilder);
});

#if DEBUG
builder.Logging.SetMinimumLevel(LogLevel.Trace);
builder.Logging.AddFilter("Microsoft.AspNetCore.SignalR", LogLevel.Trace);
builder.Logging.AddFilter("Microsoft.AspNetCore.Http.Connections", LogLevel.Trace);
#endif

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();


app.MapHub<QuizHub>("/quizHub");

app.Run();
