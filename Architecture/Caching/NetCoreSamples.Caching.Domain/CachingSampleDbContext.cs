﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using NetCoreSamples.Caching.Domain.Entities;

namespace NetCoreSamples.Caching.Domain;

public partial class CachingSampleDbContext : DbContext
{
    public CachingSampleDbContext(DbContextOptions<CachingSampleDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<StateProvince> StateProvinces { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Country__3214EC07CD55C2A7");

            entity.ToTable("Country");

            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<StateProvince>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__StatePro__3214EC073F029497");

            entity.ToTable("StateProvince");

            entity.Property(e => e.Name).HasMaxLength(100);

            entity.HasOne(d => d.Country).WithMany(p => p.StateProvinces)
                .HasForeignKey(d => d.CountryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StateProvince_Country");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__User__3214EC0757DE7661");

            entity.ToTable("User");

            entity.Property(e => e.Username).HasMaxLength(100);

            entity.HasOne(d => d.StateProvince).WithMany(p => p.Users)
                .HasForeignKey(d => d.StateProvinceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_User_StateProvince");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
