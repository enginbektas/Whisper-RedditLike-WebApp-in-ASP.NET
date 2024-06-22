using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Whisper.Entities;
using System.Linq;

namespace Whisper.Data.Context
{
    /// <summary>
    /// DbContext, AspnetIdentity kullanıldığı için IdentityDbContext class'ından miras alıyor
    /// IdentityDbContext'e user ve role tabloları için hangi modelleri kullanacağımızı generic olarak geçiyoruz.
    /// IdentityDbContext'in diğer overload'larına ihtiyacımız bu versiyon işimizi görür.
    /// </summary>
    public class WhisperDbContext : IdentityDbContext<User, Role, string> 
    {
        public WhisperDbContext(DbContextOptions<WhisperDbContext> options) : base(options)
        {
        }

        public DbSet<Comment> Comments { get; set; }
        public DbSet<Thread> Threads { get; set; }
        public DbSet<Community> Communities { get; set; }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // Community-User
            modelBuilder.Entity<Community>()
                .HasMany(a => a.Moderators)
                .WithMany(b => b.ModOfCommunities);
            modelBuilder.Entity<Community>()
                .HasMany(a => a.Members)
                .WithMany(b => b.MemberOfCommunities);

            // User-Community
            modelBuilder.Entity<Community>()
                .HasOne(a => a.Creator)
                .WithMany(b => b.AdminOfCommunities);




            base.OnModelCreating(modelBuilder);

        }
    }
}
