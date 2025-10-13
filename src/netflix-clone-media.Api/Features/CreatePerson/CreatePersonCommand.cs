namespace netflix_clone_media.Api.Features.CreatePerson;

public record CreatePersonCommand(
    string RequestId,
    string FullName,
    string? OtherName,
    string? ShortBio,
    IFormFile? Avatar,
    Gender Gender,
    int Day,
    int Month,
    int Year) : IIdempotentRequest, ICommand;
