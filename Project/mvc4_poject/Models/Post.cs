using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace mvc4_poject.Models
{
    public class Post
    {
        public long id { get; set; }
        public string title { get; set; }
        public string author { get; set; }
        public string IdAuthor { get; set; }
        public string date { get; set; }

        public string intro { get; set; }
        public string body { get; set; }

        public string category { get; set; }
        public string images { get; set; }
        public string video { get; set; }
    }
    public class PostDBContext : DbContext
    {
        public DbSet<Post> Posts { get; set; }
    }
}