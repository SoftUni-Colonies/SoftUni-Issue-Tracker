namespace SIT.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Priority
    {
        private ICollection<Project> projects;
        private ICollection<Issue> issues; 

        public Priority()
        {
            this.projects = new HashSet<Project>();
            this.issues = new HashSet<Issue>();
        }

        [Key]
        public int Id { get; set; } 

        [Required]
        public string Name { get; set; }

        public virtual ICollection<Project> Projects
        {
            get { return this.projects; }
            set { this.projects = value; }
        }

        public virtual ICollection<Issue> Issues
        {
            get { return this.issues; }
            set { this.issues = value; }
        } 
    }
}