namespace MoodDetector.DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("FacebookUserPost")]
    public partial class FacebookUserPost
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public FacebookUserPost()
        {
            FacebookUserPostKeyPhrases = new HashSet<FacebookUserPostKeyPhras>();
            FacebookUserPostSentiments = new HashSet<FacebookUserPostSentiment>();
        }

        public long FacebookUserPostId { get; set; }

        [Required]
        [StringLength(50)]
        public string PostId { get; set; }

        public long FacebookProfileId { get; set; }

        [StringLength(500)]
        public string Caption { get; set; }

        public string Description { get; set; }

        [StringLength(500)]
        public string From { get; set; }

        [StringLength(500)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Status_Type { get; set; }

        public string Story { get; set; }

        [StringLength(500)]
        public string Type { get; set; }

        public string Message { get; set; }

        public DateTimeOffset DatePosted { get; set; }

        public virtual FacebookProfile FacebookProfile { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FacebookUserPostKeyPhras> FacebookUserPostKeyPhrases { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FacebookUserPostSentiment> FacebookUserPostSentiments { get; set; }
    }
}
