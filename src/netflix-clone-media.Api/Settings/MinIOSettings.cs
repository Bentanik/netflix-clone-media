namespace netflix_clone_media.Api.Settings;

public class MinIOSettings
{
    public const string SectionName = "MinIOSettings";
    public string Endpoint { get; set; } = default!;
    public string AccessKey { get; set; } = default!;
    public string SecretKey { get; set; } = default!;
    public bool UseSSL { get; set; } = default!;
}