namespace netflix_clone_media.Api.Features.GetAllCountries;

public record GetAllCountriesQuery(string RequestId) : IQuery<ICollection<CountryDto>>, IIdempotentRequest;
