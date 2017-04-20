namespace MoodDetector.DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("FacebookProfile")]
    public partial class FacebookProfile
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public FacebookProfile()
        {
            FacebookUserPosts = new HashSet<FacebookUserPost>();
        }

        public long FacebookProfileId { get; set; }

        [Required]
        [StringLength(50)]
        public string ProfileId { get; set; }

        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        public DateTimeOffset DateRecordCreated { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FacebookUserPost> FacebookUserPosts { get; set; }
    }
}
