using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Infrastructure;
using SIT.Data;
using SIT.Models;
using SIT.Web.BindingModels;

namespace SIT.Web.Controllers
{
    public class ProjectsController : BaseController
    {
        [HttpPost]
        public IActionResult Add(ProjectBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.HttpBadRequest(ModelState);
            }

            var project = new Project()
            {
                Name = model.Name,
                Description = model.Description,
                LeadId = this.userId,
                ProjectKey = model.ProjectKey,
            };

            if (model.TransitionSchemeId != null)
            {
                project.TransitionSchemeId = model.TransitionSchemeId.Value;
            }
            else
            {
                var transitionScheme = new TransitionScheme()
                {
                    Name = model.Name + " transition scheme"
                };

                this.data.TransitionSchemeRepository.Insert(transitionScheme);
                project.TransitionScheme = transitionScheme;
            }
            foreach (var label in model.Labels)
            {
                var labelEntity = this.data.LabelRepository.Get(l => l.Name == label.Name).FirstOrDefault();
                if (labelEntity == null)
                {
                    labelEntity = new Label()
                    {
                        Name = label.Name
                    };
                    this.data.LabelRepository.Insert(labelEntity);
                }

                var projectLabel = new ProjectLabel()
                {
                    Label = labelEntity,
                    Project = project
                };
                this.data.ProjectLabelRepository.Insert(projectLabel);

                project.ProjectLabels.Add(projectLabel);
            }

            this.data.ProjectRepository.Insert(project);

            this.data.Save();
            return new HttpOkResult();
            return CreatedAtRoute("GetProject", new { controller = "Projects", id = project.Id });
        }

        public IActionResult Get()
        {
            var projects = this.data.ProjectRepository.Get();
            return new JsonResult(projects);
        }

        public IActionResult Get(int id)
        {
            var project = this.data.ProjectRepository.GetById(id);
            return new JsonResult(project);
        }

        protected override void Dispose(bool disposing)
        {
            this.data.Dispose();
            base.Dispose(disposing);
        }
    }
}
