using SIT.Data.Repositories;
using SIT.Models;

namespace SIT.Data.Interfaces
{
    public interface ISoftUniIssueTrackerData
    {
        EntityRepository<Project> ProjectRepository { get; }
        EntityRepository<Comment> CommentRepository { get; }
        EntityRepository<Issue> IssueRepository { get; }
        EntityRepository<Label> LabelRepository { get; }
        EntityRepository<Priority> PriorityRepository { get; }
        EntityRepository<Status> StatusRepository { get; }
        EntityRepository<StatusTransition> StatusTransitionRepository { get; }
        EntityRepository<TransitionScheme> TransitionSchemeRepository { get; }
    }
}
