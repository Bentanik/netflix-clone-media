namespace netflix_clone_media.Api.Persistence.Configurations;

internal sealed class MediaTypeConfiguration : IEntityTypeConfiguration<MediaType>
{
    public void Configure(EntityTypeBuilder<MediaType> builder)
    {
        builder.ToTable(nameof(MediaType));

        builder.HasKey(mt => mt.Id);

        // Relations
        builder.HasMany(mt => mt.MediaMappings)
            .WithOne(mtm => mtm.MediaType)
            .HasForeignKey(mtm => mtm.MediaTypeId);
    }
}