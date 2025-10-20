namespace netflix_clone_media.Api.Features.CreateMedia;

public record CreateMediaCommand
    (string RequestId,
     string Title,
     string Description,
     IFormFile Thumbnail,
     IFormFile Video,
     int AgeRating,
     ICollection<Guid> MediaTypes,
     ICollection<Guid> Countries,
     ICollection<Guid> Directors,
     ICollection<Guid> Casts,
     int ReleaseYear) : ICommand;
