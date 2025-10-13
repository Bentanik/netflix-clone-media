using netflix_clone_media.Api.Messages;

namespace netflix_clone_media.Api.Features.GetAllMediaTypes;

public sealed class GetAllMediaTypesQueryHandler : IQueryHandler<GetAllMediaTypesQuery, ICollection<MediaTypeDto>>
{
    private readonly IQueryRepository<MediaType> _mediaTypeRepo;

    public GetAllMediaTypesQueryHandler(IQueryRepository<MediaType> mediaTypeRepo)
    {
        _mediaTypeRepo = mediaTypeRepo;
    }

    public async Task<Result<ICollection<MediaTypeDto>>> Handle(GetAllMediaTypesQuery query, CancellationToken cancellationToken)
    {
        var mediaTypes = await _mediaTypeRepo.GetAllAsync(cancellationToken: cancellationToken);

        var mediaTypeDtos = mediaTypes
            .Select(mt => new MediaTypeDto(mt.Name))
            .ToList();

        return Result<ICollection<MediaTypeDto>>.Success(
            data: mediaTypeDtos,
            code: MediaTypeMessages.GetAllMediaTypesSuccessfully.GetMessage().Code,
            message: MediaTypeMessages.GetAllMediaTypesSuccessfully.GetMessage().Message
        );
    }
}
