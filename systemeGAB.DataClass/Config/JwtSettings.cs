namespace systemeGAB.DataClass.Config
{
    public class JwtSettings
    {
        public string? Issuer { get; set; }
        public string Secret { get; set; } = "JwtS3cr3tK3yWithM@ximumSiz3Is64Byt3s";
        public int ExpirationInMinutes { get; set; }
    }
}
