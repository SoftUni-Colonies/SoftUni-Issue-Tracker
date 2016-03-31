using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using SIT.Web.BindingModels;
using SIT.Web.Services;
using SIT.Web.Services.Interfaces;

namespace SIT.Web.Controllers
{
    [Route("issues/")]
    public class IssuesController : BaseController
    {
        private IIssuesService issuesService;

        public IssuesController(IIssuesService issuesService)
        {
            this.issuesService = issuesService;
        }

        [HttpPost]
        [Route("")]
        public IActionResult Add(IssueBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.HttpBadRequest(ModelState);
            }

            try
            {
                var issue = this.issuesService.Add(this.userId, model);
                return CreatedAtRoute("GetIssueById", new {id = issue.Id}, issue);
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(e.Message);
            }
        }


        [HttpPut]
        [Route("{id}")]
        public IActionResult Edit(int id, IssueEditBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.HttpBadRequest(ModelState);
            }

            try
            {
                var issue = this.issuesService.Edit(id, model);
                return new JsonResult(issue);
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(e.Message);
            }
        }

        [HttpGet]
        [Route("")]
        public IActionResult Get(string filter)
        {
            try
            {
                var issues = issuesService.Get(filter);
                return new JsonResult(issues);
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(e.Message);
            }
        }

        [HttpGet]
        [Route("me")]
        public IActionResult GetUserIssues(string orderBy)
        {
            try
            {
                var issues = issuesService.GetUserIssues(this.userId, orderBy);
                return new JsonResult(issues);
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(e.Message);
            }
        }

        [HttpGet]
        [Route("{id}", Name = "GetIssueById")]
        public IActionResult GetById(int id)
        {
            try
            {
                var issue = issuesService.GetById(id);
                return new JsonResult(issue);
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(e.Message);
            }
        }

        [HttpPut]
        [Route("{id}/changestatus")]
        public IActionResult ChangeStatus(int id, int statusId)
        {
            if (!ModelState.IsValid)
            {
                return this.HttpBadRequest(ModelState);
            }

            try
            {
                var availableStatuses = issuesService.ChangeStatus(id, statusId);
                return new JsonResult(availableStatuses);
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(e.Message);
            }
        }
    }
}