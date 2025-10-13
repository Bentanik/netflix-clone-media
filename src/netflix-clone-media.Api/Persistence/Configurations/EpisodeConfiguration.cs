namespace netflix_clone_media.Api.Persistence.Configurations;

internal sealed class EpisodeConfiguration : IEntityTypeConfiguration<Episode>
{
    public void Configure(EntityTypeBuilder<Episode> builder)
    {
        builder.ToTable(nameof(Episode));

        builder.HasKey(e => e.Id);

        // Relations
        builder.HasOne(e => e.Media)
            .WithMany(m => m.Episodes)
            .HasForeignKey(e => e.MediaId);

        builder.HasOne(e => e.MediaPart)
            .WithMany(p => p.Episodes)
            .HasForeignKey(e => e.MediaPartId)
            .IsRequired(false);
    }
}