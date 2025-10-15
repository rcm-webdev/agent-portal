using System.ComponentModel.DataAnnotations;

namespace agent_portal.Models;

public class Agent
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Name is required")]
    public string? Name { get; set; }

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Please enter a valid email address")]
    public string? Email { get; set; }

    public string? Phone { get; set; }
    public string? Agency { get; set; }
    public ICollection<Policy>? Policies { get; set; }
}