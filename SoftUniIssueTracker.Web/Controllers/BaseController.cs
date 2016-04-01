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
        protected string userId = "e8d700d6-e0ab-4768-92b1-ab9ac63c69c0";
    }
}
