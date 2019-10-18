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
using Microsoft.AspNet.Identity.Owin;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace Egreeting.Web.Controllers.Admin
{
    [LogAction]
    public class EgreetingUsersController : BaseAdminController
    {
        private ApplicationUserManager _userManager;
        private IEgreetingUserBusiness EgreetingUserBusiness;
        private IEgreetingRoleBusiness EgreetingRoleBusiness;

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        public EgreetingUsersController(IEgreetingUserBusiness EgreetingUserBusiness, IEgreetingRoleBusiness EgreetingRoleBusiness)
        {
            this.EgreetingUserBusiness = EgreetingUserBusiness;
            this.EgreetingRoleBusiness = EgreetingRoleBusiness;
        }

        // GET: EgreetingUsers
        public ActionResult Index(string search, int page = 1, int pageSize = 10)
        {
            var listModel = new List<ApplicationUser>();
            if (!string.IsNullOrEmpty(search))
            {
                listModel = UserManager.Users.Where(x => !x.EgreetingUser.Status).Where(x => x.Email.Contains(search)).OrderBy(x => x.Id).Skip((page - 1) * pageSize).Take(pageSize).ToList();
            }
            else
            {
                ViewBag.totalItem = UserManager.Users.Count(x => !x.EgreetingUser.Status);
                listModel = UserManager.Users.Where(x => !x.EgreetingUser.Status).OrderBy(x => x.Id).Skip((page - 1) * pageSize).Take(pageSize).ToList();
            }
            ViewBag.currentPage = page;
            ViewBag.pageSize = pageSize;
            ViewBag.search = search;
            return View(ViewNamesConstant.AdminEgreetingUsersIndex, listModel);
        }

        // GET: EgreetingUsers/Details/5
        public ActionResult Details(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EgreetingUser egreetingUser = UserManager.FindById(id).EgreetingUser;
            if (egreetingUser == null)
            {
                return HttpNotFound();
            }
            return View(ViewNamesConstant.AdminEgreetingUsersDetails,egreetingUser);
        }

        // GET: EgreetingUsers/Create
        public ActionResult Create()
        {
            ViewBag.ListRole = EgreetingRoleBusiness.All.Where(x => !x.Status).ToList();
            return View(ViewNamesConstant.AdminEgreetingUsersCreate);
        }

        // POST: EgreetingUsers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "EgreetingUserSlug,FirstName,LastName,Email,Password,ConfirmPassword,BirthDay,CreditCardNumber,CreditCardCVG,PaymentDueDate")] EgreetingUser egreetingUser, string ListRole)
        {
            var file = Request.Files["Avatar"];
            byte[] image = new byte[file.ContentLength];
            file.InputStream.Read(image, 0, image.Length);

            if (file.ContentLength == 0)
            {
                image = System.IO.File.ReadAllBytes(Server.MapPath("~/Content/Admin/dist/img/avatar.png"));
            }

            if (ModelState.IsValid)
            {
                var lstRoleId = ListRole.Split('-').Where(x => x.Length > 0).Select(x => Convert.ToInt32(x)).ToList();
                var lstEgreetingRole = EgreetingRoleBusiness.All.Where(x => !x.Status && lstRoleId.Contains(x.EgreetingRoleID)).ToList();
                egreetingUser.CreatedDate = DateTime.Now;
                egreetingUser.Avatar = image;
                egreetingUser.EgreetingRoles = lstEgreetingRole;
                var applicationUser = new ApplicationUser { Email = egreetingUser.Email, UserName = egreetingUser.Email, EgreetingUser = egreetingUser };
                var result = await UserManager.CreateAsync(applicationUser, egreetingUser.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                AddErrors(result);
            }
            return View(ViewNamesConstant.AdminEgreetingUsersCreate,egreetingUser);
        }

        // GET: EgreetingUsers/Edit/5
        public ActionResult Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EgreetingUser egreetingUser = UserManager.FindById(id).EgreetingUser;
            ViewBag.UserId = id;
            if (egreetingUser == null)
            {
                return HttpNotFound();
            }
            return View(ViewNamesConstant.AdminEgreetingUsersEdit,egreetingUser);
        }

        // POST: EgreetingUsers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EgreetingUserSlug,FirstName,LastName,Email,Password,ConfirmPassword,BirthDay,CreditCardNumber,CreditCardCVG,PaymentDueDate")] EgreetingUser egreetingUser, string UserId)
        {
            var file = Request.Files["Avatar"];
            byte[] image = new byte[file.ContentLength];
            file.InputStream.Read(image, 0, image.Length);

            if (ModelState.IsValid)
            {
                var user = UserManager.FindById(UserId);
                user.EgreetingUser.EgreetingUserSlug = egreetingUser.EgreetingUserSlug;
                user.EgreetingUser.FirstName = egreetingUser.FirstName;
                user.EgreetingUser.LastName = egreetingUser.LastName;
                user.EgreetingUser.BirthDay = egreetingUser.BirthDay;
                user.EgreetingUser.CreditCardNumber = egreetingUser.EgreetingUserSlug;
                user.EgreetingUser.CreditCardCVG = egreetingUser.CreditCardCVG;
                user.EgreetingUser.PaymentDueDate = egreetingUser.PaymentDueDate;
                if(file.ContentLength > 0)
                {
                    user.EgreetingUser.Avatar = image;
                }
                if (string.IsNullOrEmpty(egreetingUser.Password))
                {
                    UserManager.RemovePassword(user.Id);
                    UserManager.AddPassword(user.Id, user.EgreetingUser.Password);
                    UserManager.Update(user);
                }
                else
                {
                    UserManager.Update(user);
                }
                return RedirectToAction("Index");
            }
            return View(ViewNamesConstant.AdminEgreetingUsersEdit,egreetingUser);
        }

        // POST: EgreetingUsers/Delete/5
        [HttpPost]
        public ActionResult Delete(string ItemID)
        {
            if (string.IsNullOrEmpty(ItemID))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = UserManager.FindById(ItemID);
            
            if (user == null)
            {
                return HttpNotFound();
            }
            user.EgreetingUser.Status = true;

            var resultUpdate = UserManager.Update(user);
            if (resultUpdate.Succeeded)
            {
                return RedirectToAction("Index");
            }
            return View(ViewNamesConstant.AdminEgreetingUsersDetails,user.EgreetingUser);
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
