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

namespace Egreeting.Web.Controllers.Admin
{
    [LogAction]
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
                listModel = FeedbackBusiness.All.Where(x => x.Subject.Contains(search) && !x.Status).OrderBy(x => x.FeedbackID).Skip((page - 1) * pageSize).Take(pageSize).ToList();
                ViewBag.totalItem = FeedbackBusiness.All.Count(x => x.Subject.Contains(search) && !x.Status);
            }
            else
            {
                ViewBag.totalItem = FeedbackBusiness.All.Count(x => !x.Status);
                listModel = FeedbackBusiness.All.Where(x => !x.Status).OrderBy(x => x.FeedbackID).Skip((page - 1) * pageSize).Take(pageSize).ToList();
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
            return View(ViewNamesConstant.AdminFeedbacksCreate);
        }

        // POST: Feedbacks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Subject,Message")] Feedback feedback, int EcardId)
        {
            if (ModelState.IsValid)
            {
                var user = new EgreetingUser();
                var currentContext = System.Web.HttpContext.Current;
                if (currentContext.User != null)
                {
                    string email = Membership.GetUser().Email;
                    user = EgreetingUserBusiness.All.Where(x => x.Email.Equals(email)).FirstOrDefault();
                }
                if (user != null)
                    feedback.EgreetingUser = user;

                var ecard = EcardBusiness.Find(EcardId);
                if (ecard != null)
                    feedback.Ecard = ecard;

                FeedbackBusiness.Insert(feedback);
                FeedbackBusiness.Save();
                return RedirectToAction("Index");
            }

            return View(ViewNamesConstant.AdminFeedbacksCreate,feedback);
        }

        // GET: Feedbacks/Edit/5
        public ActionResult Edit(int? id)
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
            return View(ViewNamesConstant.AdminFeedbacksEdit,feedback);
        }

        // POST: Feedbacks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FeedbackID,Subject,Message")] Feedback feedback, int EcardId)
        {
            if (ModelState.IsValid)
            {
                var feedbackUpdate = FeedbackBusiness.Find(feedback.FeedbackID);
                feedbackUpdate.Subject = feedback.Subject;
                feedbackUpdate.Message = feedback.Message;
                FeedbackBusiness.Update(feedbackUpdate);
                FeedbackBusiness.Save();
                return RedirectToAction("Index");
            }
            return View(ViewNamesConstant.AdminFeedbacksEdit,feedback);
        }

        // GET: Feedbacks/Delete/5
        public ActionResult Delete(int? id)
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
            return View(ViewNamesConstant.AdminFeedbacksDelete,feedback);
        }

        // POST: Feedbacks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int ItemID)
        {
            Feedback feedback = FeedbackBusiness.Find(ItemID);
            feedback.Status = true;
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
