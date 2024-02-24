namespace Api_AIKO.Models.JwtConfiguration
{
    public class AppConfiguration
    {
        public required string AllowedHosts { get; set; }
        public required JwtConfiguration Jwt { get; set; }
    }
}
