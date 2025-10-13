namespace netflix_clone_media.Api.Persistence.Entitiy;

public class Episode : BaseEntity<Guid>
{
    public string Title { get; set; } = default!;
    public int EpisodeNumber { get; set; }
    public string Description { get; set; } = default!;
    public TimeSpan Duration { get; set; }
    public string VideoId { get; set; } = default!;
    public string VideoUrl { get; set; } = default!;
    public string ThumbnailId { get; set; } = default!;
    public string ThumbnailUrl { get; set; } = default!;

    public Guid? MediaPartId { get; set; } // nullable if episode belongs to a single media
    public MediaPart MediaPart { get; set; } = default!;

    public Guid MediaId { get; set; }
    public Media Media { get; set; } = default!;
}
