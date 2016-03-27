using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SIT.Models;

namespace SIT.Web.BindingModels
{
    public class ProjectBindingModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ProjectKey { get; set; }
        public string LeadId { get; set; }
        public int? TransitionSchemeId { get; set; }
        public List<Label> Labels { get; set; } 
        public List<Priority> Priorities { get; set; } 
    }
}
