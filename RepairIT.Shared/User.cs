using System.ComponentModel.DataAnnotations;

namespace RepairIT.Shared;

public enum Role
{
    Administrator,
    Manager
}

public class User
{
    public int Id { get; init; }

    [Required]
    public string UserId_Keycloak { get; set; } = string.Empty;
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public Role UserRole { get; set; }
    public bool IsActivated { get; set; } = false;
}