namespace MoodDetector.DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class FacebookPersonalityInsightsConsumptionPreference
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public FacebookPersonalityInsightsConsumptionPreference()
        {
            FacebookPersonalityInsightsConsumptionPreferences1 = new HashSet<FacebookPersonalityInsightsConsumptionPreference>();
        }

        [Key]
        public long FacebookPersonalityInsightsConsumptionPreferencesId { get; set; }

        public long FacebookPersonalityInsightsId { get; set; }

        [Required]
        public string PreferenceCategoryId { get; set; }

        public string PreferenceCategoryName { get; set; }

        public long? ParentFacebookPersonalityInsightsConsumptionPreferencesId { get; set; }

        public string PreferenceId { get; set; }

        public string PreferenceName { get; set; }

        public double? PreferenceScore { get; set; }

        public virtual FacebookPersonalityInsight FacebookPersonalityInsight { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FacebookPersonalityInsightsConsumptionPreference> FacebookPersonalityInsightsConsumptionPreferences1 { get; set; }

        public virtual FacebookPersonalityInsightsConsumptionPreference FacebookPersonalityInsightsConsumptionPreference1 { get; set; }
    }
}
