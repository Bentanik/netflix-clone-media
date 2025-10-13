using netflix_clone_media.Api.Features.CreatePerson;

namespace netflix_clone_media.Api.Endpoints;

public class PersonEndpoint : ICarterModule
{
    private const string BaseUrl = "/api/v{version:apiVersion}/persons";

    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.NewVersionedApi("Person")
            .MapGroup(BaseUrl).HasApiVersion(1);

        group.MapPost("", HandleCreatePersonAsync).DisableAntiforgery();
    }

    [Consumes("multipart/form-data")]
    private static async Task<IResult> HandleCreatePersonAsync(
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
            DateOfBirth: DateTime.UtcNow
        );

        var result = await messageBus.Send(createMediaTypesCommand);

        return result.IsFailure ? Results.BadRequest(result) : Results.Ok(result);
    }
}
