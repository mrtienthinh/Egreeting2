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
    public class OrdersController : BaseController
    {
        private IOrderBusiness OrderBusiness;
        public OrdersController(IOrderBusiness OrderBusiness)
        {
            this.OrderBusiness = OrderBusiness;
        }

        // GET: Orders
        public ActionResult Index()
        {
            return View(ViewNamesConstant.FrontendOrdersIndex, OrderBusiness.All.ToList());
        }

        // GET: Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order Order = OrderBusiness.Find(id);
            if (Order == null)
            {
                return HttpNotFound();
            }
            return View(ViewNamesConstant.FrontendOrdersDetails, Order);
        }

        // GET: Orders/Create
        public ActionResult Create()
        {
            return View(ViewNamesConstant.FrontendOrdersCreate);
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OrderID,SenderEmail,RecipientEmail,SendSubject,SendMessage,TimeSuccess,CreatedDate,ModifiedDate")] Order Order)
        {
            if (ModelState.IsValid)
            {
                OrderBusiness.Insert(Order);
                OrderBusiness.Save();
                return RedirectToAction("Index");
            }

            return View(ViewNamesConstant.FrontendOrdersCreate, Order);
        }

        // GET: Orders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order Order = OrderBusiness.Find(id);
            if (Order == null)
            {
                return HttpNotFound();
            }
            return View(ViewNamesConstant.FrontendOrdersEdit, Order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrderID,SenderEmail,RecipientEmail,SendSubject,SendMessage,TimeSuccess,CreatedDate,ModifiedDate")] Order Order)
        {
            if (ModelState.IsValid)
            {
                OrderBusiness.Update(Order);
                OrderBusiness.Save();
                return RedirectToAction("Index");
            }
            return View(ViewNamesConstant.FrontendOrdersEdit, Order);
        }

        // GET: Orders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order Order = OrderBusiness.Find(id);
            if (Order == null)
            {
                return HttpNotFound();
            }
            return View(ViewNamesConstant.FrontendOrdersDelete, Order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order Order = OrderBusiness.Find(id);
            OrderBusiness.Delete(Order);
            OrderBusiness.Save();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                OrderBusiness.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
