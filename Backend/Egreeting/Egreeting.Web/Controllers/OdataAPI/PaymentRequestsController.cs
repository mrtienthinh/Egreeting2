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
    public class PaymentsController : ODataController 
    {
        private EgreetingContext db = new EgreetingContext();

        [EnableQuery]
        public IQueryable<Payment> Get()
        {
            return db.Payments;
        }
        [EnableQuery]
        public SingleResult<Payment> Get([FromODataUri] int key)
        {
            IQueryable<Payment> result = db.Payments.Where(p => p.PaymentID == key);
            return SingleResult.Create(result);
        }

        public async Task<IHttpActionResult> Post(Payment Payment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            db.Payments.Add(Payment);
            await db.SaveChangesAsync();
            return Created(Payment);
        }

        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<Payment> Payment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = await db.Payments.FindAsync(key);
            if (entity == null)
            {
                return NotFound();
            }
            Payment.Patch(entity);
            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PaymentExists(key))
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
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Payment update)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (key != update.PaymentID)
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
                if (!PaymentExists(key))
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
            var Payment = await db.Payments.FindAsync(key);
            if (Payment == null)
            {
                return NotFound();
            }
            db.Payments.Remove(Payment);
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

        private bool PaymentExists(int id)
        {
            return db.Payments.Count(e => e.PaymentID == id) > 0;
        }
    }
}