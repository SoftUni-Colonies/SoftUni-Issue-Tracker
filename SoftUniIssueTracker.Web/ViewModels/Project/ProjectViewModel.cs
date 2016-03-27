using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SIT.Models;
using SIT.Web.ViewModels.Label;
using SIT.Web.ViewModels.Priority;

namespace SIT.Web.ViewModels.Project
{
    public class ProjectViewModel
    {
        public string Name { get; set; }

        public string ProjectKey { get; set; }

        public string Description { get; set; }

        public string LeadId { get; set; }

        public int TransitionSchemeId { get; set; }

        public List<LabelViewModel> Labels { get; set; }

        public List<PriorityViewModel> Priorities { get; set; }
    }
}
