using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SIT.Models;

namespace SIT.Web.BindingModels
{
    public class IssueBindingModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public int ProjectId { get; set; }
        public string AssigneeId { get; set; }
        public int PriorityId { get; set; }
        public List<Label> Labels { get; set; } 
    }
}
