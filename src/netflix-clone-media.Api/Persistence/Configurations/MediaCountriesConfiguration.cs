namespace netflix_clone_media.Api.Persistence.Configurations;

internal sealed class MediaCountriesConfiguration : IEntityTypeConfiguration<MediaCountries>
{
    public void Configure(EntityTypeBuilder<MediaCountries> builder)
    {
        builder.ToTable(nameof(MediaCountries));

        builder.HasKey(mc => new { mc.MediaId, mc.CountryId });

        // Relations
        builder.HasOne(mc => mc.Media)
            .WithMany(m => m.MediaCountries)
            .HasForeignKey(mc => mc.MediaId);

        builder.HasOne(mc => mc.Country)
            .WithMany(c => c.MediaCountries)
            .HasForeignKey(mc => mc.CountryId);
    }
}