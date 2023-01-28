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
            base.OnModelCreating(builder);
        }

        public DbSet<Apps.Models.Act> Act { get; set; }
        public DbSet<Apps.Models.Comment> Comment { get; set; }
        public DbSet<Apps.Models.Pdf> Pdf { get; set; }
        public DbSet<Apps.Models.ApplicationUserDataChangeRequest> ApplicationUserDataChangeRequests { get; set; }
        public DbSet<Apps.Models.Expert> Experts { get; set; }

    }
}
