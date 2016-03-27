using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SIT.Models;

namespace SIT.Web.BindingModels
{
    public class CommentBindingModel
    {
        public string Text { get; set; }
        public int IssueId { get; set; }
    }
}
