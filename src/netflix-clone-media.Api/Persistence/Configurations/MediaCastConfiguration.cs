namespace netflix_clone_media.Api.Persistence.Configurations;

internal sealed class MediaCastConfiguration : IEntityTypeConfiguration<MediaCast>
{
    public void Configure(EntityTypeBuilder<MediaCast> builder)
    {
        builder.ToTable(nameof(MediaCast));

        builder.HasKey(mc => new { mc.MediaId, mc.PersonId });

        // Relations
        builder.HasOne(mc => mc.Media)
            .WithMany(m => m.Casts)
            .HasForeignKey(mc => mc.MediaId);

        builder.HasOne(mc => mc.Person)
            .WithMany(p => p.CastMedias)
            .HasForeignKey(mc => mc.PersonId);
    }
}
