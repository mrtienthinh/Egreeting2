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
using System.Web.Security;
using Egreeting.Models.AppContext;
using Egreeting.Web.Filters;

namespace Egreeting.Web.Controllers.Admin
{
    [LogAction]
    [RoleAuthorize(Roles = "Admin")]
    public class FeedbacksController : BaseAdminController
    {
        private IFeedbackBusiness FeedbackBusiness;
        private IEgreetingUserBusiness EgreetingUserBusiness;
        private IEcardBusiness EcardBusiness;

        public FeedbacksController(IFeedbackBusiness FeedbackBusiness, IEgreetingUserBusiness EgreetingUserBusiness, IEcardBusiness EcardBusiness)
        {
            this.FeedbackBusiness = FeedbackBusiness;
            this.EgreetingUserBusiness = EgreetingUserBusiness;
            this.EcardBusiness = EcardBusiness;
        }

        // GET: Feedbacks
        public ActionResult Index(string search, int page = 1, int pageSize = 10)
        {
            var listModel = new List<Feedback>();
            if (!string.IsNullOrEmpty(search))
            {
                listModel = FeedbackBusiness.All.Where(x => x.Subject.Contains(search) && x.Draft != true).OrderBy(x => x.FeedbackID).Skip((page - 1) * pageSize).Take(pageSize).ToList();
                ViewBag.totalItem = FeedbackBusiness.All.Count(x => x.Subject.Contains(search) && x.Draft != true);
            }
            else
            {
                ViewBag.totalItem = FeedbackBusiness.All.Count(x => x.Draft != true);
                listModel = FeedbackBusiness.All.Where(x => x.Draft != true).OrderBy(x => x.FeedbackID).Skip((page - 1) * pageSize).Take(pageSize).ToList();
            }
            ViewBag.currentPage = page;
            ViewBag.pageSize = pageSize;
            ViewBag.search = search;
            return View(ViewNamesConstant.AdminFeedbacksIndex, listModel);
        }

        // GET: Feedbacks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Feedback feedback = FeedbackBusiness.Find(id);
            if (feedback == null)
            {
                return HttpNotFound();
            }
            return View(ViewNamesConstant.AdminFeedbacksDetails,feedback);
        }

        // GET: Feedbacks/Create
        public ActionResult Create()
        {
            ViewBag.Ecards = EcardBusiness.All.Where(x => x.Draft != true).Select(x => new { x.EcardID, x.EcardName }).ToDictionary(k => k.EcardID, v => v.EcardName);
            return View(ViewNamesConstant.AdminFeedbacksCreate);
        }

        // POST: Feedbacks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Subject,Message")] Feedback feedback, int? EcardID)
        {
            if (ModelState.IsValid)
            {
                using (var context = new EgreetingContext())
                {
                    var user = new EgreetingUser();
                    var currentContext = System.Web.HttpContext.Current;
                    if (currentContext.User != null)
                    {
                        string email = currentContext.User.Identity.Name;
                        user = context.Set<EgreetingUser>().Where(x => x.Email.Equals(email)).FirstOrDefault();
                    }
                    if (user != null)
                        feedback.EgreetingUser = user;

                    var ecard = context.Set<Ecard>().Find(EcardID);
                    if (ecard != null)
                        feedback.Ecard = ecard;

                    feedback.CreatedDate = DateTime.Now;
                    context.Set<Feedback>().Add(feedback);
                    context.SaveChanges();
                }
                    
                return RedirectToAction("Index");
            }

            return View(ViewNamesConstant.AdminFeedbacksCreate,feedback);
        }

        // POST: Feedbacks/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int ItemID)
        {
            Feedback feedback = FeedbackBusiness.Find(ItemID);
            feedback.Draft = true;
            FeedbackBusiness.Update(feedback);
            FeedbackBusiness.Save();
            return RedirectToAction("Index");
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
