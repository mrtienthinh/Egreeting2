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
    public class SubcribersController : ODataController 
    {
        private EgreetingContext db = new EgreetingContext();

        [EnableQuery]
        public IQueryable<Subcriber> Get()
        {
            return db.Subcribers;
        }
        [EnableQuery]
        public SingleResult<Subcriber> Get([FromODataUri] int key)
        {
            IQueryable<Subcriber> result = db.Subcribers.Where(p => p.SubcriberID == key);
            return SingleResult.Create(result);
        }

        public async Task<IHttpActionResult> Post(Subcriber Subcriber)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            db.Subcribers.Add(Subcriber);
            await db.SaveChangesAsync();
            return Created(Subcriber);
        }

        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<Subcriber> Subcriber)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = await db.Subcribers.FindAsync(key);
            if (entity == null)
            {
                return NotFound();
            }
            Subcriber.Patch(entity);
            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubcriberExists(key))
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
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Subcriber update)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (key != update.SubcriberID)
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
                if (!SubcriberExists(key))
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
            var Subcriber = await db.Subcribers.FindAsync(key);
            if (Subcriber == null)
            {
                return NotFound();
            }
            db.Subcribers.Remove(Subcriber);
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

        private bool SubcriberExists(int id)
        {
            return db.Subcribers.Count(e => e.SubcriberID == id) > 0;
        }
    }
}