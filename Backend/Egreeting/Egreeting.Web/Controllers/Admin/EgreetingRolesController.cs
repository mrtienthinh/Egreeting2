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
    public class EgreetingRolesController : BaseAdminController
    {
        private IEgreetingRoleBusiness EgreetingRoleBusiness;
        public EgreetingRolesController(IEgreetingRoleBusiness EgreetingRoleBusiness)
        {
            this.EgreetingRoleBusiness = EgreetingRoleBusiness;
        }

        // GET: EgreetingRoles
        public ActionResult Index(string search, int page = 1, int pageSize = 10)
        {
            var listModel = new List<EgreetingRole>();
            if (!string.IsNullOrEmpty(search))
            {
                listModel = EgreetingRoleBusiness.All.Where(x => x.EgreetingRoleName.Contains(search)).OrderBy(x => x.EgreetingRoleID).Skip((page - 1) * pageSize).Take(pageSize).ToList();
                ViewBag.totalItem = EgreetingRoleBusiness.All.Count(x => x.EgreetingRoleName.Contains(search));
            }
            else
            {
                ViewBag.totalItem = EgreetingRoleBusiness.All.Count();
                listModel = EgreetingRoleBusiness.All.OrderBy(x => x.EgreetingRoleID).Skip((page - 1) * pageSize).Take(pageSize).ToList();
            }
            ViewBag.currentPage = page;
            ViewBag.pageSize = pageSize;
            ViewBag.search = search;
            return View(ViewNamesConstant.AdminEgreetingRolesIndex,listModel);
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
        public ActionResult Create([Bind(Include = "EgreetingRoleName")] EgreetingRole egreetingRole)
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
        public ActionResult Edit([Bind(Include = "EgreetingRoleID,EgreetingRoleName")] EgreetingRole egreetingRole)
        {
            if (ModelState.IsValid)
            {
                EgreetingRoleBusiness.Update(egreetingRole);
                EgreetingRoleBusiness.Save();
                return RedirectToAction("Index");
            }
            return View(ViewNamesConstant.AdminEgreetingRolesEdit,egreetingRole);
        }

        // POST: EgreetingRoles/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int ItemID)
        {
            EgreetingRoleBusiness.Delete(ItemID);
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
