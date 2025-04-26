using BicasTeam.MoviGestion.API.Vehicles.Domain.Model.Aggregates;
﻿using BicasTeam.MoviGestion.API.Alerts.Domain.Model.Aggregates;
using BicasTeam.MoviGestion.API.Shipments.Domain.Model.Aggregates;
﻿using BicasTeam.MoviGestion.API.Profiles.Domain.Model.Aggregates;
using BicasTeam.MoviGestion.API.Shared.Infrastructure.Persistence.EFC.Configuration.Extensions;

using EntityFrameworkCore.CreatedUpdatedDate.Extensions;
using Microsoft.EntityFrameworkCore;

namespace BicasTeam.MoviGestion.API.Shared.Infrastructure.Persistence.EFC.Configuration;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        base.OnConfiguring(builder);
        builder.AddCreatedUpdatedInterceptor();
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        // Vehicle Context
        builder.Entity<Vehicle>().HasKey(f => f.Id);
        builder.Entity<Vehicle>().Property(f => f.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Vehicle>().Property(f => f.LicensePlate).IsRequired();
        builder.Entity<Vehicle>().Property(f => f.Model).IsRequired();
        builder.Entity<Vehicle>().Property(f => f.SerialNumber).IsRequired();
        
        // Category Context
        builder.Entity<Report>().HasKey(f => f.Id);
        builder.Entity<Report>().Property(f => f.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Report>().Property(f => f.Type).IsRequired();
        builder.Entity<Report>().Property(f => f.UserId).IsRequired();
        builder.Entity<Report>().Property(f => f.Description).IsRequired();
        
        // Shipment Context
        builder.Entity<Shipment>().HasKey(f => f.Id);
        builder.Entity<Shipment>().Property(f => f.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Shipment>().Property(f => f.UserId).IsRequired();
        builder.Entity<Shipment>().Property(f => f.Destiny).IsRequired();
        builder.Entity<Shipment>().Property(f => f.Description).IsRequired();
        builder.Entity<Shipment>().Property(f => f.Status).IsRequired();
        
        // User Context
        builder.Entity<Profile>().HasKey(f => f.Id);
        builder.Entity<Profile>().Property(f => f.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Profile>().Property(f => f.Name).IsRequired();
        builder.Entity<Profile>().Property(f => f.LastName).IsRequired();
        builder.Entity<Profile>().Property(f => f.Email).IsRequired();
        builder.Entity<Profile>().Property(f => f.Password).IsRequired();
        builder.Entity<Profile>().Property(f => f.Type).IsRequired();


        
        // Apply SnakeCase Naming Convention
        builder.UseSnakeCaseWithPluralizedTableNamingConvention();
    }
}