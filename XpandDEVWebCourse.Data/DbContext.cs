using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace XpandDEVWebCourse.Data
{
    public partial class CourseDbContext : DbContext
    {
        public CourseDbContext() : base() { }

        public CourseDbContext(DbContextOptions options) : base(options) { }

        public virtual DbSet<Cars> Cars { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cars>(e =>
            {
                e.HasKey(m => m.Id);

                e.Property(m => m.Model).HasMaxLength(80).IsRequired();

                e.Property(m => m.ExternalId);
            });
        }
    }
}
