using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using SIT.Data;
using SIT.Data.Interfaces;
using SIT.Data.Repositories;

namespace SIT.Web.Controllers
{
    public abstract class BaseController : Controller
    {
        protected ISoftUniIssueTrackerData data;
        protected string userId = "5e126bd3-519d-416f-b13d-9bf272b52274";

        public BaseController(ISoftUniIssueTrackerData data)
        {
            this.data = data;
        }

        public BaseController() : this(new SoftUniIssueTrackerData(new ApplicationDbContext()))
        {
            
        }
    }
}
