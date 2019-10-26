﻿using Egreeting.Business.IBusiness;
using Egreeting.Domain;
using Egreeting.Models.AppContext;
using Egreeting.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Egreeting.Web.Controllers.Frontend
{
    public class TrackingController : Controller
    {
        private IOrderDetailBusiness OrderDetailBusiness;
        public TrackingController(IOrderDetailBusiness OrderDetailBusiness)
        {
            this.OrderDetailBusiness = OrderDetailBusiness;
        }
        // GET: Tracking
        public ActionResult Index()
        {
            return View("~/Views/Frontend/Tracking/Index.cshtml");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ShowOrderDetail(int orderID)
        {
            var listOrderDetails = OrderDetailBusiness.All.Where(x => x.Order.OrderID == orderID).ToList();
            return PartialView("~/Views/Frontend/Tracking/_OrderDetail.cshtml", listOrderDetails);
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