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
using Egreeting.Web.Filters;

namespace Egreeting.Web.Controllers.Admin
{
    [LogAction]
    [RoleAuthorize(Roles = "Admin")]
    public class OrderDetailsController : BaseAdminController
    {
        private IOrderDetailBusiness OrderDetailBusiness;
        public OrderDetailsController(IOrderDetailBusiness OrderDetailBusiness)
        {
            this.OrderDetailBusiness = OrderDetailBusiness;
        }

        // GET: OrderDetails
        public ActionResult Index()
        {
            return View(ViewNamesConstant.AdminOrderDetailsIndex,OrderDetailBusiness.All.ToList());
        }

        // GET: OrderDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderDetail OrderDetail = OrderDetailBusiness.Find(id);
            if (OrderDetail == null)
            {
                return HttpNotFound();
            }
            return View(ViewNamesConstant.AdminOrderDetailsDetails,OrderDetail);
        }

        // GET: OrderDetails/Create
        public ActionResult Create()
        {
            return View(ViewNamesConstant.AdminOrderDetailsCreate);
        }

        // POST: OrderDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SenderEmail,RecipientEmail,SendSubject,SendMessage,TimeSuccess,CreatedDate,ModifiedDate")] OrderDetail OrderDetail)
        {
            if (ModelState.IsValid)
            {
                OrderDetailBusiness.Insert(OrderDetail);
                OrderDetailBusiness.Save();
                return RedirectToAction("Index");
            }

            return View(ViewNamesConstant.AdminOrderDetailsCreate,OrderDetail);
        }

        // GET: OrderDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderDetail OrderDetail = OrderDetailBusiness.Find(id);
            if (OrderDetail == null)
            {
                return HttpNotFound();
            }
            return View(ViewNamesConstant.AdminOrderDetailsEdit,OrderDetail);
        }

        // POST: OrderDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SenderEmail,RecipientEmail,SendSubject,SendMessage,TimeSuccess,CreatedDate,ModifiedDate")] OrderDetail OrderDetail)
        {
            if (ModelState.IsValid)
            {
                OrderDetailBusiness.Update(OrderDetail);
                OrderDetailBusiness.Save();
                return RedirectToAction("Index");
            }
            return View(ViewNamesConstant.AdminOrderDetailsEdit,OrderDetail);
        }

        // GET: OrderDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderDetail OrderDetail = OrderDetailBusiness.Find(id);
            if (OrderDetail == null)
            {
                return HttpNotFound();
            }
            return View(ViewNamesConstant.AdminOrderDetailsDelete,OrderDetail);
        }

        // POST: OrderDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OrderDetail OrderDetail = OrderDetailBusiness.Find(id);
            OrderDetailBusiness.Delete(OrderDetail);
            OrderDetailBusiness.Save();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                OrderDetailBusiness.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
