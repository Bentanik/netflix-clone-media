namespace netflix_clone_media.Api.Settings;

public class UserSettings
{
    public const string SectionName = "UserSettings";
    public string AvatarUrl { get; set; } = default!;
    public string AvatarId { get; set; } = default!;
}