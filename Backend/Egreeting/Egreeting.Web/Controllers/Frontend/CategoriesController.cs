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
    public class CategoriesController : Controller
    {
        private ICategoryBusiness CategoryBusiness;
        public CategoriesController(ICategoryBusiness CategoryBusiness)
        {
            this.CategoryBusiness = CategoryBusiness;
        }

        // GET: Categorys
        public ActionResult Index()
        {
            return View(ViewNamesConstant.FrontendCategoriesIndex, CategoryBusiness.All.ToList());
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
            return View(ViewNamesConstant.FrontendCategoriesDetails, Category);
        }

        // GET: Categorys/Create
        public ActionResult Create()
        {
            return View(ViewNamesConstant.FrontendCategoriesCreate);
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

            return View(ViewNamesConstant.FrontendCategoriesCreate, Category);
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
            return View(ViewNamesConstant.FrontendCategoriesEdit, Category);
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
            return View(ViewNamesConstant.FrontendCategoriesEdit, Category);
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
            return View(ViewNamesConstant.FrontendCategoriesDelete, Category);
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
