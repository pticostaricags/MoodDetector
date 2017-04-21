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
        public long FacebookPersonalityInsightsPersonalityId { get; set; }

        public long FacebookPersonalityInsightsId { get; set; }

        [StringLength(50)]
        public string TraitId { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(50)]
        public string Category { get; set; }

        public double? Percentile { get; set; }

        public virtual FacebookPersonalityInsight FacebookPersonalityInsight { get; set; }
    }
}
