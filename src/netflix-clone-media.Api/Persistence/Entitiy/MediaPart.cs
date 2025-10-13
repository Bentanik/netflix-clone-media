namespace netflix_clone_media.Api.Persistence.Entitiy;

public class MediaPart : BaseEntity<Guid>
{
    public string Title { get; set; } = default!; // E.g., "Season 1", "Volume 1"
    public int PartNumber { get; set; } // E.g., 1, 2, 3

    public Guid MediaId { get; set; }
    public Media Media { get; set; } = default!;

    public ICollection<Episode> Episodes { get; set; } = [];
}