namespace agent_portal.Models;

public class InsuranceProduct
{
    public int Id { get; set; }
    public string? Name { get; set; } // e.g., "Homeowners", "Auto", "Farm"
    public string? Type { get; set; } // Category
    public string? CoverageSummary { get; set; } // Brief description
    public string? EligibilityNotes { get; set; } // Who qualifies
    public string? QuickLinks { get; set; } // URLs or attachments
    public ICollection<Policy>? Policies { get; set; }
}