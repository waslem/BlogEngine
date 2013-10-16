using BlogEngine.Core.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace BlogEngine.Core.Contexts
{
    public class BlogContext : DbContext
    {
        public BlogContext() 
            : base("DefaultConnection")
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<BlogEntry> BlogEntries { get; set; }
        public DbSet<Vote> Votes { get; set; }
        public DbSet<BlogImage> Images { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Comment>().HasKey(x => x.CommentId);
            modelBuilder.Entity<Comment>().Property(x => x.CommentId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Comment>().HasOptional(x => x.Parent)
                                            .WithMany(x => x.Children)
                                            .HasForeignKey(x => x.ParentId)
                                            .WillCascadeOnDelete(false);

            modelBuilder.Entity<Vote>().HasKey(x => x.VoteId);
            modelBuilder.Entity<Vote>().Property(x => x.VoteId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Vote>().HasRequired(x => x.Comment)
                                            .WithMany(x => x.Votes)
                                            .HasForeignKey(x => x.CommentId)
                                            .WillCascadeOnDelete(false);
        }
    }
}
