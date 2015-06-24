using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace mvc4_poject.Models
{
    public class Reporter
    {
        public long ID { get; set; }
        public double tz { get; set; }
        public string name { get; set; }
        public string bd { get; set; }
        public string img { get; set; }
        public string info { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
    }
    public class ReporterDBContext : DbContext
    {
        public DbSet<Reporter> Reporter { get; set; }
    }
}