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
    public class EcardsController : BaseController
    {
        private IEcardBusiness EcardBusiness;
        public EcardsController(IEcardBusiness EcardBusiness)
        {
            this.EcardBusiness = EcardBusiness;
        }

        [Route("Ecards/{slug}")]
        // GET: Ecards/Details/5
        public ActionResult Details(string slug)
        {
            if (string.IsNullOrEmpty(slug))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ecard ecard = EcardBusiness.All.Where(x => x.EcardSlug.Equals(slug)).FirstOrDefault();
            if (ecard == null)
            {
                return HttpNotFound();
            }
            ViewBag.topEcards = EcardBusiness.All.Where(x => x.Draft != true).OrderBy(x => x.Price).Take(12).ToList();
            return View(ViewNamesConstant.FrontendEcardsDetails, ecard);
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
