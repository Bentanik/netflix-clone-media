namespace netflix_clone_media.Api.Features.CreateMediaTypes;

public record CreateMediaTypesCommand
    (string RequestId, List<string> Names) : IIdempotentRequest, ICommand;
