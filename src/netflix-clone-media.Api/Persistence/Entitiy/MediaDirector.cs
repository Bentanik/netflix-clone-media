namespace netflix_clone_media.Api.Persistence.Entitiy;

public class MediaDirector : BaseEntity<Guid>
{
    public Guid MediaId { get; set; }
    public Media Media { get; set; } = default!;

    public Guid PersonId { get; set; }
    public Person Person { get; set; } = default!;
}