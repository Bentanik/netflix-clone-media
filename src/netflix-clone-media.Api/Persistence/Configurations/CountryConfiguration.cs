namespace netflix_clone_media.Api.Persistence.Configurations;

internal sealed class CountryConfiguration : IEntityTypeConfiguration<Country>
{
    public void Configure(EntityTypeBuilder<Country> builder)
    {
        builder.ToTable(nameof(Country));

        builder.HasKey(mt => mt.Id);

        // Relations
        builder.HasMany(ct => ct.MediaCountries)
            .WithOne(mct => mct.Country)
            .HasForeignKey(mct => mct.CountryId);
    }
}