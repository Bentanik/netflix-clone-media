namespace netflix_clone_media.Api.Features.CreateCountries;

public record CreateCountriesCommand
    (string RequestId, List<string> Names) : IIdempotentRequest, ICommand;
