using netflix_clone_media.Api.Features.CreatePerson;

namespace netflix_clone_media.Api.Endpoints;

public class MediaEndpoint : ICarterModule
{
    private const string BaseUrl = "/api/v{version:apiVersion}/media";

    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.NewVersionedApi("Media")
            .MapGroup(BaseUrl).HasApiVersion(1);

        group.MapPost("", HandleCreateMediaAsync).DisableAntiforgery();
    }

    [Consumes("multipart/form-data")]
    private static async Task<IResult> HandleCreateMediaAsync(
      [FromServices] IMessageBus messageBus,
      [FromServices] IRequestContext requestContext,
      [FromForm] CreatePersonRequest request)
    {
        string requestId = requestContext.GetIdempotencyKey()
            ?? throw new AppExceptions.XRequestIdRequiredException();

        var createMediaTypesCommand = new CreatePersonCommand(
            RequestId: requestId,
            FullName: request.FullName,
            OtherName: request.OtherName,
            ShortBio: request.ShortBio,
            Avatar: request.Avatar,
            Gender: request.Gender,
            Day: request.Day,
            Month: request.Month,
            Year: request.Year
        );

        var result = await messageBus.Send(createMediaTypesCommand);

        return result.IsFailure ? Results.BadRequest(result) : Results.Ok(result);
    }

}
