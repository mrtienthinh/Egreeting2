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
    public class CategoriesController : ODataController 
    {
        private EgreetingContext db = new EgreetingContext();

        [EnableQuery(PageSize = 10)]
        public IQueryable<Category> Get()
        {
            return db.Categories;
        }
        [EnableQuery]
        public SingleResult<Category> Get([FromODataUri] int key)
        {
            IQueryable<Category> result = db.Categories.Where(p => p.CategoryID == key);
            return SingleResult.Create(result);
        }

        public async Task<IHttpActionResult> Post(Category Category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            db.Categories.Add(Category);
            await db.SaveChangesAsync();
            return Created(Category);
        }

        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<Category> Category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = await db.Categories.FindAsync(key);
            if (entity == null)
            {
                return NotFound();
            }
            Category.Patch(entity);
            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(key))
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
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Category update)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (key != update.CategoryID)
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
                if (!CategoryExists(key))
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
            var Category = await db.Categories.FindAsync(key);
            if (Category == null)
            {
                return NotFound();
            }
            db.Categories.Remove(Category);
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

        private bool CategoryExists(int id)
        {
            return db.Categories.Count(e => e.CategoryID == id) > 0;
        }
    }
}