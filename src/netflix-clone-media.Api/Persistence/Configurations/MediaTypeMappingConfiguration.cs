namespace netflix_clone_media.Api.Persistence.Configurations;

internal sealed class MediaTypeMappingConfiguration : IEntityTypeConfiguration<MediaTypeMapping>
{
    public void Configure(EntityTypeBuilder<MediaTypeMapping> builder)
    {
        builder.ToTable(nameof(MediaTypeMapping));

        builder.HasKey(mt => new { mt.MediaId, mt.MediaTypeId });

        // Relations
        builder.HasOne(mt => mt.Media)
            .WithMany(m => m.MediaTypes)
            .HasForeignKey(mt => mt.MediaId);

        builder.HasOne(mt => mt.MediaType)
            .WithMany(mt => mt.MediaMappings)
            .HasForeignKey(mt => mt.MediaTypeId);
    }
}