using netflix_clone_media.Api.Features.CreateMedia;

namespace netflix_clone_media.Api.Endpoints;

public class MediaEndpoint : ICarterModule
{
    private const string BaseUrl = "/api/v{version:apiVersion}/medias";

    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.NewVersionedApi("Medias")
            .MapGroup(BaseUrl).HasApiVersion(1);

        group.MapPost("", HandleCreateMediaAsync).DisableAntiforgery();
    }

    [Consumes("multipart/form-data")]
    private static async Task<IResult> HandleCreateMediaAsync(
      [FromServices] IMessageBus messageBus,
      [FromServices] IRequestContext requestContext,
      [FromForm] CreateMediaRequest request)
    {
        string requestId = requestContext.GetIdempotencyKey()
            ?? throw new AppExceptions.XRequestIdRequiredException();

        var createMediaTypesCommand = new CreateMediaCommand(
            RequestId: requestId,
            Title: request.Title,
            Description: request.Description,
            Thumbnail: request.Thumbnail,
            Video: request.Video,
            AgeRating: request.AgeRating,
            MediaTypes: request.MediaTypes,
            Countries: request.Countries,
            Directors: request.Directors,
            Casts: request.Casts,
            ReleaseYear: request.ReleaseYear
        );

        var result = await messageBus.Send(createMediaTypesCommand);

        return result.IsFailure ? Results.BadRequest(result) : Results.Ok(result);
    }
}
