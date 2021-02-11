using ChatApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChatApp.EntityConfiguration
{
    public class UserConfig : IEntityTypeConfiguration<User>        
    {        
        public virtual void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Id).HasDefaultValueSql("NEWID()");
            builder.Property(u => u.FirstName).HasMaxLength(64).IsRequired();
            builder.Property(u => u.LastName).HasMaxLength(64).IsRequired();
            builder.Property(u => u.Email).HasMaxLength(64).IsRequired();            
            builder.HasIndex(u => u.Email).IsUnique(true);

            builder.ToTable("User");
        }           
    
    }
}
