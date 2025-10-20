using netflix_clone_media.Api.Infrastructure.Media;
using netflix_clone_media.Api.Messages;

namespace netflix_clone_media.Api.Features.CreateMedia;

public sealed class CreateMediaCommandHandler
    : ICommandHandler<CreateMediaCommand>
{
    private readonly ICommandRepository<MediaType, Guid> _mediaTypeRepo;
    private readonly ICommandRepository<Country, Guid> _countryRepo;
    private readonly ICommandRepository<Person, Guid> _personRepo;
    private readonly ICommandRepository<Media, Guid> _mediaRepo;

    private readonly IMediaService _mediaService;
    private readonly IUnitOfWork _unitOfWork;

    public CreateMediaCommandHandler(
        ICommandRepository<MediaType, Guid> mediaTypeRepo,
        ICommandRepository<Country, Guid> countryRepo,
        ICommandRepository<Person, Guid> personRepo,
        ICommandRepository<Media, Guid> mediaRepo,
        IMediaService mediaService,
        IUnitOfWork unitOfWork)
    {
        _mediaTypeRepo = mediaTypeRepo;
        _countryRepo = countryRepo;
        _personRepo = personRepo;
        _mediaRepo = mediaRepo;
        _mediaService = mediaService;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<object>> Handle(CreateMediaCommand command, CancellationToken cancellationToken)
    {
        // Validate Media Types
        var mediaTypes = await _mediaTypeRepo.AnyAsync(
            predicate: mt => command.MediaTypes.Contains(mt.Id),
            isTracking: false,
            cancellationToken);

        if (!mediaTypes)
        {
            var error = new Error<object>(
                code: MediaTypeMessages.MediaTypeNotFound.GetMessage().Code,
                message: MediaTypeMessages.MediaTypeNotFound.GetMessage().Message,
                data: null);
            return Result<object>.Failure([error]);
        }

        // Validate Countries
        var countries = await _countryRepo.AnyAsync(
            predicate: c => command.Countries.Contains(c.Id),
            isTracking: false,
            cancellationToken);
        if (!countries)
        {
            var error = new Error<object>(
                code: CountryMessages.CountryNotFound.GetMessage().Code,
                message: CountryMessages.CountryNotFound.GetMessage().Message,
                data: null);

            return Result<object>.Failure([error]);
        }

        // Validate Directors And Casts
        var persons = await _personRepo.AnyAsync(
            predicate: p => command.Directors.Concat(command.Casts).Contains(p.Id),
            isTracking: false,
            cancellationToken);

        if (!persons)
        {
            var error = new Error<object>(
                code: PersonMessages.PersonNotFound.GetMessage().Code,
                message: PersonMessages.PersonNotFound.GetMessage().Message,
                data: null);
            return Result<object>.Failure([error]);
        }

        // Validate Media
        var existingMedia = await _mediaRepo.AnyAsync(
            predicate: m => m.Title == command.Title && m.ReleaseYear == command.ReleaseYear,
            isTracking: false,
            cancellationToken);

        if (existingMedia)
        {
            var error = new Error<object>(
                 code: MediaMessages.MediaTitileAlreadyExists.GetMessage().Code,
                 message: MediaMessages.MediaTitileAlreadyExists.GetMessage().Message,
                 data: null);
            return Result<object>.Failure([error]);
        }

        // Upload Thumnail Media
        var thumnailUploaded = await _mediaService.UploadFileAsync(
            file: command.Thumbnail,
            folder: $"Medias/{command.Title}",
            isPublic: true);

        // Upload Original Media
        await _mediaService.UploadFileAsync(
            file: command.Video,
            folder: $"Medias/{command.Title}",
            isPublic: false);

        var mediaId = Guid.NewGuid();

        // Create Media Types Relation
        var mediaMediaTypes = command.MediaTypes.Select(mtId => new MediaTypeMapping
        {
            Id = Guid.NewGuid(),
            MediaId = mediaId,
            MediaTypeId = mtId
        });

        // Create Countries Relation
        var mediaCountries = command.Countries.Select(cId => new MediaCountries
        {
            Id = Guid.NewGuid(),
            MediaId = mediaId,
            CountryId = cId
        });

        // Create Casts Relation
        var mediaCasts = command.Casts.Select(cId => new MediaCast
        {
            Id = Guid.NewGuid(),
            MediaId = mediaId,
            PersonId = cId,
        });

        // Create Directors Relation
        var mediaDirectors = command.Directors.Select(dId => new MediaDirector
        {
            Id = Guid.NewGuid(),
            MediaId = mediaId,
            PersonId = dId,
        });

        // Save Media to Database
        var newMedia = new Media
        {
            Id = Guid.NewGuid(),
            Title = command.Title,
            Description = command.Description,
            AgeRating = command.AgeRating,
            CoverThumnailId = thumnailUploaded.Id,
            CoverThumnailUrl = thumnailUploaded.Url,
            ReleaseYear = command.ReleaseYear,
            TotalDuration = TimeSpan.Zero,
            MediaTypes = [.. mediaMediaTypes],
            MediaCountries = [.. mediaCountries],
            Casts = [.. mediaCasts],
            Directors = [.. mediaDirectors],
            Status = MediaStatus.Pending,
        };

        await _mediaRepo.AddAsync(newMedia, cancellationToken);

        return Result<object>.Success(
            code: MediaMessages.ValidateMediaSuccessfully.GetMessage().Code,
            message: MediaMessages.ValidateMediaSuccessfully.GetMessage().Message);
    }
}
