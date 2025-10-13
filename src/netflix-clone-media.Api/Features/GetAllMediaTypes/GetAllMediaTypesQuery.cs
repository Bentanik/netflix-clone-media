namespace netflix_clone_media.Api.Features.GetAllMediaTypes;

public record GetAllMediaTypesQuery(string RequestId) : IQuery<ICollection<MediaTypeDto>>, IIdempotentRequest;
