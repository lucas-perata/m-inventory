using System;
using BACK.Entities;
using Microsoft.EntityFrameworkCore;

namespace BACK.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions options) : base(options)
    {

    }

    public DbSet<Item> Items { get; set; }
    public DbSet<Project> Projects { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProjectItem>()
            .HasOne(pi => pi.Project)
            .WithMany(p => p.ProjectItems)
            .HasForeignKey(pi => pi.ProjectId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<ProjectItem>()
            .HasOne(pi => pi.Item)
            .WithMany(i => i.ProjectItems)
            .HasForeignKey(pi => pi.ItemId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
