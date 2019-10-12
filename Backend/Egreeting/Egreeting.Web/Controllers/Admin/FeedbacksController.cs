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

namespace Egreeting.Web.Controllers.Admin
{
    [LogAction]
    public class FeedbacksController : BaseAdminController
    {
        private IFeedbackBusiness FeedbackBusiness;
        public FeedbacksController(IFeedbackBusiness FeedbackBusiness)
        {
            this.FeedbackBusiness = FeedbackBusiness;
        }

        // GET: Feedbacks
        public ActionResult Index()
        {
            return View(ViewNamesConstant.AdminFeedbacksIndex,FeedbackBusiness.All.ToList());
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
        public ActionResult Create([Bind(Include = "FeedbackID,Subject,Content,CreatedDate,ModifiedDate")] Feedback feedback)
        {
            if (ModelState.IsValid)
            {
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
        public ActionResult Edit([Bind(Include = "FeedbackID,Subject,Content,CreatedDate,ModifiedDate")] Feedback feedback)
        {
            if (ModelState.IsValid)
            {
                FeedbackBusiness.Update(feedback);
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
        public ActionResult DeleteConfirmed(int id)
        {
            Feedback feedback = FeedbackBusiness.Find(id);
            FeedbackBusiness.Delete(feedback);
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
