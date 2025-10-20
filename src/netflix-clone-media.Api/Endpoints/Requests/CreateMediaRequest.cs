namespace netflix_clone_media.Api.Endpoints.Requests;

public record CreateMediaRequest(
    string Title,
    string Description,
    int AgeRating,
    ICollection<Guid> MediaTypes,
    ICollection<Guid> Countries,
    ICollection<Guid> Directors,
    ICollection<Guid> Casts,
    int ReleaseYear,
    IFormFile Thumbnail,
    IFormFile Video);
