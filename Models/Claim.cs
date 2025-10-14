namespace agent_portal.Models;
public class Claim
{
    public int Id { get; set; }
    public int PolicyId { get; set; } // FK to Policy
    public Policy? Policy { get; set; } // Navigation

}