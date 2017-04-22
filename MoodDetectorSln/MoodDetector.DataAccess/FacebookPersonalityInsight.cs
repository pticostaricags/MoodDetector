namespace MoodDetector.DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class FacebookPersonalityInsight
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public FacebookPersonalityInsight()
        {
            FacebookPersonalityInsightsBehaviors = new HashSet<FacebookPersonalityInsightsBehavior>();
            FacebookPersonalityInsightsConsumptionPreferences = new HashSet<FacebookPersonalityInsightsConsumptionPreference>();
            FacebookPersonalityInsightsNeeds = new HashSet<FacebookPersonalityInsightsNeed>();
            FacebookPersonalityInsightsPersonalities = new HashSet<FacebookPersonalityInsightsPersonality>();
            FacebookPersonalityInsightsValues = new HashSet<FacebookPersonalityInsightsValue>();
        }

        [Key]
        public long FacebookPersonalityInsightsId { get; set; }

        public int WordCount { get; set; }

        [StringLength(10)]
        public string ProcessedLanguage { get; set; }

        public long FacebookProfileId { get; set; }

        [Column(TypeName = "text")]
        [Required]
        public string JsonRequest { get; set; }

        public virtual FacebookProfile FacebookProfile { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FacebookPersonalityInsightsBehavior> FacebookPersonalityInsightsBehaviors { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FacebookPersonalityInsightsConsumptionPreference> FacebookPersonalityInsightsConsumptionPreferences { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FacebookPersonalityInsightsNeed> FacebookPersonalityInsightsNeeds { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FacebookPersonalityInsightsPersonality> FacebookPersonalityInsightsPersonalities { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FacebookPersonalityInsightsValue> FacebookPersonalityInsightsValues { get; set; }
    }
}
