using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using SIT.Web.BindingModels;
using SIT.Web.BindingModels.Comment;
using SIT.Web.BindingModels.Issue;
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

            var issue = this.issuesService.Add(this.userId, model);
            return CreatedAtRoute("GetIssueById", new { id = issue.Id }, issue);
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult Edit(int id, IssueEditBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.HttpBadRequest(ModelState);
            }

            var issue = this.issuesService.Edit(id, model);
            return new JsonResult(issue);
        }

        [HttpGet]
        [Route("")]
        public IActionResult Get(int pageSize, int pageNumber, string filter)
        {
            var issues = issuesService.Get(pageSize, pageNumber, filter);
            return new JsonResult(issues);
        }

        [HttpGet]
        [Route("me")]
        public IActionResult GetUserIssues(int pageSize, int pageNumber, string orderBy)
        {
            var issues = issuesService.GetUserIssues(this.userId, pageSize, pageNumber, orderBy);
            return new JsonResult(issues);
        }

        [HttpGet]
        [Route("{id}", Name = "GetIssueById")]
        public IActionResult GetById(int id)
        {
            var issue = issuesService.GetById(id);
            return new JsonResult(issue);
        }

        [HttpPut]
        [Route("{id}/changestatus")]
        public IActionResult ChangeStatus(int id, int statusId)
        {
            if (!ModelState.IsValid)
            {
                return this.HttpBadRequest(ModelState);
            }

            var availableStatuses = issuesService.ChangeStatus(id, statusId);
            return new JsonResult(availableStatuses);
        }

        [HttpPost]
        [Route("{id}/comments")]
        public IActionResult AddComment(int id, CommentBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.HttpBadRequest(ModelState);
            }

            var allIssueComments = issuesService.AddComment(id, this.userId, model);
            return CreatedAtRoute("GetIssueComments", new { id = id }, allIssueComments);
        }

        [HttpGet]
        [Route("{id}/comments", Name = "GetIssueComments")]
        public IActionResult GetIssueComments(int id)
        { 
            var comments = issuesService.GetIssueComments(id);
            return new JsonResult(comments);
        }
    }
}