using Egreeting.Web.Utils;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Egreeting.Web.Controllers
{
    public class BaseController : Controller
    {
        public GlobalInfo _globalInfo = null;

        public BaseController()
        {
            _globalInfo = GlobalInfo.getInstance();
        }

        public void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }
    }
}