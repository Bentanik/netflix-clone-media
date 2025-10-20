namespace netflix_clone_media.Api.Messages;

public enum MediaMessages
{
    [Message("Media title exists", "MEDIA_01")]
    MediaTitileAlreadyExists,

    [Message("Validate media success, start proccess", "MEDIA_02")]
    ValidateMediaSuccessfully
}