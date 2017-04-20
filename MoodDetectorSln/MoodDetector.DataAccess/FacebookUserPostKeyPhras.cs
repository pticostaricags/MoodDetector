namespace MoodDetector.DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("FacebookUserPostKeyPhrases")]
    public partial class FacebookUserPostKeyPhras
    {
        [Key]
        public long FacebookUserPostKeyPhrasesId { get; set; }

        public long FacebookUserPostId { get; set; }

        [Required]
        public string KeyPhrases { get; set; }

        public virtual FacebookUserPost FacebookUserPost { get; set; }
    }
}
