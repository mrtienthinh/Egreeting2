using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Globalization;
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
    public class CategoriesController : BaseAdminController
    {
        private ICategoryBusiness CategoryBusiness;
        public CategoriesController(ICategoryBusiness CategoryBusiness)
        {
            this.CategoryBusiness = CategoryBusiness;
        }

        // GET: Categorys
        public ActionResult Index(string search, int page = 1, int pageSize = 10)
        {
            var listModel = new List<Category>();
            if (!string.IsNullOrEmpty(search))
            {
                listModel = CategoryBusiness.All.Where(x => x.CategoryName.Contains(search)).OrderBy(x => x.CategoryID).Skip((page - 1) * pageSize).Take(pageSize).ToList();
                ViewBag.totalItem = CategoryBusiness.All.Count(x => x.CategoryName.Contains(search));
            }
            else
            {
                ViewBag.totalItem = CategoryBusiness.All.Count();
                listModel = CategoryBusiness.All.OrderBy(x => x.CategoryID).Skip((page - 1) * pageSize).Take(pageSize).ToList();
            }
            ViewBag.currentPage = page;
            ViewBag.pageSize = pageSize;
            ViewBag.search = search;
            return View(ViewNamesConstant.AdminCategoriesIndex, listModel);
        }

        // GET: Categorys/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category Category = CategoryBusiness.Find(id);
            if (Category == null)
            {
                return HttpNotFound();
            }
            return View(ViewNamesConstant.AdminCategoriesDetails,Category);
        }

        // GET: Categorys/Create
        public ActionResult Create()
        {
            return View(ViewNamesConstant.AdminCategoriesCreate);
        }

        // POST: Categorys/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CategorySlug,CategoryName")] Category Category)
        {
            if (ModelState.IsValid)
            {
                CategoryBusiness.Insert(Category);
                CategoryBusiness.Save();
                return RedirectToAction("Index");
            }

            return View(ViewNamesConstant.AdminCategoriesCreate, Category);
        }

        // GET: Categorys/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category Category = CategoryBusiness.Find(id);
            if (Category == null)
            {
                return HttpNotFound();
            }
            return View(ViewNamesConstant.AdminCategoriesEdit, Category);
        }

        // POST: Categorys/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CategorySlug,CategoryName")] Category Category)
        {
            if (ModelState.IsValid)
            {
                CategoryBusiness.Update(Category);
                CategoryBusiness.Save();
                return RedirectToAction("Index");
            }
            return View(ViewNamesConstant.AdminCategoriesEdit, Category);
        }

        // GET: Categorys/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category Category = CategoryBusiness.Find(id);
            if (Category == null)
            {
                return HttpNotFound();
            }
            return View(ViewNamesConstant.AdminCategoriesDelete,Category);
        }

        // POST: Categorys/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Category Category = CategoryBusiness.Find(id);
            CategoryBusiness.Delete(Category);
            CategoryBusiness.Save();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                CategoryBusiness.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
