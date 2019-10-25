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
using Egreeting.Models.AppContext;
using System.Web.Security;

namespace Egreeting.Web.Controllers.Frontend
{
    [LogAction]
    public class FeedbacksController : BaseController
    {
        private IFeedbackBusiness FeedbackBusiness;
        public FeedbacksController(IFeedbackBusiness FeedbackBusiness)
        {
            this.FeedbackBusiness = FeedbackBusiness;
        }

        public ActionResult Index()
        {
            return View(ViewNamesConstant.FrontendFeedbacksIndex);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Subject,Message")] Feedback feedback, int? EcardID)
        {
            if (ModelState.IsValid)
            {
                using (var context = new EgreetingContext())
                {
                    var currentContext = System.Web.HttpContext.Current;
                    if (Request.IsAuthenticated)
                    {
                        var user = new EgreetingUser();
                        if (currentContext.User != null)
                        {
                            string email = User.Identity.Name;
                            user = context.Set<EgreetingUser>().Where(x => x.Email.Equals(email)).FirstOrDefault();
                            if (user != null)
                                feedback.EgreetingUser = user;
                        }
                    }

                    var ecard = context.Set<Ecard>().Find(EcardID);
                    if (ecard != null)
                        feedback.Ecard = ecard;

                    feedback.CreatedDate = DateTime.Now;
                    context.Set<Feedback>().Add(feedback);
                    context.SaveChanges();
                }
                return Redirect(Request.UrlReferrer.ToString());
            }
            return Redirect(Request.UrlReferrer.ToString());
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                FeedbackBusiness.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
