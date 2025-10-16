namespace netflix_clone_media.Api.Messages;

public enum CountryMessages
{
    [Message("Types exists", "COUNTRY_01")]
    CountryAlreadyExists,

    [Message("Create countries successfully", "COUNTRY_02")]
    CreateCountriesSuccessfully,

    [Message("Get all countries successfully", "COUNTRY_03")]
    GetAllCountriesSuccessfully,
}