namespace SIT.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Project
    {
        private ICollection<Priority> priorities;
        private ICollection<Label> labels;
        private ICollection<Issue> issues;

        public Project()
        {
            this.priorities = new HashSet<Priority>();
            this.labels = new HashSet<Label>();
            this.issues = new HashSet<Issue>();
        }

        [Key]
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; } 

        [Required]
        public string ProjectKey { get; set; }

        public string Description { get; set; }

        [Required]
        public string LeadId { get; set; }

        public virtual User Lead { get; set; }

        public int SchemeId { get; set; }

        public virtual TransitionScheme TransitionScheme { get; set; }

        public virtual ICollection<Priority> Priorities
        {
            get { return this.priorities; }
            set { this.priorities = value; }
        }

        public virtual ICollection<Label> Labels
        {
            get { return this.labels; }
            set { this.labels = value; }
        }

        public virtual ICollection<Issue> Issues
        {
            get { return this.issues; }
            set { this.issues = value; }
        }
    }
}