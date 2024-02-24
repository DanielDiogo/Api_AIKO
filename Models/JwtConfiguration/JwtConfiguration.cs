namespace Api_AIKO.Models.JwtConfiguration
{
    public class JwtConfiguration
    {
        public string? SecretKey { get; set; }
        public string? Issuer { get; set; }
        public string? Audience { get; set; }
    }
}
