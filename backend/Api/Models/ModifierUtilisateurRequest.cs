namespace Api.Models;

public class ModifierUtilisateurRequest
{
    public string Nom { get; set; } = string.Empty;
    public string Prenom { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public bool EstActif { get; set; }
    public int RoleId { get; set; }
}
