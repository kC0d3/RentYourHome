namespace RentYourHome.Services.Authentication;

public record AuthResult(
    bool Success,
    string Email,
    string Username,
    string Token,
    string Role)
{
    public readonly Dictionary<string, string> ErrorMessages = new();
}