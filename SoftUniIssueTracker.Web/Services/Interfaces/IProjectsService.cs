﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SIT.Models;
using SIT.Web.BindingModels;
using SIT.Web.ViewModels.Project;

namespace SIT.Web.Services.Interfaces
{
    public interface IProjectsService
    {
        void Add(ProjectBindingModel model);
        void Edit(int id, ProjectBindingModel model);
        IEnumerable<ProjectViewModel> Get();
        ProjectViewModel GetById(int id);
    }
}
