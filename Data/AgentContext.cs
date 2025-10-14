using Microsoft.EntityFrameworkCore;
using agent_portal.Models;

namespace agent_portal.Data

{
    public class AgentContext : DbContext
    {
        public AgentContext(DbContextOptions<AgentContext> options)
            : base(options)
        {
        }

        //DbSet instances for each model created. 
        public DbSet<InsuranceProduct> InsuranceProducts { get; set; }
        public DbSet<Policy> Policies { get; set; }
        public DbSet<Claim> Claims { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Agent> Agents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // InsuranceProduct to Policies
            modelBuilder.Entity<Policy>()
                .HasOne(p => p.Product)
                .WithMany(ip => ip.Policies)
                .HasForeignKey(p => p.ProductId);

            // Client to Policies
            modelBuilder.Entity<Policy>()
                .HasOne(p => p.Client)
                .WithMany(c => c.Policies)
                .HasForeignKey(p => p.ClientId);

            // Agent to Policies
            modelBuilder.Entity<Policy>()
                .HasOne(p => p.Agent)
                .WithMany(a => a.Policies)
                .HasForeignKey(p => p.AgentId);

            // Policy to Claims
            modelBuilder.Entity<Claim>()
                .HasOne(c => c.Policy)
                .WithMany(p => p.Claims)
                .HasForeignKey(c => c.PolicyId);
        }

    }

}


