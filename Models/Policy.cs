namespace agent_portal.Models;
public class Policy
{
    public int Id { get; set; }
    public int ProductId { get; set; } // FK to InsuranceProduct
    public InsuranceProduct? Product { get; set; } // Navigation
    public int ClientId { get; set; }
    public Client? Client { get; set; }
    public int AgentId { get; set; }
    public Agent? Agent { get; set; }
    public ICollection<Claim>? Claims { get; set; }

}