namespace Api.Models;

public class DemandeRequest
{
    public int ClientId { get; set; }
    public string Message { get; set; } = string.Empty;
}
