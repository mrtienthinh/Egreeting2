﻿using System;
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
    public class EgreetingUsersController : Controller
    {
        private IEgreetingUserBusiness EgreetingUserBusiness;
        public EgreetingUsersController(IEgreetingUserBusiness EgreetingUserBusiness)
        {
            this.EgreetingUserBusiness = EgreetingUserBusiness;
        }

        // GET: EgreetingUsers
        public ActionResult Index()
        {
            return View(ViewNamesConstant.FrontendEgreetingUsersIndex, EgreetingUserBusiness.All.ToList());
        }

        // GET: EgreetingUsers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EgreetingUser egreetingUser = EgreetingUserBusiness.Find(id);
            if (egreetingUser == null)
            {
                return HttpNotFound();
            }
            return View(ViewNamesConstant.FrontendEgreetingUsersDetails, egreetingUser);
        }

        // GET: EgreetingUsers/Create
        public ActionResult Create()
        {
            return View(ViewNamesConstant.FrontendEgreetingUsersCreate);
        }

        // POST: EgreetingUsers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EgreetingUserID,EgreetingUserSlug,FirstName,LastName,BirthDay,CreditCardNumber,CreditCardCVG,PaymentDueDate,CreatedDate,ModifiedDate")] EgreetingUser egreetingUser)
        {
            if (ModelState.IsValid)
            {
                EgreetingUserBusiness.Insert(egreetingUser);
                EgreetingUserBusiness.Save();
                return RedirectToAction("Index");
            }

            return View(ViewNamesConstant.FrontendEgreetingUsersCreate, egreetingUser);
        }

        // GET: EgreetingUsers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EgreetingUser egreetingUser = EgreetingUserBusiness.Find(id);
            if (egreetingUser == null)
            {
                return HttpNotFound();
            }
            return View(ViewNamesConstant.FrontendEgreetingUsersEdit, egreetingUser);
        }

        // POST: EgreetingUsers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EgreetingUserID,EgreetingUserSlug,FirstName,LastName,BirthDay,CreditCardNumber,CreditCardCVG,PaymentDueDate,CreatedDate,ModifiedDate")] EgreetingUser egreetingUser)
        {
            if (ModelState.IsValid)
            {
                EgreetingUserBusiness.Update(egreetingUser);
                EgreetingUserBusiness.Save();
                return RedirectToAction("Index");
            }
            return View(ViewNamesConstant.FrontendEgreetingUsersEdit, egreetingUser);
        }

        // GET: EgreetingUsers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EgreetingUser egreetingUser = EgreetingUserBusiness.Find(id);
            if (egreetingUser == null)
            {
                return HttpNotFound();
            }
            return View(ViewNamesConstant.FrontendEgreetingUsersDelete, egreetingUser);
        }

        // POST: EgreetingUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EgreetingUser egreetingUser = EgreetingUserBusiness.Find(id);
            EgreetingUserBusiness.Delete(egreetingUser);
            EgreetingUserBusiness.Save();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                EgreetingUserBusiness.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
