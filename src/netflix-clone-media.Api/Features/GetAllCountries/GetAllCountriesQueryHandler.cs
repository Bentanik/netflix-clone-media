using netflix_clone_media.Api.Messages;

namespace netflix_clone_media.Api.Features.GetAllCountries;

public sealed class GetAllCountriesQueryHandler : IQueryHandler<GetAllCountriesQuery, ICollection<CountryDto>>
{
    private readonly IQueryRepository<Country> _countryRepo;

    public GetAllCountriesQueryHandler(IQueryRepository<Country> countryRepo)
    {
        _countryRepo = countryRepo;
    }

    public async Task<Result<ICollection<CountryDto>>> Handle(GetAllCountriesQuery query, CancellationToken cancellationToken)
    {
        var countries = await _countryRepo.GetAllAsync(cancellationToken: cancellationToken);

        var countryDtos = countries
            .Select(mt => new CountryDto(mt.Name))
            .ToList();

        return Result<ICollection<CountryDto>>.Success(
            data: countryDtos,
            code: CountryMessages.GetAllCountriesSuccessfully.GetMessage().Code,
            message: CountryMessages.GetAllCountriesSuccessfully.GetMessage().Message
        );
    }
}
