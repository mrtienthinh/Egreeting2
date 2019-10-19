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
using System.IO;
using System.Web.Security;

namespace Egreeting.Web.Controllers.Admin
{
    [LogAction]
    public class EcardsController : BaseAdminController
    {
        private IEcardBusiness EcardBusiness;
        private ICategoryBusiness CategoryBusiness;
        private IEgreetingUserBusiness EgreetingUserBusiness;


        public EcardsController(IEcardBusiness EcardBusiness, ICategoryBusiness CategoryBusiness, IEgreetingUserBusiness EgreetingUserBusiness)
        {
            this.EcardBusiness = EcardBusiness;
            this.CategoryBusiness = CategoryBusiness;
            this.EgreetingUserBusiness = EgreetingUserBusiness;
        }

        // GET: Ecards
        public ActionResult Index(string search, int page = 1, int pageSize = 10)
        {
             var listModel = new List<Ecard>();
            if (!string.IsNullOrEmpty(search))
            {
                listModel = EcardBusiness.All.Where(x => x.EcardName.Contains(search) && !x.Status).OrderBy(x => x.EcardID).Skip((page - 1) * pageSize).Take(pageSize).ToList();
                ViewBag.totalItem = EcardBusiness.All.Count(x => x.EcardName.Contains(search) && !x.Status);
            }
            else
            {
                ViewBag.totalItem = EcardBusiness.All.Count(x => !x.Status);
                listModel = EcardBusiness.All.Where(x => !x.Status).OrderBy(x => x.EcardID).Skip((page - 1) * pageSize).Take(pageSize).ToList();
            }
            ViewBag.currentPage = page;
            ViewBag.pageSize = pageSize;
            ViewBag.search = search;
            return View(ViewNamesConstant.AdminEcardsIndex, listModel);
        }

        // GET: Ecards/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ecard ecard = EcardBusiness.Find(id);
            if (ecard == null)
            {
                return HttpNotFound();
            }
            return View(ViewNamesConstant.AdminEcardsDetails,ecard);
        }

        // GET: Ecards/Create
        public ActionResult Create()
        {
            ViewBag.Categories = CategoryBusiness.All.ToList();
            return View(ViewNamesConstant.AdminEcardsCreate);
        }

        // POST: Ecards/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EcardName,EcardSlug,EcardType,Price")] Ecard ecard, string ListCategoryString)
        {
            

            if (ModelState.IsValid)
            {
                var thumbnailFile = Request.Files["Thumbnail"];
                var ecardFile = Request.Files["EcardFile"];
                string pathThumbnail = Server.MapPath("~/Uploads/Thumbnails/");
                string pathEcardFiles = Server.MapPath("~/Uploads/EcardFiles/");

                if (!Directory.Exists(pathEcardFiles))
                {
                    Directory.CreateDirectory(pathEcardFiles);
                    if (!Directory.Exists(pathThumbnail) )
                    {
                        Directory.CreateDirectory(pathThumbnail);
                    }
                }


                if ( ecardFile != null)
                {
                    ecard.EcardUrl = "EcardUrl_" + DateTime.Now.ToFileTime();
                    ecardFile.SaveAs(pathEcardFiles + ecard.EcardUrl);

                    if ( thumbnailFile != null)
                    {
                        ecard.ThumbnailUrl = "Thumbnail_" + DateTime.Now.ToFileTime();
                        thumbnailFile.SaveAs(pathThumbnail + ecard.ThumbnailUrl);
                    }
                    else
                    {
                        ecard.ThumbnailUrl = "thumbnail.png";
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "There is no is ecard file found!");
                    ViewBag.Categories = CategoryBusiness.All.ToList();
                    return View(ViewNamesConstant.AdminEcardsCreate, ecard);
                }

                var lstCategoryID = ListCategoryString.Split('-').Where(x => x.Length > 0).Select(x => Convert.ToInt32(x)).ToList();
                var lstCategory = CategoryBusiness.All.Where(x => lstCategoryID.Contains(x.CategoryID)).ToList();
                ecard.Categories = lstCategory;
                //string email = "";
                //var currentContext = System.Web.HttpContext.Current;
                //if (currentContext.User != null)
                //{
                //    email = Membership.GetUser().Email;
                //}
                //var user = EgreetingUserBusiness.All.Where(x => x.Email.Equals(email)).FirstOrDefault();
                //ecard.EgreetingUser = user;
                EcardBusiness.Insert(ecard);
                EcardBusiness.Save();
                return RedirectToAction("Index");
            }
            ViewBag.Categories = CategoryBusiness.All.ToList();
            return View(ViewNamesConstant.AdminEcardsCreate, ecard);
        }

        // GET: Ecards/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ecard ecard = EcardBusiness.Find(id);
            if (ecard == null)
            {
                return HttpNotFound();
            }
            ViewBag.Categories = CategoryBusiness.All.ToList();
            return View(ViewNamesConstant.AdminEcardsEdit, ecard);
        }

        // POST: Ecards/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EcardID,EcardName,EcardSlug,EcardType,Price")] Ecard ecard)
        {
            

            if (ModelState.IsValid)
            {
                var ecardUpdate = EcardBusiness.Find(ecard.EcardID);

                var thumbnailFile = Request.Files["Thumbnail"];
                var ecardFile = Request.Files["EcardFile"];
                string pathThumbnail = Server.MapPath("~/Uploads/Thumbnails/");
                string pathEcardFiles = Server.MapPath("~/Uploads/EcardFiles/");
                if (!Directory.Exists(pathEcardFiles) && ecardFile != null)
                {
                    Directory.CreateDirectory(pathEcardFiles);
                    ecardUpdate.EcardUrl = "EcardUrl_" + DateTime.Now.ToFileTime();
                    ecardFile.SaveAs(pathEcardFiles + ecard.ThumbnailUrl);

                    if (!Directory.Exists(pathThumbnail) && thumbnailFile != null)
                    {
                        Directory.CreateDirectory(pathThumbnail);
                        ecardUpdate.ThumbnailUrl = "Thumbnail_" + DateTime.Now.ToFileTime();
                        thumbnailFile.SaveAs(pathThumbnail + ecard.ThumbnailUrl);
                    }
                }

                ecardUpdate.EcardName = ecard.EcardName;
                ecardUpdate.EcardSlug = ecard.EcardSlug;
                ecardUpdate.EcardType = ecard.EcardType;
                ecardUpdate.Price = ecard.Price;

                EcardBusiness.Update(ecardUpdate);
                EcardBusiness.Save();
                return RedirectToAction("Index");
            }
            ViewBag.Categories = CategoryBusiness.All.ToList();
            return View(ViewNamesConstant.AdminEcardsEdit, ecard);
        }

        // POST: Ecards/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int ItemID)
        {
            Ecard ecard = EcardBusiness.Find(ItemID);
            ecard.Status = true;
            EcardBusiness.Update(ecard);
            EcardBusiness.Save();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                EcardBusiness.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
