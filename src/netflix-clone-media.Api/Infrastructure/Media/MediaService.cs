namespace netflix_clone_media.Api.Infrastructure.Media;

public class MediaService : IMediaService
{
    private readonly MinioClient _minio;
    private readonly string _publicBucket = "public-media";
    private readonly string _privateBucket = "private-media";

    public MediaService(IOptions<MinIOSettings> options, IConfiguration configuration)
    {
        var settings = options.Value;

        _minio = new MinioClient();
        _minio
            .WithEndpoint(settings.Endpoint)
            .WithCredentials(settings.AccessKey, settings.SecretKey)
            .WithSSL(settings.UseSSL)
            .Build();
    }

    public async Task<MediaServiceDto> UploadFileAsync(IFormFile file, string folder, bool? isPublic)
    {
        bool usePublic = isPublic ?? false;
        var bucket = usePublic ? _publicBucket : _privateBucket;
        var key = $"{folder}/{Guid.NewGuid()}_{file.FileName}";

        // Ensure bucket exists
        bool found = await _minio.BucketExistsAsync(new BucketExistsArgs().WithBucket(bucket));
        if (!found)
        {
            await _minio.MakeBucketAsync(new MakeBucketArgs().WithBucket(bucket));
        }

        // Upload
        using var stream = file.OpenReadStream();
        await _minio.PutObjectAsync(new PutObjectArgs()
            .WithBucket(bucket)
            .WithObject(key)
            .WithStreamData(stream)
            .WithObjectSize(file.Length)
            .WithContentType(file.ContentType));

        return new MediaServiceDto(
            Id: key,
            Url: $"{(usePublic ? "public" : "private")}/{key}",
            Bucket: bucket,
            IsPublic: usePublic
        );
    }

    public async Task DeleteFileAsync(string key, string bucket)
    {
        await _minio.RemoveObjectAsync(new RemoveObjectArgs()
            .WithBucket(bucket)
            .WithObject(key));
    }

    public async Task<(Stream Stream, string ContentType)> GetFileAsync(string bucket, string key)
    {
        var memory = new MemoryStream();

        var stat = await _minio.StatObjectAsync(
            new StatObjectArgs()
                .WithBucket(bucket)
                .WithObject(key)
        );

        await _minio.GetObjectAsync(new GetObjectArgs()
            .WithBucket(bucket)
            .WithObject(key)
            .WithCallbackStream(stream => stream.CopyTo(memory))
        );

        memory.Position = 0;
        return (memory, stat.ContentType);
    }
}