using System.ComponentModel.DataAnnotations;
using SIT.Data.Interfaces;

namespace SIT.Models
{
    public class ProjectLabel : IDentificatable<int>
    {
        [Key]
        public int Id { get; set; }

        public int ProjectId { get; set; }
        public virtual Project Project { get; set; }

        public int LabelId { get; set; }
        public virtual Label Label { get; set; }
    }
}
