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
    public class ScheduleSendersController : ODataController 
    {
        private EgreetingContext db = new EgreetingContext();

        [EnableQuery]
        public IQueryable<ScheduleSender> Get()
        {
            return db.ScheduleSenders;
        }
        [EnableQuery]
        public SingleResult<ScheduleSender> Get([FromODataUri] int key)
        {
            IQueryable<ScheduleSender> result = db.ScheduleSenders.Where(p => p.ScheduleSenderID == key);
            return SingleResult.Create(result);
        }

        public async Task<IHttpActionResult> Post(ScheduleSender ScheduleSender)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            db.ScheduleSenders.Add(ScheduleSender);
            await db.SaveChangesAsync();
            return Created(ScheduleSender);
        }

        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<ScheduleSender> ScheduleSender)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = await db.ScheduleSenders.FindAsync(key);
            if (entity == null)
            {
                return NotFound();
            }
            ScheduleSender.Patch(entity);
            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ScheduleSenderExists(key))
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
        public async Task<IHttpActionResult> Put([FromODataUri] int key, ScheduleSender update)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (key != update.ScheduleSenderID)
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
                if (!ScheduleSenderExists(key))
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
            var ScheduleSender = await db.ScheduleSenders.FindAsync(key);
            if (ScheduleSender == null)
            {
                return NotFound();
            }
            db.ScheduleSenders.Remove(ScheduleSender);
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

        private bool ScheduleSenderExists(int id)
        {
            return db.ScheduleSenders.Count(e => e.ScheduleSenderID == id) > 0;
        }
    }
}