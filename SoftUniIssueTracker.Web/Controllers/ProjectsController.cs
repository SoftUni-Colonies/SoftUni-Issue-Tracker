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

        public ProjectsController(IProjectsService projectsService)
        {
            this.projectsService = projectsService;
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
                this.projectsService.Add(model);
                return new HttpOkResult();
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(e.Message);
            }
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult Edit(int id, ProjectBindingModel model)
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
            var projects = this.projectsService.Get();
            return new JsonResult(projects);
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult Get(int id)
        {
            var project = this.projectsService.GetById(id);
            return new JsonResult(project);
        }
    }
}
