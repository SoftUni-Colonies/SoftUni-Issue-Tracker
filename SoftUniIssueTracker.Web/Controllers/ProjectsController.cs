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

            try
            {
                this.projectsService.Add(this.userId, model);
                return new HttpOkResult();
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(e.Message);
            }
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult Edit(int id, ProjectEditBindingModel model)
        {
            try
            {
                this.projectsService.Edit(id, model);
                return new HttpOkResult();
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(e.Message);
            }
        }

        [HttpGet]
        [Route("")]
        public IActionResult Get()
        {
            try
            {
                var projects = this.projectsService.Get();
                return new JsonResult(projects);
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(e.Message);
            }
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var project = this.projectsService.GetById(id);
                return new JsonResult(project);
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(e.Message);
            }
        }

        [HttpGet]
        [Route("{id}/issues")]
        public IActionResult GetIssues(int id)
        {
            try
            {
                var issues = issuesService.GetProjectIssues(id);
                return new JsonResult(issues);
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(e.Message);
            }
        }
    }
}
