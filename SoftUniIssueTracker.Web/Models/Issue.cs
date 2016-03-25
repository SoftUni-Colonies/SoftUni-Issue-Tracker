namespace SIT.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Issue
    {
        private ICollection<Label> labels;
        private ICollection<Comment> comments;

        public Issue()
        {
            this.labels = new HashSet<Label>();
            this.comments = new HashSet<Comment>();
        }

        [Key]
        public int Id { get; set; }
        
        [Required]
        public string Title { get; set; }
        
        [Required]
        public string IssueKey { get; set; } 

        [Required]
        public string Description { get; set; }

        public DateTime DueDate { get; set; }

        [Required]
        public int ProjectId { get; set; }

        public virtual Project Project { get; set; }

        [Required]
        public string AuthorId { get; set; }

        public virtual User Author { get; set; }

        public string AssigneeId { get; set; }

        public virtual User Assignee { get; set; }

        public int PriorityId { get; set; }

        public virtual Priority Priority { get; set; }

        public int StatusId { get; set; }

        public virtual Status Status { get; set; }

        public virtual ICollection<Label> Labels
        {
            get { return this.labels; }
            set { this.labels = value; }
        }

        public virtual ICollection<Comment> Comments
        {
            get { return this.comments; }
            set { this.comments = value; }
        }
    }
}