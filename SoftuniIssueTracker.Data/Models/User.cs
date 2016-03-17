namespace SIT.Data.Models
{
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    public class User : IdentityUser
    {
        private ICollection<Project> leadingProjects;
        private ICollection<Issue> assignedIssues;
        private ICollection<Issue> authoredIssues;
        private ICollection<Comment> comments; 

        public User()
        {
            this.leadingProjects = new HashSet<Project>();
            this.authoredIssues = new HashSet<Issue>();
            this.assignedIssues = new HashSet<Issue>();
            this.comments = new HashSet<Comment>();
        }

        public virtual ICollection<Project> LeadingProjects
        {
            get { return this.leadingProjects; }
            set { this.leadingProjects = value; }
        }

        public virtual ICollection<Issue> AssignedIssues
        {
            get { return this.assignedIssues; }
            set { this.assignedIssues = value; }
        }

        public virtual ICollection<Issue> AuthoredIssues
        {
            get { return this.authoredIssues; }
            set { this.authoredIssues = value; }
        }

        public virtual ICollection<Comment> Comments
        {
            get { return this.comments; }
            set { this.comments = value; }
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(
            UserManager<User> manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);

            userIdentity.AddClaim(new Claim(ClaimTypes.Name, userIdentity.Name));
            userIdentity.AddClaim(new Claim(ClaimTypes.NameIdentifier, this.Id));

            // Add custom user claims here
            return userIdentity;
        }
    }
}
