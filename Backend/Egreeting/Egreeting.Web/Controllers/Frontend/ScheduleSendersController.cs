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
    public class ScheduleSendersController : Controller
    {
        private IScheduleSenderBusiness ScheduleSenderBusiness;
        public ScheduleSendersController(IScheduleSenderBusiness ScheduleSenderBusiness)
        {
            this.ScheduleSenderBusiness = ScheduleSenderBusiness;
        }

        // GET: ScheduleSenders
        public ActionResult Index()
        {
            return View(ViewNamesConstant.FrontendScheduleSendersIndex, ScheduleSenderBusiness.All.ToList());
        }

        // GET: ScheduleSenders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ScheduleSender ScheduleSender = ScheduleSenderBusiness.Find(id);
            if (ScheduleSender == null)
            {
                return HttpNotFound();
            }
            return View(ViewNamesConstant.FrontendScheduleSendersDetails, ScheduleSender);
        }

        // GET: ScheduleSenders/Create
        public ActionResult Create()
        {
            return View(ViewNamesConstant.FrontendScheduleSendersCreate);
        }

        // POST: ScheduleSenders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ScheduleSenderID,ScheduleSenderEmail,CreatedDate,ModifiedDate")] ScheduleSender ScheduleSender)
        {
            if (ModelState.IsValid)
            {
                ScheduleSenderBusiness.Insert(ScheduleSender);
                ScheduleSenderBusiness.Save();
                return RedirectToAction("Index");
            }

            return View(ViewNamesConstant.FrontendScheduleSendersCreate, ScheduleSender);
        }

        // GET: ScheduleSenders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ScheduleSender ScheduleSender = ScheduleSenderBusiness.Find(id);
            if (ScheduleSender == null)
            {
                return HttpNotFound();
            }
            return View(ViewNamesConstant.FrontendScheduleSendersEdit, ScheduleSender);
        }

        // POST: ScheduleSenders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ScheduleSenderID,ScheduleSenderEmail,CreatedDate,ModifiedDate")] ScheduleSender ScheduleSender)
        {
            if (ModelState.IsValid)
            {
                ScheduleSenderBusiness.Update(ScheduleSender);
                ScheduleSenderBusiness.Save();
                return RedirectToAction("Index");
            }
            return View(ViewNamesConstant.FrontendScheduleSendersEdit, ScheduleSender);
        }

        // GET: ScheduleSenders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ScheduleSender ScheduleSender = ScheduleSenderBusiness.Find(id);
            if (ScheduleSender == null)
            {
                return HttpNotFound();
            }
            return View(ViewNamesConstant.FrontendScheduleSendersDelete, ScheduleSender);
        }

        // POST: ScheduleSenders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ScheduleSender ScheduleSender = ScheduleSenderBusiness.Find(id);
            ScheduleSenderBusiness.Delete(ScheduleSender);
            ScheduleSenderBusiness.Save();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                ScheduleSenderBusiness.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
