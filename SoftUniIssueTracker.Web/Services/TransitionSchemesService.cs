using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SIT.Data.Interfaces;
using SIT.Models;
using SIT.Web.Services.Interfaces;

namespace SIT.Web.Services
{
    public class TransitionSchemesService : BaseService, ITransitionSchemeService
    {
        private ISoftUniIssueTrackerData data;

        public TransitionSchemesService(ISoftUniIssueTrackerData data)
        {
            this.data = data;
        }

        public TransitionScheme AddDefaultTransitionScheme()
        {
            var transitionScheme = new TransitionScheme()
            {
                Name = Constants.DefaultSchemeName,
                IsDefault = true
            };

            var openStatus = this.data.StatusRepository.Get(s => s.Name == DefaultTransitionSchemeStatuses.Open.ToString()).FirstOrDefault();
            var closedStatus = this.data.StatusRepository.Get(s => s.Name == DefaultTransitionSchemeStatuses.Closed.ToString()).FirstOrDefault();
            if (openStatus == null)
            {
                openStatus = new Status()
                {
                    Name = DefaultTransitionSchemeStatuses.Open.ToString()
                };
                this.data.StatusRepository.Insert(openStatus);
            }

            if (closedStatus == null)
            {
                closedStatus = new Status()
                {
                    Name = DefaultTransitionSchemeStatuses.Closed.ToString()
                };
                this.data.StatusRepository.Insert(closedStatus);
            }

            this.data.StatusTransitionRepository.Insert(new StatusTransition()
            {
                ParentStatus = openStatus,
                ChildStatus = closedStatus,
                TransitionScheme = transitionScheme
            });

            this.data.TransitionSchemeRepository.Insert(transitionScheme);
            return transitionScheme;
        }
    }
}
