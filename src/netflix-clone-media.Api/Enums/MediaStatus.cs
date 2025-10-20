namespace netflix_clone_media.Api.Enums;

public enum MediaStatus
{
    Pending = 0,        // Newly created, upload not finished or encoding not started
    IsProcessing = 1,   // Currently encoding (FFmpeg is running)
    IsPublished = 2,    // Encoding completed, ready to be published
    IsRejected = 3,     // Failed or invalid (encoding failed, invalid file)
    IsArchived = 4      // (optional) Old or hidden video
}