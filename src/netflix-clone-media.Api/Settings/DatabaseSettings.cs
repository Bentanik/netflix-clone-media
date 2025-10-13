namespace netflix_clone_media.Api.Settings;

public class DatabaseSettings
{
    public const string SectionName = "DatabaseSettings";
    public string ConnectionString { get; set; } = default!;
}