using Autofac;
using Autofac.Extensions.DependencyInjection;
using QuizUp.Server;
using QuizUp.Server.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddSignalR();

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    DependencyInjection.RegisterServices(containerBuilder);
});

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();


app.MapHub<QuizHub>("/quizHub");

app.Run();
