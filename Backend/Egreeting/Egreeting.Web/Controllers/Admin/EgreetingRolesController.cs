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
    public class EgreetingRolesController : Controller
    {
        private IEgreetingRoleBusiness EgreetingRoleBusiness;
        public EgreetingRolesController(IEgreetingRoleBusiness EgreetingRoleBusiness)
        {
            this.EgreetingRoleBusiness = EgreetingRoleBusiness;
        }

        // GET: EgreetingRoles
        public ActionResult Index()
        {
            return View(ViewNamesConstant.AdminEgreetingRolesIndex,EgreetingRoleBusiness.All.ToList());
        }

        // GET: EgreetingRoles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EgreetingRole egreetingRole = EgreetingRoleBusiness.Find(id);
            if (egreetingRole == null)
            {
                return HttpNotFound();
            }
            return View(ViewNamesConstant.AdminEgreetingRolesDetails,egreetingRole);
        }

        // GET: EgreetingRoles/Create
        public ActionResult Create()
        {
            return View(ViewNamesConstant.AdminEgreetingRolesCreate);
        }

        // POST: EgreetingRoles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EgreetingRoleID,EgreetingRoleName,CreatedDate,ModifiedDate")] EgreetingRole egreetingRole)
        {
            if (ModelState.IsValid)
            {
                EgreetingRoleBusiness.Insert(egreetingRole);
                EgreetingRoleBusiness.Save();
                return RedirectToAction("Index");
            }

            return View(ViewNamesConstant.AdminEgreetingRolesCreate,egreetingRole);
        }

        // GET: EgreetingRoles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EgreetingRole egreetingRole = EgreetingRoleBusiness.Find(id);
            if (egreetingRole == null)
            {
                return HttpNotFound();
            }
            return View(ViewNamesConstant.AdminEgreetingRolesEdit,egreetingRole);
        }

        // POST: EgreetingRoles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EgreetingRoleID,EgreetingRoleName,CreatedDate,ModifiedDate")] EgreetingRole egreetingRole)
        {
            if (ModelState.IsValid)
            {
                EgreetingRoleBusiness.Update(egreetingRole);
                EgreetingRoleBusiness.Save();
                return RedirectToAction("Index");
            }
            return View(ViewNamesConstant.AdminEgreetingRolesEdit,egreetingRole);
        }

        // GET: EgreetingRoles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EgreetingRole egreetingRole = EgreetingRoleBusiness.Find(id);
            if (egreetingRole == null)
            {
                return HttpNotFound();
            }
            return View(ViewNamesConstant.AdminEgreetingRolesDelete,egreetingRole);
        }

        // POST: EgreetingRoles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EgreetingRole egreetingRole = EgreetingRoleBusiness.Find(id);
            EgreetingRoleBusiness.Delete(egreetingRole);
            EgreetingRoleBusiness.Save();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                EgreetingRoleBusiness.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
