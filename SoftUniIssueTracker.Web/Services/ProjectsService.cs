using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        private ISoftUniIssueTrackerData data;

        public ProjectsService(ISoftUniIssueTrackerData data)
        {
            this.data = data;
        }

        public void Add(ProjectBindingModel model)
        {
            var project = new Project()
            {
                Name = model.Name,
                Description = model.Description,
                LeadId = model.LeadId,
                ProjectKey = model.ProjectKey,
            };

            AddTransitionScheme(model, project);
            AddLabels(model, project);
            AddPriorities(model, project);

            this.data.ProjectRepository.Insert(project);
            this.data.Save();
        }

        public void Edit(int id, ProjectBindingModel model)
        {
            var project = this.data.ProjectRepository.GetById(id)
                .Include(p => p.ProjectLabels)
                .ThenInclude(p => p.Label)
                .Include(p => p.ProjectPriorities)
                .ThenInclude(p => p.Priority)
                .FirstOrDefault();
            if (project == null)
            {
                throw new ArgumentException("Project not found");
            }

            project.Name = model.Name;
            project.Description = model.Description;
            project.ProjectKey = model.ProjectKey;
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

            AddTransitionScheme(model, project);
            AddLabels(model, project);
            AddPriorities(model, project);

            this.data.Save();
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
                var projectViewModel = new ProjectViewModel()
                {
                    Name = project.Name,
                    Description = project.Description,
                    ProjectKey = project.ProjectKey,
                    TransitionSchemeId = project.TransitionSchemeId,
                    LeadId = project.LeadId
                };

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

            var projectViewModel = new ProjectViewModel()
            {
                Name = project.Name,
                Description = project.Description,
                ProjectKey = project.ProjectKey,
                TransitionSchemeId = project.TransitionSchemeId,
                LeadId = project.LeadId
            };

            projectViewModel.Labels = new List<LabelViewModel>();
            projectViewModel.Priorities = new List<PriorityViewModel>();

            foreach (var projectLabel in project.ProjectLabels)
            {
                projectViewModel.Labels.Add(new LabelViewModel() { Name = projectLabel.Label.Name});
            }

            foreach (var projectPriority in project.ProjectPriorities)
            {
                projectViewModel.Priorities.Add(new PriorityViewModel() { Name = projectPriority.Priority.Name });
            }
            return projectViewModel;
        }

        private void AddLabels(ProjectBindingModel model, Project project)
        {
            //var modelLabelNames = model.Labels.Select(l => l.Name).ToList();
            //foreach (var projectLabel in project.ProjectLabels.Where(projectLabel => modelLabelNames.Contains(projectLabel.Label.Name)))
            //{
            //    model.Labels.RemoveAt(modelLabelNames.IndexOf(projectLabel.Label.Name));
            //}

            foreach (var label in model.Labels)
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

        private void AddPriorities(ProjectBindingModel model, Project project)
        {
            foreach (var priority in model.Priorities)
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

        private void AddTransitionScheme(ProjectBindingModel model, Project project)
        {
            if (model.TransitionSchemeId != null)
            {
                var transitionSchemeEntity = this.data.TransitionSchemeRepository.GetById(model.TransitionSchemeId.Value);
                if (transitionSchemeEntity == null)
                {
                    throw new ArgumentException("Invalid transition scheme id.");
                }
                project.TransitionSchemeId = model.TransitionSchemeId.Value;
            }
            else
            {
                var transitionScheme = new TransitionScheme()
                {
                    Name = model.Name + " transition scheme"
                };

                this.data.TransitionSchemeRepository.Insert(transitionScheme);
                project.TransitionScheme = transitionScheme;
            }
        }
    }
}
