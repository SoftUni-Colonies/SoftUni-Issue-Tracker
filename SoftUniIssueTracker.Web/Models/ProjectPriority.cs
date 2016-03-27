using System.ComponentModel.DataAnnotations;
using SIT.Data.Interfaces;

namespace SIT.Models
{
    public class ProjectPriority : IDentificatable<int>
    {
        [Key]
        public int Id { get; set; }

        public int ProjectId { get; set; }
        public virtual Project Project { get; set; }

        public int PriorityId { get; set; }
        public virtual Priority Priority { get; set; }
    }
}
