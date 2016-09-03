using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SimpleBlog.Models {
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser> {
        public static ApplicationDbContext Create() {
            return new ApplicationDbContext();
        }
        public ApplicationDbContext() : base("DefaultConnection", throwIfV1Schema: false) {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder) {
            modelBuilder.Entity<Post>()
                .HasMany(p => p.Tags)
                .WithMany();
            base.OnModelCreating(modelBuilder);
        }
        
        public virtual DbSet<Post> Post { get; set; }
        public virtual DbSet<Tag> Tag { get; set; }
    }
}