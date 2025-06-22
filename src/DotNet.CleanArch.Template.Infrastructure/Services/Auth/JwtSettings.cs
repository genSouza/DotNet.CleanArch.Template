namespace DotNet.CleanArch.Template.Infrastructure.Services.Auth;

public class JwtSettings
{
    public const string SectionName = "JwtSettings"; // Nome da seção no appsettings.json
    public string SecretKey { get; set; } = string.Empty; // Chave secreta para assinar o token
    public string ValidIssuer { get; set; } = string.Empty; // Emissor válido
    public string ValidAudience { get; set; } = string.Empty; // Público-alvo válido
    public int ExpiryInMinutes { get; set; } = 60; // Tempo de expiração
    public string RefreshTokenSecret { get; set; } = string.Empty; // Opcional: para refresh tokens
    public int RefreshTokenExpiryInDays { get; set; } = 7; // Opcional
}
