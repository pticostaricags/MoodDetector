namespace MoodDetector.DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("FacebookPersonalityInsightsPersonality")]
    public partial class FacebookPersonalityInsightsPersonality
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public FacebookPersonalityInsightsPersonality()
        {
            FacebookPersonalityInsightsPersonality1 = new HashSet<FacebookPersonalityInsightsPersonality>();
        }

        public long FacebookPersonalityInsightsPersonalityId { get; set; }

        public long FacebookPersonalityInsightsId { get; set; }

        [StringLength(50)]
        public string TraitId { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(50)]
        public string Category { get; set; }

        public double? Percentile { get; set; }

        public long? ParentFacebookPersonalityInsightsPersonalityId { get; set; }

        public virtual FacebookPersonalityInsight FacebookPersonalityInsight { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FacebookPersonalityInsightsPersonality> FacebookPersonalityInsightsPersonality1 { get; set; }

        public virtual FacebookPersonalityInsightsPersonality FacebookPersonalityInsightsPersonality2 { get; set; }
    }
}
