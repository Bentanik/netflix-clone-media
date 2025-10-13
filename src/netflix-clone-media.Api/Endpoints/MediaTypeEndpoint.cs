using netflix_clone_media.Api.Features.CreateMediaTypes;
using netflix_clone_media.Api.Features.GetAllMediaTypes;

namespace netflix_clone_media.Api.Endpoints;

public class MediaTypeEndpoint : ICarterModule
{
    private const string BaseUrl = "/api/v{version:apiVersion}/types";

    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.NewVersionedApi("Media Types")
            .MapGroup(BaseUrl).HasApiVersion(1);

        group.MapPost("", HandleCreateMediaTypesAsync);
        group.MapGet("", HandleGetAllMediaTypesAsync);
    }

    private static async Task<IResult> HandleCreateMediaTypesAsync(
        [FromServices] IMessageBus messageBus,
        [FromServices] IRequestContext requestContext,
        [FromBody] CreateMediaTypesRequest request)
    {
        string requestId = requestContext.GetIdempotencyKey()
            ?? throw new AppExceptions.XRequestIdRequiredException();

        var createMediaTypesCommand = new CreateMediaTypesCommand(
            RequestId: requestId,
            Names: request.Names
        );

        var result = await messageBus.Send(createMediaTypesCommand);

        return result.IsFailure ? Results.BadRequest(result) : Results.Ok(result);
    }

    private static async Task<IResult> HandleGetAllMediaTypesAsync([FromServices] IMessageBus messageBus,
        [FromServices] IRequestContext requestContext)
    {
        string requestId = requestContext.GetIdempotencyKey()
            ?? throw new AppExceptions.XRequestIdRequiredException();

        var getAllMediaTypesQuery = new GetAllMediaTypesQuery(RequestId: requestId);

        var result = await messageBus.Send(getAllMediaTypesQuery);

        return result.IsFailure ? Results.BadRequest(result) : Results.Ok(result);
    }
}
