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
    public class EgreetingUsersController : ODataController 
    {
        private EgreetingContext db = new EgreetingContext();

        [EnableQuery]
        public IQueryable<EgreetingUser> Get()
        {
            return db.EgreetingUsers;
        }
        [EnableQuery]
        public SingleResult<EgreetingUser> Get([FromODataUri] int key)
        {
            IQueryable<EgreetingUser> result = db.EgreetingUsers.Where(p => p.EgreetingUserID == key);
            return SingleResult.Create(result);
        }

        public async Task<IHttpActionResult> Post(EgreetingUser EgreetingUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            db.EgreetingUsers.Add(EgreetingUser);
            await db.SaveChangesAsync();
            return Created(EgreetingUser);
        }

        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<EgreetingUser> EgreetingUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = await db.EgreetingUsers.FindAsync(key);
            if (entity == null)
            {
                return NotFound();
            }
            EgreetingUser.Patch(entity);
            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EgreetingUserExists(key))
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
        public async Task<IHttpActionResult> Put([FromODataUri] int key, EgreetingUser update)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (key != update.EgreetingUserID)
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
                if (!EgreetingUserExists(key))
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
            var EgreetingUser = await db.EgreetingUsers.FindAsync(key);
            if (EgreetingUser == null)
            {
                return NotFound();
            }
            db.EgreetingUsers.Remove(EgreetingUser);
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

        private bool EgreetingUserExists(int id)
        {
            return db.EgreetingUsers.Count(e => e.EgreetingUserID == id) > 0;
        }
    }
}