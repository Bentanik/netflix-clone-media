namespace netflix_clone_media.Api.Persistence.Entitiy;

public class Person : BaseEntity<Guid>
{
    public string FullName { get; set; } = default!;
    public string OtherName { get; set; } = default!;
    public string ShortBio { get; set; } = default!;
    public string AvatarId { get; set; } = default!;
    public string AvatarUrl { get; set; } = default!;
    public Gender Gender { get; set; }
    public int Day { get; set; }
    public int Month { get; set; }
    public int Year { get; set; }

    public ICollection<MediaDirector> DirectedMedias { get; set; } = [];
    public ICollection<MediaCast> CastMedias { get; set; } = [];
}