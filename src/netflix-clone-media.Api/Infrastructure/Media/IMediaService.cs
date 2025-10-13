namespace netflix_clone_media.Api.Infrastructure.Media;

public interface IMediaService
{
    const string PublicBucket = "public-media";
    const string PrivateBucket = "private-media";

    Task<MediaServiceDto> UploadFileAsync(IFormFile file, string folder, bool? isPublic);
    Task DeleteFileAsync(string key, string bucket);
    Task<(Stream Stream, string ContentType)> GetFileAsync(string bucket, string key);
}
