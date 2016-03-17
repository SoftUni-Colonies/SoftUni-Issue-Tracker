namespace SIT.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class TransitionScheme
    {
        private ICollection<StatusTransition> transitions;

        public TransitionScheme()
        {
            this.transitions = new HashSet<StatusTransition>();
        }

        [Key]
        public int Id { get; set; } 

        [Required]
        public string Name { get; set; }

        public virtual ICollection<StatusTransition> StatusTransitions
        {
            get { return this.transitions; }
            set { this.transitions = value; }
        } 
    }
}
