namespace SoftuniIssueTracker.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Status
    {
        private ICollection<Issue> issues;
        private ICollection<StatusTransition> childStatuses;
        private ICollection<StatusTransition> parentStatuses;  

        public Status()
        {
            this.issues= new HashSet<Issue>();
            this.childStatuses = new HashSet<StatusTransition>();
            this.parentStatuses = new HashSet<StatusTransition>();
        }

        [Key]
        public int Id { get; set; }
        
        public string Name { get; set; }

        public virtual ICollection<Issue> Issues
        {
            get { return this.issues; }
            set { this.issues = value; }
        }

        public virtual ICollection<StatusTransition> ChildStatuses
        {
            get { return this.childStatuses; }
            set { this.childStatuses = value; }
        }

        public virtual ICollection<StatusTransition> ParentStatuses
        {
            get { return this.parentStatuses; }
            set { this.parentStatuses = value; }
        }
    }
}