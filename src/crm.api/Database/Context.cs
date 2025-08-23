using Microsoft.EntityFrameworkCore;
using crm.api.Entities;

namespace crm.api.Database;

public class Context(DbContextOptions<Context> options) : DbContext(options)
{
    public DbSet<Contact> Contacts{get; init;}
    public DbSet<Organization> Organizations{get; init;}
    public DbSet<Sector> Sectors{get; init;}
    public DbSet<LeadOrigin> LeadOrigins{get; init;}
    public DbSet<Category> Categories{get; init;}
    public DbSet<Person> People{get; init;}

    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Organization>(entity =>
        {
            entity.HasKey(p => p.Id);
            entity.HasOne(p => p.Sector)
                .WithMany()
                .HasForeignKey("SectorId")
                .OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(p => p.LeadOrigin)
                .WithMany()
                .HasForeignKey("LeadOriginId")
                .OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(p => p.Category)
                .WithMany()
                .HasForeignKey("CategoryId")
                .OnDelete(DeleteBehavior.Restrict);
        });
        
        modelBuilder.Entity<Person>(entity =>
        {
            entity.HasKey(p => p.Id);
            entity.HasOne(p => p.LeadOrigin)
                .WithMany()
                .HasForeignKey("LeadOriginId")
                .OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(p => p.Category)
                .WithMany()
                .HasForeignKey("CategoryId")
                .OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(p => p.Organization)
                .WithMany()
                .HasForeignKey("OrganizationId")
                .OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(p => p.Contact)
                .WithMany()
                .HasForeignKey("CategoryId")
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}