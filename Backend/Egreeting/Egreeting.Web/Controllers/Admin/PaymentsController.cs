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
    public class PaymentsController : Controller
    {
        private IPaymentBusiness PaymentBusiness;
        public PaymentsController(IPaymentBusiness PaymentBusiness)
        {
            this.PaymentBusiness = PaymentBusiness;
        }

        // GET: Payments
        public ActionResult Index()
        {
            return View(ViewNamesConstant.AdminPaymentsIndex,PaymentBusiness.All.ToList());
        }

        // GET: Payments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Payment Payment = PaymentBusiness.Find(id);
            if (Payment == null)
            {
                return HttpNotFound();
            }
            return View(ViewNamesConstant.AdminPaymentsDetails,Payment);
        }

        // GET: Payments/Create
        public ActionResult Create()
        {
            return View(ViewNamesConstant.AdminPaymentsCreate);
        }

        // POST: Payments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PaymentID,ReceiveMoney,IsReceived,CreatedDate,ModifiedDate")] Payment Payment)
        {
            if (ModelState.IsValid)
            {
                PaymentBusiness.Insert(Payment);
                PaymentBusiness.Save();
                return RedirectToAction("Index");
            }

            return View(ViewNamesConstant.AdminPaymentsCreate,Payment);
        }

        // GET: Payments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Payment Payment = PaymentBusiness.Find(id);
            if (Payment == null)
            {
                return HttpNotFound();
            }
            return View(ViewNamesConstant.AdminPaymentsEdit,Payment);
        }

        // POST: Payments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PaymentID,ReceiveMoney,IsReceived,CreatedDate,ModifiedDate")] Payment Payment)
        {
            if (ModelState.IsValid)
            {
                PaymentBusiness.Update(Payment);
                PaymentBusiness.Save();
                return RedirectToAction("Index");
            }
            return View(ViewNamesConstant.AdminPaymentsEdit,Payment);
        }

        // GET: Payments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Payment Payment = PaymentBusiness.Find(id);
            if (Payment == null)
            {
                return HttpNotFound();
            }
            return View(ViewNamesConstant.AdminPaymentsDelete,Payment);
        }

        // POST: Payments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Payment Payment = PaymentBusiness.Find(id);
            PaymentBusiness.Delete(Payment);
            PaymentBusiness.Save();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                PaymentBusiness.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
