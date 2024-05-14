﻿using QuizUp.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace QuizUp.DAL.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext(options)
{
    public DbSet<Game> Games { get; set; }
    public DbSet<GameAnswer> GameAnswers { get; set; }
    public DbSet<GameApplicationUser> GameApplicationUsers { get; set; }
    public DbSet<Answer> Answers { get; set; }
    public DbSet<Question> Questions { get; set; }
    public DbSet<Quiz> Quizes { get; set; }
    public DbSet<ApplicationUser> ApplicationUsers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
        {
            relationship.DeleteBehavior = DeleteBehavior.Cascade;
        }

        // configuring many to many relationships
        modelBuilder.Entity<Game>()
            .HasMany(g => g.ApplicationUsers)
            .WithMany(au => au.Games)
            .UsingEntity<GameApplicationUser>();

        modelBuilder.Entity<Game>()
            .HasMany(g => g.Answers)
            .WithMany(a => a.Games)
            .UsingEntity<GameAnswer>();

        modelBuilder.Seed();

        base.OnModelCreating(modelBuilder);
    }
}
