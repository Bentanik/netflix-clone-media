using netflix_clone_media.Api.DependencyInjection.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.AddApplicationServices();

var app = builder.Build();

app.ConfigureMiddleware();

app.Run();
