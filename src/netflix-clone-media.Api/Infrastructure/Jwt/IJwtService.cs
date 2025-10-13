namespace netflix_clone_media.Api.Infrastructure.Jwt;

public interface IJwtService
{
    ClaimsPrincipal? ValidateAccessToken(string token);
}
