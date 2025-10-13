namespace netflix_clone_media.Api.Persistence.Entitiy;

public class MediaType : BaseEntity<Guid>
{
    public string Name { get; set; } = null!;

    public ICollection<MediaTypeMapping> MediaMappings { get; set; } = [];
}