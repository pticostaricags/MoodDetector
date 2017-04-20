using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MoodDetector.Models.Entities.Facebook
{
    public class PostsAnalysis
    {
        public List<DataAccess.FacebookUserPost> UserPosts { get; set; } = null;

        public PostsAnalysis(List<DataAccess.FacebookUserPost> pUserPosts)
        {
            this.UserPosts = pUserPosts;
        }
    }
}