namespace netflix_clone_media.Api.Persistence.Configurations;

internal sealed class MediaDirectorConfiguration : IEntityTypeConfiguration<MediaDirector>
{
    public void Configure(EntityTypeBuilder<MediaDirector> builder)
    {
        builder.ToTable(nameof(MediaDirector));

        builder.HasKey(md => new { md.MediaId, md.PersonId });

        // Relations
        builder.HasOne(md => md.Media)
            .WithMany(m => m.Directors)
            .HasForeignKey(md => md.MediaId);

        builder.HasOne(md => md.Person)
            .WithMany(p => p.DirectedMedias)
            .HasForeignKey(md => md.PersonId);
    }
}
