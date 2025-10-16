namespace netflix_clone_media.Api.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
           => builder.ApplyConfigurationsFromAssembly(AssemblyReference.Assembly);

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }

    public DbSet<Media> Medias { get; set; }
    public DbSet<MediaPart> MediaParts { get; set; }
    public DbSet<Episode> Episodes { get; set; }
    public DbSet<MediaType> MediaTypes { get; set; }
    public DbSet<MediaTypeMapping> MediaTypeMappings { get; set; }
    public DbSet<Person> Persons { get; set; }
    public DbSet<MediaCast> MediaCasts { get; set; }
    public DbSet<MediaDirector> MediaDirectors { get; set; }
    public DbSet<Country> Countries { get; set; }
    public DbSet<MediaCountries> MediaCountries { get; set; }
}
