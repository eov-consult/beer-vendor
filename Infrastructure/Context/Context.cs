using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Infrastructure.Context
{
    internal class Context : DbContext, IContext
    {
        public DbSet<Beer> Beer { get; set; }

        public DbSet<Brewer> Brewers { get; set; }

        public DbSet<Vendor> Vendors { get; set; }

        public DbSet<VendorBeer> VendorBeers { get; set; }

        public Context()
        {

        }
        public Context(DbContextOptions options) : base(options)
        {
            this.Database.EnsureCreated();

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseInMemoryDatabase("BeerVendorDb");

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Brewer>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.HasData(new Brewer
                {
                    Id = 1,
                    Name = "Abbaye de Leffe"
                },
                new Brewer
                {
                    Id = 2,
                    Name = "AB Inbev"
                },
                new Brewer
                {
                    Id = 3,
                    Name = "Brasserie d'Orval"
                });
            });


            modelBuilder.Entity<Beer>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.HasOne(d => d.Brewer)
                    .WithMany(p => p.Beers)
                    .HasForeignKey(d => d.BrewerId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasData(new Beer
                {
                    Id = 1,
                    Name = "Leffe Brune",
                    AlcoholDegree = 6.5f,
                    Amount = 50,
                    BrewerId = 1,
                },
                new Beer
                {
                    Id = 2,
                    Name = "Leffe Triple",
                    AlcoholDegree = 8.5f,
                    Amount = 88,
                    BrewerId = 1,
                },
                new Beer
                {
                    Id = 3,
                    Name = "Leffe Ruby",
                    AlcoholDegree = 5f,
                    Amount = 20,
                    BrewerId = 1,
                },
                new Beer
                {
                    Id = 4,
                    Name = "Jupiler",
                    AlcoholDegree = 5.2f,
                    Amount = 30,
                    BrewerId = 2,
                },
                new Beer
                {
                    Id = 5,
                    Name = "Jupiler Blue",
                    AlcoholDegree = 3.3f,
                    Amount = 30,
                    BrewerId = 2,
                },
                new Beer
                {
                    Id = 6,
                    Name = "Jupiler Tauro",
                    AlcoholDegree = 8.3f,
                    Amount = 10,
                    BrewerId = 2,
                },
                new Beer
                {
                    Id = 7,
                    Name = "Orval Trapiste",
                    AlcoholDegree = 6.2f,
                    Amount = 29,
                    BrewerId = 3,
                });
            });

            modelBuilder.Entity<Vendor>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.HasData(new Vendor
                {
                    Id = 1,
                    Name = "GeneDrinks"
                }, new Vendor { Id = 2, Name = "ErcanDrinks" });
            });

            modelBuilder.Entity<VendorBeer>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.HasOne(d => d.Beer)
                    .WithMany(p => p.VendorBeers)
                    .HasForeignKey(d => d.BeerId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Vendor)
                    .WithMany(p => p.VendorBeers)
                    .HasForeignKey(d => d.VendorId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasData(new VendorBeer
                {
                    Id = 1,
                    BeerId = 1,
                    VendorId = 1,
                    Quantity = 50
                });
            });
        }
    }
}
