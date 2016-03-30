using SIT.Data.Interfaces;

namespace SIT.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class TransitionScheme : IDentificatable<int>
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

        public bool IsDefault { get; set; }

        public virtual ICollection<StatusTransition> StatusTransitions
        {
            get { return this.transitions; }
            set { this.transitions = value; }
        } 
    }
}
