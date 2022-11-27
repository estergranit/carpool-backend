using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Carpool.Repositories.GeneratedModels;

public partial class CarpoolDBContext : DbContext
{
    public CarpoolDBContext()
    {
    }

    public CarpoolDBContext(DbContextOptions<CarpoolDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Station> Stations { get; set; }

    public virtual DbSet<Travel> Travels { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("Name=CarpoolDBConnectionString");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Station>(entity =>
        {
            entity.HasKey(e => e.StationId).HasName("Station_pkey");

            entity.Property(e => e.StationId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("station_id");
            entity.Property(e => e.StationLocation).HasColumnName("station_location");
            entity.Property(e => e.StationPassengerId).HasColumnName("station_passenger_id");
            entity.Property(e => e.StationTime)
                .HasColumnType("time with time zone")
                .HasColumnName("station_time");
            entity.Property(e => e.StationTravelId).HasColumnName("station_travel_id");

            entity.HasOne(d => d.StationPassenger).WithMany(p => p.Stations)
                .HasForeignKey(d => d.StationPassengerId)
                .HasConstraintName("stations_fkey_passenger_id_users_user_id");

            entity.HasOne(d => d.StationTravel).WithMany(p => p.Stations)
                .HasForeignKey(d => d.StationTravelId)
                .HasConstraintName("stations_fkey_travel_id_travels_travel_id");
        });

        modelBuilder.Entity<Travel>(entity =>
        {
            entity.HasKey(e => e.TravelId).HasName("travels_pkey");

            entity.ToTable("travels");

            entity.Property(e => e.TravelId)
                .ValueGeneratedNever()
                .HasColumnName("travel_id");
            entity.Property(e => e.TravelAvailable)
                .HasDefaultValueSql("1")
                .HasColumnName("travel_available");
            entity.Property(e => e.TravelDepartureTime).HasColumnName("travel_departure_time");
            entity.Property(e => e.TravelDestination).HasColumnName("travel_destination");
            entity.Property(e => e.TravelDriverId).HasColumnName("travel_driver_id");
            entity.Property(e => e.TravelMap).HasColumnName("travel_map");
            entity.Property(e => e.TravelOrigin).HasColumnName("travel_origin");

            entity.HasOne(d => d.TravelDriver).WithMany(p => p.Travels)
                .HasForeignKey(d => d.TravelDriverId)
                .HasConstraintName("travels_fkey_driverId_users_userId");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("users_pkey");

            entity.ToTable("users");

            entity.HasIndex(e => e.UserName, "users_unique").IsUnique();

            entity.Property(e => e.UserId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("user_id");
            entity.Property(e => e.UserEmail).HasColumnName("user_email");
            entity.Property(e => e.UserName).HasColumnName("user_name");
            entity.Property(e => e.UserPassword).HasColumnName("user_password");
            entity.Property(e => e.UserPhone).HasColumnName("user_phone");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
