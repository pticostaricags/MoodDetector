namespace MoodDetector.DataAccess
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class MoodDetectorContext : DbContext
    {
        public MoodDetectorContext()
            : base("name=MoodDetectorContext")
        {
        }

        public virtual DbSet<FacebookProfile> FacebookProfiles { get; set; }
        public virtual DbSet<FacebookUserPost> FacebookUserPosts { get; set; }
        public virtual DbSet<FacebookUserPostKeyPhras> FacebookUserPostKeyPhrases { get; set; }
        public virtual DbSet<FacebookUserPostSentiment> FacebookUserPostSentiments { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FacebookProfile>()
                .HasMany(e => e.FacebookUserPosts)
                .WithRequired(e => e.FacebookProfile)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<FacebookUserPost>()
                .HasMany(e => e.FacebookUserPostKeyPhrases)
                .WithRequired(e => e.FacebookUserPost)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<FacebookUserPost>()
                .HasMany(e => e.FacebookUserPostSentiments)
                .WithRequired(e => e.FacebookUserPost)
                .WillCascadeOnDelete(false);
        }
    }
}
