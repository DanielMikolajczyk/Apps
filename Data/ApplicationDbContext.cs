using Apps.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Apps.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ApplicationUserExpert>().HasKey(ae => new { ae.ApplicationUserId, ae.ExpertId });
            base.OnModelCreating(builder);
        }

        public DbSet<Apps.Models.Act> Act { get; set; }
        public DbSet<Apps.Models.Comment> Comment { get; set; }
        public DbSet<Apps.Models.Pdf> Pdf { get; set; }
        public DbSet<Apps.Models.ApplicationUserDataChangeRequest> ApplicationUserDataChangeRequests { get; set; }
        public DbSet<Apps.Models.Expert> Experts { get; set; }
        public DbSet<Apps.Models.ApplicationUserExpert> ApplicationUserExperts { get; set; }
        public DbSet<Apps.Models.ApplicationUserExpertChangeRequest> ApplicationUserExpertChangeRequests { get; set; }

    }
}
