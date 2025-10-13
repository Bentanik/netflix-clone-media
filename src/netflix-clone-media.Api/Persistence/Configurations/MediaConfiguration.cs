namespace netflix_clone_media.Api.Persistence.Configurations;

internal sealed class MediaConfiguration : IEntityTypeConfiguration<Media>
{
    public void Configure(EntityTypeBuilder<Media> builder)
    {
        builder.ToTable(nameof(Media));

        builder.HasKey(m => m.Id);

        builder.Property(m => m.Title)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(m => m.Category)
            .HasConversion<int>();

        // Relations
        builder.HasMany(m => m.Parts)
            .WithOne(p => p.Media)
            .HasForeignKey(p => p.MediaId);

        builder.HasMany(m => m.Episodes)
            .WithOne(e => e.Media)
            .HasForeignKey(e => e.MediaId);

        builder.HasMany(m => m.Directors)
            .WithOne(d => d.Media)
            .HasForeignKey(d => d.MediaId);

        builder.HasMany(m => m.Casts)
            .WithOne(c => c.Media)
            .HasForeignKey(c => c.MediaId);

        builder.HasMany(m => m.MediaTypes)
            .WithOne(mt => mt.Media)
            .HasForeignKey(mt => mt.MediaId);
    }
}