using netflix_clone_media.Api.Messages;

namespace netflix_clone_media.Api.Features.CreateCountries;

public sealed class CreateCountriesCommandHandler
    : ICommandHandler<CreateCountriesCommand>
{
    private readonly ICommandRepository<Country, Guid> _countryRepo;
    private readonly IUnitOfWork _unitOfWork;

    public CreateCountriesCommandHandler
        (ICommandRepository<Country, Guid> countryRepo,
         IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _countryRepo = countryRepo;
    }

    public async Task<Result<object>> Handle(CreateCountriesCommand command, CancellationToken cancellationToken)
    {
        var existingCountries = await _countryRepo.FindAllAsync(
            predicate: mt => command.Names.Contains(mt.Name),
            isTracking: false,
            cancellationToken);

        if (existingCountries != null && existingCountries.Any())
        {
            var duplicateNames = existingCountries.Select(mt => mt.Name).ToList();

            var error = new Error<List<string>>(
                code: CountryMessages.CountryAlreadyExists.GetMessage().Code,
                message: CountryMessages.CountryAlreadyExists.GetMessage().Message,
                data: duplicateNames);

            return Result<object>.Failure([error]);
        }

        var countries = command.Names.Select(name => new Country
        {
            Name = name
        });

        await _countryRepo.AddRangeAsync(countries, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<object>.Success(
            code: CountryMessages.CreateCountriesSuccessfully.GetMessage().Code,
            message: CountryMessages.CreateCountriesSuccessfully.GetMessage().Message);
    }
}
