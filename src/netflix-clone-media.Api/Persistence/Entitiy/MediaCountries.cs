namespace netflix_clone_media.Api.Persistence.Entitiy;

public class MediaCountries : BaseEntity<Guid>
{
    public Guid MediaId { get; set; }
    public Media Media { get; set; } = default!;

    public Guid CountryId { get; set; }
    public Country Country { get; set; } = default!;
}