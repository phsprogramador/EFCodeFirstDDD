using Domains;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations;

public class EntityUser : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder
            .ToTable("TB_USER");

        builder
            .HasKey(c => c.id);

        builder
            .Property(c => c.id)
            .HasColumnName("ID_USER")
            .HasDefaultValueSql("NEWID()");

        builder
            .Property(c => c.idProfile)
            .HasColumnName("ID_PROFILE")
            .IsRequired(true);

        builder
            .HasOne(u => u.Profiles)
            .WithMany()            
            .HasForeignKey(u => u.idProfile);

        builder
            .Property(c => c.name)
            .HasColumnName("NAME")
            .HasMaxLength(50);

        builder
            .Property(c => c.login)
            .HasColumnName("LOGIN")
            .HasMaxLength(30)
            .IsRequired(true);

        builder
            .Property(c => c.password)
            .HasColumnName("PASSWORD")
            .HasMaxLength(70)
            .IsRequired(true);

        builder
            .Property(c => c.email)
            .HasColumnName("EMAIL")
            .HasMaxLength(150)
            .IsRequired();

        builder
            .Property(c => c.dtInsert)
            .HasColumnName("DT_INSERT");
            //.HasDefaultValueSql("GETDATE()");

        builder
            .Property(c => c.dtUpdate)
            .HasColumnName("DT_UPDATE")
            .HasDefaultValueSql("GETDATE()");

        builder
            .Property(c => c.active)
            .HasColumnName("ACTIVE")
            .HasDefaultValueSql("1")
            .IsRequired(true);
    }
}