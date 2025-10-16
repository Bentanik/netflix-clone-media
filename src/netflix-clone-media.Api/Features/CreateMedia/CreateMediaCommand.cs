namespace netflix_clone_media.Api.Features.CreateMedia;

public record CreateMediaCommand
    (string RequestId,
     string Title,
     string Description,
     IFormFile? Thumnail,
     int AgeRating,
     ICollection<Guid> Categories) : ICommand;
