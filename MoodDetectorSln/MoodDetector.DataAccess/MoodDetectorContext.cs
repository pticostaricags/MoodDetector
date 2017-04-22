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

        public virtual DbSet<FacebookPersonalityInsight> FacebookPersonalityInsights { get; set; }
        public virtual DbSet<FacebookPersonalityInsightsBehavior> FacebookPersonalityInsightsBehaviors { get; set; }
        public virtual DbSet<FacebookPersonalityInsightsConsumptionPreference> FacebookPersonalityInsightsConsumptionPreferences { get; set; }
        public virtual DbSet<FacebookPersonalityInsightsNeed> FacebookPersonalityInsightsNeeds { get; set; }
        public virtual DbSet<FacebookPersonalityInsightsPersonality> FacebookPersonalityInsightsPersonalities { get; set; }
        public virtual DbSet<FacebookPersonalityInsightsValue> FacebookPersonalityInsightsValues { get; set; }
        public virtual DbSet<FacebookProfile> FacebookProfiles { get; set; }
        public virtual DbSet<FacebookUserPost> FacebookUserPosts { get; set; }
        public virtual DbSet<FacebookUserPostKeyPhras> FacebookUserPostKeyPhrases { get; set; }
        public virtual DbSet<FacebookUserPostSentiment> FacebookUserPostSentiments { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FacebookPersonalityInsight>()
                .Property(e => e.ProcessedLanguage)
                .IsFixedLength();

            modelBuilder.Entity<FacebookPersonalityInsight>()
                .Property(e => e.JsonRequest)
                .IsUnicode(false);

            modelBuilder.Entity<FacebookPersonalityInsight>()
                .HasMany(e => e.FacebookPersonalityInsightsBehaviors)
                .WithRequired(e => e.FacebookPersonalityInsight)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<FacebookPersonalityInsight>()
                .HasMany(e => e.FacebookPersonalityInsightsConsumptionPreferences)
                .WithRequired(e => e.FacebookPersonalityInsight)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<FacebookPersonalityInsight>()
                .HasMany(e => e.FacebookPersonalityInsightsNeeds)
                .WithRequired(e => e.FacebookPersonalityInsight)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<FacebookPersonalityInsight>()
                .HasMany(e => e.FacebookPersonalityInsightsPersonalities)
                .WithRequired(e => e.FacebookPersonalityInsight)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<FacebookPersonalityInsight>()
                .HasMany(e => e.FacebookPersonalityInsightsValues)
                .WithRequired(e => e.FacebookPersonalityInsight)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<FacebookPersonalityInsightsConsumptionPreference>()
                .HasMany(e => e.FacebookPersonalityInsightsConsumptionPreferences1)
                .WithOptional(e => e.FacebookPersonalityInsightsConsumptionPreference1)
                .HasForeignKey(e => e.ParentFacebookPersonalityInsightsConsumptionPreferencesId);

            modelBuilder.Entity<FacebookPersonalityInsightsPersonality>()
                .HasMany(e => e.FacebookPersonalityInsightsPersonality1)
                .WithOptional(e => e.FacebookPersonalityInsightsPersonality2)
                .HasForeignKey(e => e.ParentFacebookPersonalityInsightsPersonalityId);

            modelBuilder.Entity<FacebookProfile>()
                .HasMany(e => e.FacebookPersonalityInsights)
                .WithRequired(e => e.FacebookProfile)
                .WillCascadeOnDelete(false);

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
