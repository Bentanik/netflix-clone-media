namespace netflix_clone_media.Api.Persistence.Entitiy;

public class MediaTypeMapping : BaseEntity<Guid>
{
    public Guid MediaId { get; set; }
    public Media Media { get; set; } = default!;

    public Guid MediaTypeId { get; set; }
    public MediaType MediaType { get; set; } = default!;
}