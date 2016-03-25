namespace SIT.Models
{
    public class StatusTransition
    {
        public int Id { get; set; }
        
        public int TransitionSchemeId { get; set; }
        
        public virtual TransitionScheme TransitionScheme { get; set; }

        public int ChildStatusId { get; set; }

        public virtual Status ChildStatus { get; set; }

        public int ParentStatusId { get; set; }

        public virtual Status ParentStatus { get; set; }
    }
}