namespace SIT.Data
{
    using System.Data.Entity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;

    public class SITEntities : IdentityDbContext<User>
    {
        public SITEntities()
            : base("SITEntities")
        {
        }

        public virtual IDbSet<Comment> Comments { get; set; } 

        public virtual IDbSet<Issue> Issues { get; set; }
        
        public virtual IDbSet<Label> Lables { get; set; } 

        public virtual IDbSet<Priority> Priorities { get; set; }
        
        public virtual IDbSet<Project> Projects { get; set; }
        
        public virtual IDbSet<TransitionScheme> TransitionSchemes { get; set; }

        public virtual IDbSet<Status> Statuses { get; set; }
        
        public virtual IDbSet<StatusTransition> StatusTransitions { get; set; }  

        public static SITEntities Create()
        {
            return new SITEntities();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StatusTransition>()
                .HasRequired(st => st.ChildStatus)
                .WithMany(s => s.ChildStatuses)
                .HasForeignKey(st => st.ChildStatusId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<StatusTransition>()
                .HasRequired(st => st.ParentStatus)
                .WithMany(s => s.ParentStatuses)
                .HasForeignKey(st => st.ParentStatusId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TransitionScheme>()
                .HasMany(ts => ts.StatusTransitions)
                .WithRequired(s => s.TransitionScheme)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(u => u.AuthoredIssues)
                .WithRequired(issue => issue.Author)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(u => u.AssignedIssues)
                .WithRequired(issue => issue.Assignee)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(u => u.LeadingProjects)
                .WithRequired(p => p.Lead)
                .WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);
        }
    }
}