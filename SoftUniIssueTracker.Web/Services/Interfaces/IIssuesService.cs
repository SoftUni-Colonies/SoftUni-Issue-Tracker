using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SIT.Models;
using SIT.Web.BindingModels;
using SIT.Web.ViewModels.Issue;
using SIT.Web.ViewModels.Project;
using SIT.Web.ViewModels.Status;

namespace SIT.Web.Services.Interfaces
{
    public interface IIssuesService
    {
        IssueViewModel Add(string authorId, IssueBindingModel model);
        IssueViewModel Edit(int id, IssueEditBindingModel model);
        IEnumerable<IssueViewModel> Get(string filter);
        IEnumerable<IssueViewModel> GetUserIssues(string userId, string orderBy);
        IEnumerable<IssueViewModel> GetProjectIssues(int projectId);
        IssueViewModel GetById(int id);
        IEnumerable<StatusViewModel> ChangeStatus(int issueId, int statusId);
    }
}
