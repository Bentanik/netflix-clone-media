namespace netflix_clone_media.Api.Persistence.Configurations;

internal sealed class MediaPartConfiguration : IEntityTypeConfiguration<MediaPart>
{
    public void Configure(EntityTypeBuilder<MediaPart> builder)
    {
        builder.ToTable(nameof(MediaPart));

        builder.HasKey(p => p.Id);

        // Relations
        builder.HasOne(p => p.Media)
            .WithMany(m => m.Parts)
            .HasForeignKey(p => p.MediaId);

        builder.HasMany(p => p.Episodes)
            .WithOne(e => e.MediaPart)
            .HasForeignKey(e => e.MediaPartId);
    }
}