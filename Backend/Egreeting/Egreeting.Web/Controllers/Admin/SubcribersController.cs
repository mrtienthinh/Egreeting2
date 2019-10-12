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
    public class SubcribersController : BaseAdminController
    {
        private ISubcriberBusiness SubcriberBusiness;
        public SubcribersController(ISubcriberBusiness SubcriberBusiness)
        {
            this.SubcriberBusiness = SubcriberBusiness;
        }

        // GET: Subcribers
        public ActionResult Index()
        {
            return View(ViewNamesConstant.AdminSubcribersIndex,SubcriberBusiness.All.ToList());
        }

        // GET: Subcribers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subcriber subcriber = SubcriberBusiness.Find(id);
            if (subcriber == null)
            {
                return HttpNotFound();
            }
            return View(ViewNamesConstant.AdminSubcribersDetails, subcriber);
        }

        // GET: Subcribers/Create
        public ActionResult Create()
        {
            return View(ViewNamesConstant.AdminSubcribersCreate);
        }

        // POST: Subcribers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SubcriberID,SubcriberEmail,CreatedDate,ModifiedDate")] Subcriber subcriber)
        {
            if (ModelState.IsValid)
            {
                SubcriberBusiness.Insert(subcriber);
                SubcriberBusiness.Save();
                return RedirectToAction("Index");
            }

            return View(ViewNamesConstant.AdminSubcribersCreate,subcriber);
        }

        // GET: Subcribers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subcriber subcriber = SubcriberBusiness.Find(id);
            if (subcriber == null)
            {
                return HttpNotFound();
            }
            return View(ViewNamesConstant.AdminSubcribersEdit,subcriber);
        }

        // POST: Subcribers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SubcriberID,SubcriberEmail,CreatedDate,ModifiedDate")] Subcriber subcriber)
        {
            if (ModelState.IsValid)
            {
                SubcriberBusiness.Update(subcriber);
                SubcriberBusiness.Save();
                return RedirectToAction("Index");
            }
            return View(ViewNamesConstant.AdminSubcribersEdit,subcriber);
        }

        // GET: Subcribers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subcriber subcriber = SubcriberBusiness.Find(id);
            if (subcriber == null)
            {
                return HttpNotFound();
            }
            return View(ViewNamesConstant.AdminSubcribersDelete,subcriber);
        }

        // POST: Subcribers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Subcriber subcriber = SubcriberBusiness.Find(id);
            SubcriberBusiness.Delete(subcriber);
            SubcriberBusiness.Save();
            return RedirectToAction("Index");
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
