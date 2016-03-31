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
            AddLabels(model.Labels, project);
            AddPriorities(model.Priorities, project);

            this.data.ProjectRepository.Insert(project);
            this.data.Save();

            var mapperProject = mapper.Map<Project, ProjectViewModel>(project);
            mapperProject.Labels =
                mapper.Map<ICollection<ProjectLabel>, ICollection<LabelViewModel>>(project.ProjectLabels);
            mapperProject.Priorities =
                mapper.Map<ICollection<ProjectPriority>, ICollection<PriorityViewModel>>(project.ProjectPriorities);
            return mapperProject;
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

            var labels = this.data.ProjectLabelsRepository.Get().Where(pl => pl.ProjectId == project.Id);
            foreach (var projectLabel in labels)
            {
                this.data.ProjectLabelsRepository.Delete(projectLabel);
            }

            var priorities = this.data.ProjectPrioritiesRepository.Get().Where(pl => pl.ProjectId == project.Id);
            foreach (var priority in priorities)
            {
                this.data.ProjectPrioritiesRepository.Delete(priority);
            }

            AddTransitionScheme(model.TransitionSchemeId, project);
            AddLabels(model.Labels, project);
            AddPriorities(model.Priorities, project);

            this.data.Save();

            var mapperProject = mapper.Map<Project, ProjectViewModel>(project);
            mapperProject.Labels =
                mapper.Map<ICollection<ProjectLabel>, ICollection<LabelViewModel>>(project.ProjectLabels);
            mapperProject.Priorities =
                mapper.Map<ICollection<ProjectPriority>, ICollection<PriorityViewModel>>(project.ProjectPriorities);
            return mapperProject;
        }

        public IEnumerable<ProjectViewModel> Get()
        {
            var projects = this.data.ProjectRepository.Get()
                .Include(p => p.ProjectLabels)
                .ThenInclude(p => p.Label)
                .Include(p => p.ProjectPriorities)
                .ThenInclude(p => p.Priority)
                .ToList();

            var projectsViewModels = new List<ProjectViewModel>();

            foreach (var project in projects)
            {
                var projectViewModel = mapper.Map<Project, ProjectViewModel>(project);

                projectViewModel.Labels = new List<LabelViewModel>();
                projectViewModel.Priorities = new List<PriorityViewModel>();

                foreach (var projectLabel in project.ProjectLabels)
                {
                    projectViewModel.Labels.Add(new LabelViewModel() { Name = projectLabel.Label.Name });
                }

                foreach (var projectPriority in project.ProjectPriorities)
                {
                    projectViewModel.Priorities.Add(new PriorityViewModel() { Name = projectPriority.Priority.Name });
                }

                projectsViewModels.Add(projectViewModel);
            }
            return projectsViewModels;
        }

        public ProjectViewModel GetById(int id)
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
            var projectViewModel = mapper.Map<Project, ProjectViewModel>(project);

            projectViewModel.Labels = new List<LabelViewModel>();
            projectViewModel.Priorities = new List<PriorityViewModel>();

            foreach (var projectLabel in project.ProjectLabels)
            {
                projectViewModel.Labels.Add(mapper.Map<ProjectLabel, LabelViewModel>(projectLabel));
            }

            foreach (var projectPriority in project.ProjectPriorities)
            {
                projectViewModel.Priorities.Add(mapper.Map<ProjectPriority, PriorityViewModel>(projectPriority));
            }
            return projectViewModel;
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

                //var projectLabelEntity = this.data.ProjectLabelsRepository.Get(e => e.LabelId == labelEntity.Id
                //                                                            && e.ProjectId == project.Id).FirstOrDefault();
                //if (projectLabelEntity == null)
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

                //var projectPriorityEntity = this.data.ProjectPrioritiesRepository.Get(e => e.PriorityId == priorityEntity.Id
                //                                                           && e.ProjectId == project.Id).FirstOrDefault();
                //if (projectPriorityEntity == null)
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
    }
}
