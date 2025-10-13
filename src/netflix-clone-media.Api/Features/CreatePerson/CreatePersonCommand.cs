namespace netflix_clone_media.Api.Features.CreatePerson;

public record CreatePersonCommand(
    string RequestId,
    string FullName,
    string? OtherName,
    string? ShortBio,
    IFormFile? Avatar,
    Gender Gender,
    DateTime DateOfBirth) : IIdempotentRequest, ICommand;
