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
        public ActionResult Index(int? OrderID, string search, int page = 1, int pageSize = 10)
        {
            var listModel = new List<OrderDetail>();
            if (OrderID == null || OrderID == 0)
            {
                return RedirectToAction("Index", "Orders");
            }
            else if(string.IsNullOrEmpty(search))
            {
                listModel = OrderDetailBusiness.All.Where(x => x.Order.OrderID == OrderID && x.Draft != true).OrderBy(x => x.CreatedDate).Skip((page - 1) * pageSize).Take(pageSize).ToList();
                ViewBag.totalItem = OrderDetailBusiness.All.Count(x => x.Order.OrderID == OrderID && x.Draft != true);
            }
            else
            {
                listModel = OrderDetailBusiness.All.Where(x => x.OrderDetailID.ToString().Contains(search) && x.Order.OrderID == OrderID && x.Draft != true).OrderBy(x => x.CreatedDate).Skip((page - 1) * pageSize).Take(pageSize).ToList();
                ViewBag.totalItem = OrderDetailBusiness.All.Count(x => x.Order.OrderID == OrderID && x.Draft != true);
            }
            ViewBag.orderID = OrderID;
            ViewBag.currentPage = page;
            ViewBag.pageSize = pageSize;
            ViewBag.search = search;
            return View(ViewNamesConstant.AdminOrderDetailsIndex, listModel);
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

        // POST: OrderDetails/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int ItemID)
        {
            OrderDetail OrderDetail = OrderDetailBusiness.Find(ItemID);
            OrderDetail.Draft = true;
            OrderDetail.ModifiedDate = DateTime.Now;
            OrderDetailBusiness.Update(OrderDetail);
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
