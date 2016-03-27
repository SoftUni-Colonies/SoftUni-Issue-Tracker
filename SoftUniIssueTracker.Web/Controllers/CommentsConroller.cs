using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using SIT.Data.Interfaces;
using SIT.Models;
using SIT.Web.BindingModels;

namespace SIT.Web.Controllers
{
    //public class CommentsConroller : BaseController
    //{

    //    public IActionResult Add(CommentBindingModel model)
    //    {
    //        var commentEntity = new Comment()
    //        {
    //            AuthorId = this.userId,
    //            CreatedOn = DateTime.UtcNow,
    //            IssueId = model.IssueId,
    //            Text = model.Text
    //        };

    //        this.data.CommentRepository.Insert(commentEntity);
    //        this.data.Save();
    //        return new HttpOkResult();
    //    }
    //    public IActionResult GetForIssue(int issueId)
    //    {
    //        var comments = this.data.CommentRepository.Get(c => c.IssueId == issueId);
    //        return new JsonResult(comments);
    //    }

    //    public IActionResult Get(int commentId)
    //    {
    //        var comment = this.data.CommentRepository.GetById(commentId);
    //        return new JsonResult(comment);
    //    }
    //}
}
