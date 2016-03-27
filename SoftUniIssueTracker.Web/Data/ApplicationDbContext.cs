using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Internal;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Framework.Configuration;
using Newtonsoft.Json;
using NuGet;
using Microsoft.Framework.Configuration.Json;
using Microsoft.Framework;
using SIT.Models;

namespace SIT.Data
{

    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public DbSet<Project> Projects { get; set; }
        public DbSet<Issue> Issues { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Label> Labels { get; set; }
        public DbSet<Priority> Priorities { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<StatusTransition> StatusTransitions { get; set; }
        public DbSet<TransitionScheme> TransitionSchemes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.;Database=SoftUniIssueTracker;Trusted_Connection=True;MultipleActiveResultSets=true");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //builder.Entity<ProjectLabel>().HasKey(x => new {x.ProjectId, x.LabelId});
            //builder.Entity<IssueLabel>().HasKey(x => new {x.IssueId, x.LabelId});
            //builder.Entity<ProjectPriority>().HasKey(x => new {x.ProjectId, x.PriorityId});

            foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            base.OnModelCreating(builder);
        }
    }
}