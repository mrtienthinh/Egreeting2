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
    public class OrdersController : ODataController 
    {
        private EgreetingContext db = new EgreetingContext();

        [EnableQuery]
        public IQueryable<Order> Get()
        {
            return db.Orders;
        }
        [EnableQuery]
        public SingleResult<Order> Get([FromODataUri] int key)
        {
            IQueryable<Order> result = db.Orders.Where(p => p.OrderID == key);
            return SingleResult.Create(result);
        }

        public async Task<IHttpActionResult> Post(Order Order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            db.Orders.Add(Order);
            await db.SaveChangesAsync();
            return Created(Order);
        }

        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<Order> Order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = await db.Orders.FindAsync(key);
            if (entity == null)
            {
                return NotFound();
            }
            Order.Patch(entity);
            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(key))
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
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Order update)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (key != update.OrderID)
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
                if (!OrderExists(key))
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
            var Order = await db.Orders.FindAsync(key);
            if (Order == null)
            {
                return NotFound();
            }
            db.Orders.Remove(Order);
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

        private bool OrderExists(int id)
        {
            return db.Orders.Count(e => e.OrderID == id) > 0;
        }
    }
}