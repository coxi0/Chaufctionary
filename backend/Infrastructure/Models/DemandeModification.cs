namespace Infrastructure.Models;

public class DemandeModification
{
    public int Id { get; set; }
    public int ClientId { get; set; }
    public int UtilisateurId { get; set; }
    public string Message { get; set; } = string.Empty;
    public DateTime DateCreation { get; set; }

    public string ClientNom { get; set; } = string.Empty;
    public string ChauffeurNom { get; set; } = string.Empty;
    public string? ConseilActuel { get; set; }
}
