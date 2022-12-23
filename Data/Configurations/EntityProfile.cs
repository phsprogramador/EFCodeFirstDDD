using Domains;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations;

public class EntityProfile : IEntityTypeConfiguration<Profile>
{
    public void Configure(EntityTypeBuilder<Profile> builder)
    {
        builder
            .ToTable("TB_PROFILE");

        builder
            .HasKey(c => c.id);

        builder
            .Property(c => c.id)
            .HasColumnName("ID_PROFILE")
            .HasDefaultValueSql("NEWID()");

        builder
            .Property(c => c.name)
            .HasColumnName("NAME")
            .HasMaxLength(50);

        builder
            .Property(c => c.description)
            .HasColumnName("DESCRIPTION")
            .HasMaxLength(100)
            .IsRequired(true);

        builder
            .Property(c => c.dtInsert)
            .HasColumnName("DT_INSERT");            

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