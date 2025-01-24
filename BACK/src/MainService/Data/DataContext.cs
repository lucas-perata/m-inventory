using System;
using MainService.Entities;
using MassTransit;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MainService.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions options) : base(options)
    {

    }

    public DbSet<Item> Items { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<ProjectItem> ProjectItems { get; set; }
    public DbSet<UserItem> UserItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.AddInboxStateEntity();
        modelBuilder.AddOutboxMessageEntity();
        modelBuilder.AddOutboxStateEntity();


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

        modelBuilder.Entity<Project>()
            .HasMany(p => p.ProjectItems)
            .WithOne(pi => pi.Project)
            .HasForeignKey(pi => pi.ProjectId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
