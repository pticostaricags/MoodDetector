namespace MoodDetector.DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("FacebookUserPostSentiment")]
    public partial class FacebookUserPostSentiment
    {
        public long FacebookUserPostSentimentId { get; set; }

        public long FacebookUserPostId { get; set; }

        public double Score { get; set; }

        public virtual FacebookUserPost FacebookUserPost { get; set; }
    }
}
