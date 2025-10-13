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

        group.MapGet("{*objectKey}", HandleMediaAsync);
    }

    private static async Task<IResult> HandleMediaAsync(
    [FromRoute] string objectKey,
    [FromServices] IMediaService mediaService)
    {
        var decodedKey = HttpUtility.UrlDecode(objectKey);
        var keyParts = decodedKey.Split('/');

        var bucketType = keyParts[0];
        var objectPath = string.Join('/', keyParts.Skip(1));

        if (bucketType == IMediaService.PublicBucket)
        {
            var (stream, contentType) = await mediaService.GetFileAsync(IMediaService.PublicBucket, objectPath);
            return Results.File(stream, contentType);
        }

        return Results.NotFound();
    }
}