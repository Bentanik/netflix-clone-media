namespace netflix_clone_media.Api.Endpoints.Requests;

public record CreatePersonRequest(
    string FullName,
    string? OtherName,
    string? ShortBio,
    IFormFile? Avatar,
    Gender Gender,
    DateTime DateOfBirth);
