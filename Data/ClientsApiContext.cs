﻿using ClientsAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ClientsAPI.Data;

public partial class ClientsApiContext : DbContext
{
    public ClientsApiContext()
    {
    }

    public ClientsApiContext(DbContextOptions<ClientsApiContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Card> Cards { get; set; }

    public virtual DbSet<Client> Clients { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Card>(entity =>
        {
            entity.HasKey(e => e.CardNumber);

            entity.HasIndex(e => e.CardNumber, "IX_Cards_CardNumber").IsUnique();

            entity.HasIndex(e => e.ClientId, "IX_Cards_ClientID").IsUnique();

            entity.Property(e => e.CardNumber).ValueGeneratedNever();
            entity.Property(e => e.ClientId).HasColumnName("ClientID");

            entity.HasOne(d => d.Client).WithOne(p => p.Card)
                .HasForeignKey<Card>(d => d.ClientId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasIndex(e => e.Id, "IX_Clients_Id").IsUnique();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
