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
    public class EgreetingRolesController : ODataController 
    {
        private EgreetingContext db = new EgreetingContext();

        [EnableQuery]
        public IQueryable<EgreetingRole> Get()
        {
            return db.EgreetingRoles;
        }
        [EnableQuery]
        public SingleResult<EgreetingRole> Get([FromODataUri] int key)
        {
            IQueryable<EgreetingRole> result = db.EgreetingRoles.Where(p => p.EgreetingRoleID == key);
            return SingleResult.Create(result);
        }

        public async Task<IHttpActionResult> Post(EgreetingRole EgreetingRole)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            db.EgreetingRoles.Add(EgreetingRole);
            await db.SaveChangesAsync();
            return Created(EgreetingRole);
        }

        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<EgreetingRole> EgreetingRole)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = await db.EgreetingRoles.FindAsync(key);
            if (entity == null)
            {
                return NotFound();
            }
            EgreetingRole.Patch(entity);
            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EgreetingRoleExists(key))
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
        public async Task<IHttpActionResult> Put([FromODataUri] int key, EgreetingRole update)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (key != update.EgreetingRoleID)
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
                if (!EgreetingRoleExists(key))
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
            var EgreetingRole = await db.EgreetingRoles.FindAsync(key);
            if (EgreetingRole == null)
            {
                return NotFound();
            }
            db.EgreetingRoles.Remove(EgreetingRole);
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

        private bool EgreetingRoleExists(int id)
        {
            return db.EgreetingRoles.Count(e => e.EgreetingRoleID == id) > 0;
        }
    }
}