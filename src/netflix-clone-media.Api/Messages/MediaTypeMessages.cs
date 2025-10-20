namespace netflix_clone_media.Api.Messages;

public enum MediaTypeMessages
{
    [Message("Types exists", "MEDIA_TYPE_01")]
    MediaTypeAlreadyExists,

    [Message("Create media types successfully", "MEDIA_TYPE_02")]
    CreateMediaTypesSuccessfully,

    [Message("Get all media types successfully", "MEDIA_TYPE_03")]
    GetAllMediaTypesSuccessfully,

    [Message("Media type not found", "MEDIA_TYPE_04")]
    MediaTypeNotFound
}