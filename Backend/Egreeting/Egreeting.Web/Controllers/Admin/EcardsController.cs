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
    public class EcardsController : BaseAdminController
    {
        private IEcardBusiness EcardBusiness;
        public EcardsController(IEcardBusiness EcardBusiness)
        {
            this.EcardBusiness = EcardBusiness;
        }

        // GET: Ecards
        public ActionResult Index()
        {
            return View(ViewNamesConstant.AdminEcardsIndex,EcardBusiness.All.ToList());
        }

        // GET: Ecards/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ecard ecard = EcardBusiness.Find(id);
            if (ecard == null)
            {
                return HttpNotFound();
            }
            return View(ViewNamesConstant.AdminEcardsDetails,ecard);
        }

        // GET: Ecards/Create
        public ActionResult Create()
        {
            return View(ViewNamesConstant.AdminEcardsCreate);
        }

        // POST: Ecards/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EcardID,EcardSlug,EcardType,EcardUrl,CreatedDate,ModifiedDate")] Ecard ecard)
        {
            if (ModelState.IsValid)
            {
                EcardBusiness.Insert(ecard);
                EcardBusiness.Save();
                return RedirectToAction("Index");
            }

            return View(ViewNamesConstant.AdminEcardsCreate, ecard);
        }

        // GET: Ecards/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ecard ecard = EcardBusiness.Find(id);
            if (ecard == null)
            {
                return HttpNotFound();
            }
            return View(ViewNamesConstant.AdminEcardsEdit, ecard);
        }

        // POST: Ecards/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EcardSlug,EcardType,EcardUrl")] Ecard ecard)
        {
            if (ModelState.IsValid)
            {
                EcardBusiness.Update(ecard);
                EcardBusiness.Save();
                return RedirectToAction("Index");
            }
            return View(ViewNamesConstant.AdminEcardsEdit, ecard);
        }

        // GET: Ecards/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ecard ecard = EcardBusiness.Find(id);
            if (ecard == null)
            {
                return HttpNotFound();
            }
            return View(ViewNamesConstant.AdminEcardsDelete,ecard);
        }

        // POST: Ecards/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ecard ecard = EcardBusiness.Find(id);
            EcardBusiness.Delete(ecard);
            EcardBusiness.Save();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                EcardBusiness.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
