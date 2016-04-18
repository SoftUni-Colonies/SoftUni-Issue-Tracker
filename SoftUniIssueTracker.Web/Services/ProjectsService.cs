using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNet.Mvc;
using Microsoft.Data.Entity;
using SIT.Data.Interfaces;
using SIT.Models;
using SIT.Web.BindingModels;
using SIT.Web.BindingModels.Project;
using SIT.Web.Services.Interfaces;
using SIT.Web.ViewModels.Label;
using SIT.Web.ViewModels.Priority;
using SIT.Web.ViewModels.Project;

namespace SIT.Web.Services
{
    public class ProjectsService : BaseService, IProjectsService
    {
        private IMapper mapper;
        private ISoftUniIssueTrackerData data;
        private ITransitionSchemeService transitionSchemeService;

        public ProjectsService(ISoftUniIssueTrackerData data, IMapper mapper, ITransitionSchemeService transitionSchemeService)
        {
            this.data = data;
            this.mapper = mapper;
            this.transitionSchemeService = transitionSchemeService;
        }

        public ProjectViewModel Add(string authorId, ProjectBindingModel model)
        {
            var project = mapper.Map<ProjectBindingModel, Project>(model);
            project.LeadId = authorId;

            AddTransitionScheme(model.TransitionSchemeId, project);
            if (model.Labels != null)
                AddLabels(model.Labels, project);
            AddPriorities(model.Priorities, project);

            this.data.ProjectRepository.Insert(project);
            this.data.Save();

            return GetMappedProject(project);
        }

        public ProjectViewModel Edit(int id, ProjectEditBindingModel model)
        {
            var project = this.data.ProjectRepository.GetById(id)
                .Include(p => p.ProjectLabels)
                .ThenInclude(p => p.Label)
                .Include(p => p.ProjectPriorities)
                .ThenInclude(p => p.Priority)
                .FirstOrDefault();
            if (project == null)
            {
                throw new ArgumentException(Constants.UnexistingProjectErrorMessage);
            }

            project.Name = model.Name;
            project.Description = model.Description;
            project.LeadId = model.LeadId;

            if (model.Labels != null)
            {
                var labels = this.data.ProjectLabelsRepository.Get().Where(pl => pl.ProjectId == project.Id);
                foreach (var projectLabel in labels)
                {
                    this.data.ProjectLabelsRepository.Delete(projectLabel);
                }

                AddLabels(model.Labels, project);
            }

            if (model.Priorities != null)
            {
                var priorities = this.data.ProjectPrioritiesRepository.Get().Where(pl => pl.ProjectId == project.Id);
                foreach (var priority in priorities)
                {
                    this.data.ProjectPrioritiesRepository.Delete(priority);
                }

                AddPriorities(model.Priorities, project);
            }
            AddTransitionScheme(model.TransitionSchemeId, project);

            this.data.Save();

            return GetMappedProject(project);
        }

        public IEnumerable<ProjectViewModel> Get()
        {
            var projects = this.data.ProjectRepository.Get()
                .Include(p => p.ProjectLabels)
                .ThenInclude(p => p.Label)
                .Include(p => p.ProjectPriorities)
                .ThenInclude(p => p.Priority)
                .ToList();

            return GetMappedProjects(projects);
        }

        public ProjectViewModel GetById(int id)
        {
            var project = this.data.ProjectRepository.GetById(id)
                .Include(p => p.Lead)
                .Include(p => p.ProjectLabels)
                .ThenInclude(p => p.Label)
                .Include(p => p.ProjectPriorities)
                .ThenInclude(p => p.Priority)
                .FirstOrDefault();

            if (project == null)
            {
                throw new ArgumentException(Constants.UnexistingProjectErrorMessage);
            }
            
            return GetMappedProject(project);
        }

        private void AddLabels(IEnumerable<Label> labels, Project project)
        {
            foreach (var label in labels)
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

                project.ProjectLabels.Add(projectLabel);
            }
        }

        private void AddPriorities(List<Priority> priorities , Project project)
        {
            foreach (var priority in priorities)
            {
                var priorityEntity = this.data.PriorityRepository.Get(l => l.Name == priority.Name).FirstOrDefault();
                if (priorityEntity == null)
                {
                    priorityEntity = new Priority()
                    {
                        Name = priority.Name
                    };
                    this.data.PriorityRepository.Insert(priorityEntity);
                }

                var projectPriority = new ProjectPriority()
                {
                    Priority = priorityEntity,
                    Project = project
                };

                project.ProjectPriorities.Add(projectPriority);
            }
        }

        private void AddTransitionScheme(int? transitionSchemeId, Project project)
        {
            if (transitionSchemeId != null)
            {
                var transitionSchemeEntity = this.data.TransitionSchemeRepository.GetById(transitionSchemeId.Value);
                if (transitionSchemeEntity == null)
                {
                    throw new ArgumentException(Constants.UnexistingTransitionSchemeErrorMessage);
                }
                project.TransitionSchemeId = transitionSchemeId.Value;
            }
            else
            {
                var transitionScheme = this.data.TransitionSchemeRepository.Get(t => t.IsDefault).FirstOrDefault();
                if (transitionScheme == null)
                {
                    transitionScheme = transitionSchemeService.AddDefaultTransitionScheme();
                }

                project.TransitionScheme = transitionScheme;
            }
        }

        private ProjectViewModel GetMappedProject(Project project)
        {
            return GetMappedProjects(new List<Project>() {project}).First();
        }

        private IEnumerable<ProjectViewModel> GetMappedProjects(IEnumerable<Project> projects)
        {
            var mappedProjects = new List<ProjectViewModel>();

            foreach (var project in projects)
            {
                var mappedProject = this.mapper.Map<Project, ProjectViewModel>(project);
                mappedProject.Labels =
                    this.mapper.Map<ICollection<ProjectLabel>, ICollection<LabelViewModel>>(project.ProjectLabels);
                mappedProject.Priorities =
                    this.mapper.Map<ICollection<ProjectPriority>, ICollection<PriorityViewModel>>(
                        project.ProjectPriorities);
                mappedProjects.Add(mappedProject);
            }

            return mappedProjects;
        }
    }
}
