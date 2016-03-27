using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Infrastructure;
using SIT.Data;

namespace SIT.Web.Controllers
{
    public class ProjectsController : BaseController
    {
        [Route("/getprojects")]
        public string GetProjects()
        {
            var project = this.data.ProjectRepository.GetById(3);
            return project.Name;
        }
    }
}
