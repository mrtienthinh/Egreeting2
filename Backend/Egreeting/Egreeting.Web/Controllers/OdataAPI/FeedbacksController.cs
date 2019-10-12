using System.Data;
using System.Linq;
using System.Web.Http;
using Egreeting.Models.Models;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNet.OData;
using System.Threading.Tasks;
using System.Net;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;
using Egreeting.Models.AppContext;

namespace Egreeting.Web.Controllers.OdataAPI
{
    [ODataRoutePrefix("odata")]
    public class FeedbacksController : ODataController 
    {
        private EgreetingContext db = new EgreetingContext();

        [EnableQuery]
        public IQueryable<Feedback> Get()
        {
            return db.Feedbacks;
        }
        [EnableQuery]
        public SingleResult<Feedback> Get([FromODataUri] int key)
        {
            IQueryable<Feedback> result = db.Feedbacks.Where(p => p.FeedbackID == key);
            return SingleResult.Create(result);
        }

        public async Task<IHttpActionResult> Post(Feedback Feedback)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            db.Feedbacks.Add(Feedback);
            await db.SaveChangesAsync();
            return Created(Feedback);
        }

        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<Feedback> Feedback)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = await db.Feedbacks.FindAsync(key);
            if (entity == null)
            {
                return NotFound();
            }
            Feedback.Patch(entity);
            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FeedbackExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Updated(entity);
        }
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Feedback update)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (key != update.FeedbackID)
            {
                return BadRequest();
            }
            db.Entry(update).State = EntityState.Modified;
            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FeedbackExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Updated(update);
        }

        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            var Feedback = await db.Feedbacks.FindAsync(key);
            if (Feedback == null)
            {
                return NotFound();
            }
            db.Feedbacks.Remove(Feedback);
            await db.SaveChangesAsync();
            return StatusCode(HttpStatusCode.NoContent);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FeedbackExists(int id)
        {
            return db.Feedbacks.Count(e => e.FeedbackID == id) > 0;
        }
    }
}