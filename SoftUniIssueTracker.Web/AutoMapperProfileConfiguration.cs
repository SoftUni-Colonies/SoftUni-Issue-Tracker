using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNet.Hosting.Internal;
using SIT.Models;
using SIT.Web.BindingModels;
using SIT.Web.ViewModels.Issue;
using SIT.Web.ViewModels.Priority;
using SIT.Web.ViewModels.Project;
using SIT.Web.ViewModels.Status;
using SIT.Web.ViewModels.User;

namespace SIT.Web
{
    public class AutoMapperProfileConfiguration : Profile
    {
        protected override void Configure()
        {
            CreateMap<Project, ProjectViewModel>();
            CreateMap<Issue, IssueViewModel>();
            CreateMap<ProjectBindingModel, Project>();
            CreateMap<IssueBindingModel, Issue>().ForMember(x => x.IssueLabels, opt => opt.Ignore());
            CreateMap<User, UserViewModel>();
            CreateMap<Priority, PriorityViewModel>();
            CreateMap<Status, StatusViewModel>();
        }
    }
}
