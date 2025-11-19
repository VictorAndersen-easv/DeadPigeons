using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace dataccess;

public partial class NeondbContext : DbContext
{
    public NeondbContext()
    {
    }

    public NeondbContext(DbContextOptions<NeondbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Player> Players { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=ep-old-recipe-agw8ov1x-pooler.c-2.eu-central-1.aws.neon.tech; Database=neondb; Username=neondb_owner; Password=npg_n4FzJKxqi0US; SSL Mode=VerifyFull; Channel Binding=Require;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Player>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("players_pkey");

            entity.ToTable("players", "birdy");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.Name).HasColumnName("name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
