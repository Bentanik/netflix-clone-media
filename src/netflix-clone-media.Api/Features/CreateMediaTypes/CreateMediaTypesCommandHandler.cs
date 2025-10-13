using netflix_clone_media.Api.Messages;

namespace netflix_clone_media.Api.Features.CreateMediaTypes;

public sealed class CreateMediaTypesCommandHandler
    : ICommandHandler<CreateMediaTypesCommand>
{
    private readonly ICommandRepository<MediaType, Guid> _mediaTypeRepo;
    private readonly IUnitOfWork _unitOfWork;

    public CreateMediaTypesCommandHandler
        (ICommandRepository<MediaType, Guid> mediaTypeRepo,
        IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _mediaTypeRepo = mediaTypeRepo;
    }

    public async Task<Result<object>> Handle(CreateMediaTypesCommand command, CancellationToken cancellationToken)
    {
        var existingMediaTypes = await _mediaTypeRepo.FindAllAsync(
            predicate: mt => command.Names.Contains(mt.Name),
            isTracking: false,
            cancellationToken);

        if (existingMediaTypes != null && existingMediaTypes.Any())
        {
            var duplicateNames = existingMediaTypes.Select(mt => mt.Name).ToList();

            var error = new Error<List<string>>(
                code: MediaTypeMessages.MediaTypeAlreadyExists.GetMessage().Code,
                message: MediaTypeMessages.MediaTypeAlreadyExists.GetMessage().Message,
                data: duplicateNames);

            return Result<object>.Failure([error]);
        }

        var newMediaTypes = command.Names.Select(name => new MediaType
        {
            Id = Guid.NewGuid(),
            Name = name
        });

        await _mediaTypeRepo.AddRangeAsync(newMediaTypes, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<object>.Success(
            code: MediaTypeMessages.CreateMediaTypesSuccessfully.GetMessage().Code,
            message: MediaTypeMessages.CreateMediaTypesSuccessfully.GetMessage().Message);
    }
}
