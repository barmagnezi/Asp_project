using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace mvc4_poject.Models
{
    public class Comment
    {
        public long id { get; set; }
        public long postId { get; set; }
        public string title { get; set; }
        public string author { get; set; }
        public string body { get; set; }

        public class CommentDBContext : DbContext
        {
            public DbSet<Comment> Comments { get; set; }
        }
    }
}