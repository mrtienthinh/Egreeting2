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
    public class OrderDetailsController : ODataController 
    {
        private EgreetingContext db = new EgreetingContext();

        [EnableQuery]
        public IQueryable<OrderDetail> Get()
        {
            return db.OrderDetails;
        }
        [EnableQuery]
        public SingleResult<OrderDetail> Get([FromODataUri] int key)
        {
            IQueryable<OrderDetail> result = db.OrderDetails.Where(p => p.OrderDetailID == key);
            return SingleResult.Create(result);
        }

        public async Task<IHttpActionResult> Post(OrderDetail OrderDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            db.OrderDetails.Add(OrderDetail);
            await db.SaveChangesAsync();
            return Created(OrderDetail);
        }

        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<OrderDetail> OrderDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = await db.OrderDetails.FindAsync(key);
            if (entity == null)
            {
                return NotFound();
            }
            OrderDetail.Patch(entity);
            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderDetailExists(key))
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
        public async Task<IHttpActionResult> Put([FromODataUri] int key, OrderDetail update)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (key != update.OrderDetailID)
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
                if (!OrderDetailExists(key))
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
            var OrderDetail = await db.OrderDetails.FindAsync(key);
            if (OrderDetail == null)
            {
                return NotFound();
            }
            db.OrderDetails.Remove(OrderDetail);
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

        private bool OrderDetailExists(int id)
        {
            return db.OrderDetails.Count(e => e.OrderDetailID == id) > 0;
        }
    }
}