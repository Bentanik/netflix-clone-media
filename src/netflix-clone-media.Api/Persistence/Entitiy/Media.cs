namespace netflix_clone_media.Api.Persistence.Entitiy;

public class Media : BaseEntity<Guid>
{
    public string Title { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string CoverImageId { get; set; } = default!;
    public string CoverImageUrl { get; set; } = default!;
    public int AgeRating { get; set; } // Example T8, T9
    public string Country { get; set; } = default!;
    public TimeSpan TotalDuration { get; set; }
    public DateTime ReleaseDate { get; set; }

    public MediaCategory Category { get; set; } = MediaCategory.Single;

    public ICollection<MediaPart> Parts { get; set; } = []; // For movies
    public ICollection<Episode> Episodes { get; set; } = []; // For single
    public ICollection<MediaTypeMapping> MediaTypes { get; set; } = [];
    public ICollection<MediaDirector> Directors { get; set; } = [];
    public ICollection<MediaCast> Casts { get; set; } = [];
}