namespace netflix_clone_media.Api.DependencyInjection.Extensions;

public static class MiddlewareExtensions
{
    public static void ConfigureMiddleware(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.ConfigureSwagger();
        }

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapCarter();
        app.UseMiddleware<ExceptionHandlingMiddleware>();

        app.NewVersionedApi();
    }
}