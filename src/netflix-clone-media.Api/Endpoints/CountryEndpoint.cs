using netflix_clone_media.Api.Features.CreateCountries;
using netflix_clone_media.Api.Features.GetAllCountries;

namespace netflix_clone_media.Api.Endpoints;

public class CountryEndpoint : ICarterModule
{
    private const string BaseUrl = "/api/v{version:apiVersion}/countries";

    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.NewVersionedApi("Countries")
            .MapGroup(BaseUrl).HasApiVersion(1);

        group.MapPost("", HandleCreateCountriesAsync);
        group.MapGet("", HandleGetAllCountriesAsync);
    }

    private static async Task<IResult> HandleCreateCountriesAsync(
      [FromServices] IMessageBus messageBus,
      [FromServices] IRequestContext requestContext,
      [FromBody] CreateCountryRequest request)
    {
        string requestId = requestContext.GetIdempotencyKey()
            ?? throw new AppExceptions.XRequestIdRequiredException();

        var createCountriesCommand = new CreateCountriesCommand(
            RequestId: requestId,
            Names: request.Names
        );

        var result = await messageBus.Send(createCountriesCommand);

        return result.IsFailure ? Results.BadRequest(result) : Results.Ok(result);
    }

    private static async Task<IResult> HandleGetAllCountriesAsync([FromServices] IMessageBus messageBus,
        [FromServices] IRequestContext requestContext)
    {
        string requestId = requestContext.GetIdempotencyKey()
            ?? throw new AppExceptions.XRequestIdRequiredException();

        var getAllCountriesQuery = new GetAllCountriesQuery(RequestId: requestId);

        var result = await messageBus.Send(getAllCountriesQuery);

        return result.IsFailure ? Results.BadRequest(result) : Results.Ok(result);
    }
}
