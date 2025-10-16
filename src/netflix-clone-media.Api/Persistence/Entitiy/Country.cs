namespace netflix_clone_media.Api.Persistence.Entitiy;

public class Country : BaseEntity<Guid>
{
    public string Name { get; set; } = null!;

    public ICollection<MediaCountries> MediaCountries { get; set; } = [];
}