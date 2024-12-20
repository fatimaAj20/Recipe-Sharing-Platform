﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RecipeSharingProject.Common.Model;
using System.Net;
using System.Reflection.Emit;
using System.Security.Principal;

namespace RecipeSharingProject.Infrastructure;

public class ApplicationDbContext:IdentityDbContext<IdentityUser, IdentityRole, string>
{
    public DbSet<Recipe> Recipes { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Recipe>().HasKey(e => e.Id);

        base.OnModelCreating(builder);
    }
}
