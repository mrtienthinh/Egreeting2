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
    public class EcardsController : ODataController 
    {
        private EgreetingContext db = new EgreetingContext();

        [EnableQuery(PageSize = 1)]
        public IQueryable<Ecard> Get()
        {
            return db.Ecards;
        }
        [EnableQuery]
        public SingleResult<Ecard> Get([FromODataUri] int key)
        {
            IQueryable<Ecard> result = db.Ecards.Where(p => p.EcardID == key);
            return SingleResult.Create(result);
        }

        public async Task<IHttpActionResult> Post(Ecard ecard)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            db.Ecards.Add(ecard);
            await db.SaveChangesAsync();
            return Created(ecard);
        }

        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<Ecard> ecard)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = await db.Ecards.FindAsync(key);
            if (entity == null)
            {
                return NotFound();
            }
            ecard.Patch(entity);
            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EcardExists(key))
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
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Ecard update)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (key != update.EcardID)
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
                if (!EcardExists(key))
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
            var ecard = await db.Ecards.FindAsync(key);
            if (ecard == null)
            {
                return NotFound();
            }
            db.Ecards.Remove(ecard);
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

        private bool EcardExists(int id)
        {
            return db.Ecards.Count(e => e.EcardID == id) > 0;
        }
    }
}