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
        protected string userId = "1a6d7120-2aa8-4d39-b0fb-63a88bce88b1";
    }
}
