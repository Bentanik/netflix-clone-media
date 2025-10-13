using System.Web;
using netflix_clone_media.Api.Infrastructure.Media;

namespace netflix_clone_media.Api.Endpoints;

public class MediaProxyEndpoint : ICarterModule
{
    private const string BaseUrl = "/api/v{version:apiVersion}/media-proxy";

    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.NewVersionedApi("Media Proxy")
            .MapGroup(BaseUrl)
            .HasApiVersion(1);

        group.MapGet("public/{*objectKey}", HandlePublicMediaAsync);
        group.MapGet("private/{*objectKey}", HandlePrivateMediaAsync);
    }

    private static async Task<IResult> HandlePublicMediaAsync(
        [FromRoute] string objectKey,
        [FromServices] IMediaService mediaService)
    {
        var decodedKey = HttpUtility.UrlDecode(objectKey);
        var (stream, contentType) = await mediaService.GetFileAsync("public-media", decodedKey);
        return Results.File(stream, contentType);
    }

    private static async Task<IResult> HandlePrivateMediaAsync(
        [FromRoute] string objectKey,
        [FromServices] IMediaService mediaService)
        //[FromServices] IUserContext userContext)
    {
        // check quyền userContext ở đây

        var (stream, contentType) = await mediaService.GetFileAsync("private-media", objectKey);
        return Results.File(stream, contentType);
    }
}