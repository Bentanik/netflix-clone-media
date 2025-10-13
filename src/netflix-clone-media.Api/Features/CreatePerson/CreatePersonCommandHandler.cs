using netflix_clone_media.Api.Infrastructure.Media;
using netflix_clone_media.Api.Messages;

namespace netflix_clone_media.Api.Features.CreatePerson;

public sealed class CreatePersonCommandHandler
    : ICommandHandler<CreatePersonCommand>
{
    private readonly ICommandRepository<Person, Guid> _personRepo;
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserSettings _userSettings;
    private readonly IMediaService _mediaService;

    public CreatePersonCommandHandler
        (ICommandRepository<Person, Guid> personRepo,
        IUnitOfWork unitOfWork,
        IOptions<UserSettings> userOptions,
        IMediaService mediaService)
    {
        _unitOfWork = unitOfWork;
        _personRepo = personRepo;
        _userSettings = userOptions.Value;
        _mediaService = mediaService;
    }

    public async Task<Result<object>> Handle(CreatePersonCommand command, CancellationToken cancellationToken)
    {
        var avatarId = _userSettings.AvatarId;
        var avatarUrl = _userSettings.AvatarUrl;

        if(command.Avatar != null)
        {
           var mediaServiceDto = await _mediaService.UploadFileAsync(command.Avatar, "Avatar", true);
           avatarId = mediaServiceDto.Id.ToString();
           avatarUrl = mediaServiceDto.Url;
        }

        var person = new Person
        {
            FullName = command.FullName,
            OtherName = command.OtherName ?? "",
            ShortBio = command.ShortBio ?? "",
            Gender = command.Gender,
            DateOfBirth = command.DateOfBirth,
            AvatarId = avatarId,
            AvatarUrl = avatarUrl
        };

        await _personRepo.AddAsync(person, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<object>.Success(
            code: MediaTypeMessages.CreateMediaTypesSuccessfully.GetMessage().Code,
            message: MediaTypeMessages.CreateMediaTypesSuccessfully.GetMessage().Message);
    }
}
