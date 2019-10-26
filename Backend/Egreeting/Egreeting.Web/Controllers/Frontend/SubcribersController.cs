using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Egreeting.Web.App_Start;
using Egreeting.Domain;
using Egreeting.Business.IBusiness;
using Egreeting.Models.Models;

namespace Egreeting.Web.Controllers.Frontend
{
    [LogAction]
    public class SubcribersController : BaseController
    {
        private ISubcriberBusiness SubcriberBusiness;
        public SubcribersController(ISubcriberBusiness SubcriberBusiness)
        {
            this.SubcriberBusiness = SubcriberBusiness;
        }

        // GET: Subcribers
        public ActionResult Index()
        {
            if (Request.IsAuthenticated)
            {
                return Redirect("/");
            }
            return View(ViewNamesConstant.FrontendSubcribersIndex);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                SubcriberBusiness.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
