using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Infrastructure;
using SIT.Data;
using SIT.Data.Interfaces;
using SIT.Models;
using SIT.Web.BindingModels;
using SIT.Web.BindingModels.Project;
using SIT.Web.Services;
using SIT.Web.Services.Interfaces;

namespace SIT.Web.Controllers
{
    [Route("projects/")]
    public class ProjectsController : BaseController
    {
        private IProjectsService projectsService;
        private IIssuesService issuesService;

        public ProjectsController(IProjectsService projectsService, IIssuesService issuesService)
        {
            this.projectsService = projectsService;
            this.issuesService = issuesService;
        }

        [HttpPost]
        [Route("")]
        public IActionResult Add(ProjectBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.HttpBadRequest(ModelState);
            }

            var project = this.projectsService.Add(this.userId, model);
            return CreatedAtRoute("GetProjectById", new { id = project.Id }, project);
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult Edit(int id, ProjectEditBindingModel model)
        {
            var project = this.projectsService.Edit(id, model);
            return CreatedAtRoute("GetProjectById", new { id = project.Id }, project);
        }

        [HttpGet]
        [Route("")]
        public IActionResult Get()
        {
            var projects = this.projectsService.Get();
            return new JsonResult(projects);
        }

        [HttpGet]
        [Route("{id}", Name = "GetProjectById")]
        public IActionResult GetById(int id)
        {
            var project = this.projectsService.GetById(id);
            return new JsonResult(project);
        }

        [HttpGet]
        [Route("{id}/issues")]
        public IActionResult GetIssues(int id)
        {
            var issues = issuesService.GetProjectIssues(id);
            return new JsonResult(issues);
        }
    }
}
