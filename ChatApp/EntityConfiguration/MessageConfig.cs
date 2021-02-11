using ChatApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChatApp.EntityConfiguration
{
    public class MessageConfig : IEntityTypeConfiguration<Message>        
    {
        public virtual void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Id).HasDefaultValueSql("NEWID()");
            builder.Property(u => u.SenderId).IsRequired();
            builder.Property(u => u.ReceiverId).IsRequired();
            builder.Property(u => u.Text).HasMaxLength(1024).IsRequired();
            builder.Property(u => u.CreatedDate).HasDefaultValueSql("getdate()");

            builder.ToTable("Message");

            builder.HasOne<User>(u => u.Sender)
            .WithMany(m => m.SenderMessages)
            .HasForeignKey(u => u.SenderId)
            .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<User>(u => u.Receiver)
            .WithMany(m => m.ReceiverMessages)
            .HasForeignKey(u => u.ReceiverId)
            .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
